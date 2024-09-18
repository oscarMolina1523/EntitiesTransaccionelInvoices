using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Data.Builders;
using System;
using System.Data;
using System.Linq.Expressions;

namespace Infrastructure.Endpoint.Data.Interfaces
{

    public interface ISqlEntitySettingsBuilder
    {
        //IHaveTableSettings<TEntity> Entity<TEntity>() where TEntity : BaseEntity;        
        IExecuteEntityBuilder Entity<TEntity>(Action<IHaveTableSettings<TEntity>> action) where TEntity : BaseEntity;
    }

    public interface IHaveTableSettings<TEntity> where TEntity : BaseEntity
    {
        //IHavePropertySettings<TEntity> Table(string tableName);
        //IHavePropertySettings<TEntity> Table(string tableName, string schema);
        void Table(string tableName);
        void Table(string tableName, string schema);

        IHavePropertyName<TEntity, TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> keyExpression);
    }

    public interface IHavePropertyName<TEntity, TProperty> where TEntity : BaseEntity
    {
        IHaveSqlDbType<TEntity, TProperty> WithName(string name);
        IHaveSqlDbType<TEntity, TProperty> SetDefaultName();
    }

    public interface IHaveSqlDbType<TEntity, TProperty> where TEntity : BaseEntity
    {
        IHaveConversion<TEntity, TProperty> WithSqlDbType(SqlDbType sqlDbType);
    }

    public interface IHaveConversion<TEntity, TProperty> : IHaveComputedColumn, IAddPropertySettings where TEntity : BaseEntity
    {
        IHavePrimaryKey WithConversion<TProvider>(
            Expression<Func<TProperty, TProvider>> outgoing,
            Expression<Func<TProvider, TProperty>> incomming
        );

        IAddPropertySettings AsPrimaryKey();
        //void AddProperty();
    }

    public interface IHaveComputedColumn : IAddPropertySettings
    {
        IAddPropertySettings AsComputed();
    }

    public interface IHavePrimaryKey : IAddPropertySettings
    {
        IAddPropertySettings AsPrimaryKey();
    }

    public interface IAddPropertySettings
    {
        void AddProperty();
    }

    public interface IExecuteEntityBuilder
    {
        SqlEntitySettings Build();
    }
}
