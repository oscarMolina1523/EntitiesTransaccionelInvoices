using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Data.Builders;
using Infrastructure.Endpoint.Data.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class GenericRepository<T> where T : BaseEntity
    {
        protected readonly ISqlCommandOperationBuilder operationBuilder;
        protected readonly ISqlDbConnection sqlDbConnection;

        public GenericRepository(ISqlDbConnection sqlDbConnection, ISqlCommandOperationBuilder operationBuilder)
        {
            this.operationBuilder = operationBuilder;
            this.sqlDbConnection = sqlDbConnection;
        }

        public virtual Task<DataTable> GetDataTableAsync()
        {
            SqlCommand readCommand = operationBuilder.Initialize<T>()
                .WithOperation(SqlReadOperation.Select)
                .BuildReader();
            return sqlDbConnection.ExecuteQueryCommandAsync(readCommand);
        }

        public virtual Task<DataTable> GetDataTableByIdAsync(Guid id)
        {
            SqlCommand readCommand = operationBuilder.Initialize<T>()
                .WithOperation(SqlReadOperation.SelectById)
                .WithId(id)
                .BuildReader();
            return sqlDbConnection.ExecuteQueryCommandAsync(readCommand);
        }

        public virtual async Task CreateAsync(T entity)
        {
            SqlCommand writeCommand = operationBuilder.From(entity)
                .WithOperation(SqlWriteOperation.Create)
                .BuildWritter();
            await sqlDbConnection.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            SqlCommand writeCommand = operationBuilder.From(entity)
                .WithOperation(SqlWriteOperation.Update)
                .BuildWritter();
            await sqlDbConnection.ExecuteNonQueryCommandAsync(writeCommand);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            SqlCommand writeCommand = operationBuilder.From(entity)
                .WithOperation(SqlWriteOperation.Delete)
                .BuildWritter();
            await sqlDbConnection.ExecuteNonQueryCommandAsync(writeCommand);
        }
    }
}
