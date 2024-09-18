using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Endpoint.Data.Builders
{
    public sealed class SqlEntitySettingsBuilder : ISqlEntitySettingsBuilder
    {
        public IExecuteEntityBuilder Entity<TEntity>(Action<IHaveTableSettings<TEntity>> action) where TEntity : BaseEntity
        {
            var entityBuilder = new SqlEntitySettingsBuilder<TEntity>();
            action.Invoke(entityBuilder);
            return entityBuilder;
        }
    }

    public class SqlEntitySettingsBuilder<TEntity> : IHaveTableSettings<TEntity>, IExecuteEntityBuilder where TEntity : BaseEntity
    {
        private string _tableName;
        private string _schema = string.Empty;
        private List<SqlColumnSettings> _properties = new List<SqlColumnSettings>();

        public void Table(string tableName)
        {
            _tableName = tableName;
        }

        public void Table(string tableName, string schema)
        {
            _tableName = tableName;
            _schema = schema;
        }

        public IHavePropertyName<TEntity, TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> keyExpression)
        {
            var sqlPropetySettings = new SqlPropertySettingsBuilder<TEntity, TProperty>(keyExpression);
            _properties.Add(sqlPropetySettings.Settings);
            return sqlPropetySettings;
        }


        public SqlEntitySettings Build()
        {
            return new SqlEntitySettings
            {
                TableName = _tableName,
                Schema = _schema,
                Columns = _properties.Where(prop => prop.IsComplete).ToList(),
            };
        }
    }

    public class SqlPropertySettingsBuilder<TEntity, TProperty> :
        IHavePropertyName<TEntity, TProperty>,
        IHaveSqlDbType<TEntity, TProperty>,
        IHaveConversion<TEntity, TProperty>,
        IHaveComputedColumn,
        IHavePrimaryKey,
        IAddPropertySettings
        where TEntity : BaseEntity
    {
        public SqlColumnSettings Settings = new SqlColumnSettings();
        private readonly Expression<Func<TEntity, TProperty>> _lambdaExpression;

        public SqlPropertySettingsBuilder(Expression<Func<TEntity, TProperty>> lambdaExpression)
        {
            _lambdaExpression = lambdaExpression;
        }

        public IHaveSqlDbType<TEntity, TProperty> WithName(string name)
        {
            Settings.Name = name;
            Settings.DomainName = GetName(_lambdaExpression);
            return this;
        }

        public IHaveSqlDbType<TEntity, TProperty> SetDefaultName()
        {
            Settings.Name = GetName(_lambdaExpression);
            Settings.DomainName = GetName(_lambdaExpression);
            return this;
        }

        private string GetName(Expression<Func<TEntity, TProperty>> lambdaExpression)
        {
            MemberExpression body = lambdaExpression.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)lambdaExpression.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        public IHaveConversion<TEntity, TProperty> WithSqlDbType(SqlDbType sqlDbType)
        {
            Settings.SqlDbType = sqlDbType;
            return this;
        }

        public IHavePrimaryKey WithConversion<TProvider>(
            Expression<Func<TProperty, TProvider>> outgoing,
            Expression<Func<TProvider, TProperty>> incomming)
        {
            var conversion = new PropertyConversionData()
            {
                OutgoingConversion = outgoing.Compile(),
                IncommingConversion = incomming.Compile(),
                ProviderType = typeof(TProvider),
                PropertyType = typeof(TProperty)
            };

            Settings.Conversion = conversion;
            return this;
        }

        public IAddPropertySettings AsPrimaryKey()
        {
            Settings.IsPrimaryKey = true;
            return this;
        }

        public IAddPropertySettings AsComputed()
        {
            Settings.IsComputedColumn = true;
            return this;
        }

        public void AddProperty()
        {
            Settings.IsComplete = true;
            Settings.IsNullable = typeof(TProperty).IsGenericType && typeof(TProperty).GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
