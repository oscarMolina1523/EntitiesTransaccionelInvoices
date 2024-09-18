using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Data.Builders;
using System;
using System.Data.SqlClient;

namespace Infrastructure.Endpoint.Data.Interfaces
{
    public interface ISqlCommandOperationBuilder
    {
        IHaveSqlWriteOperation From<TEntity>(TEntity entity) where TEntity : BaseEntity;
        IHaveSqlReadOperation Initialize<TEntity>() where TEntity : BaseEntity;
    }

    public interface IHaveSqlWriteOperation
    {
        IExecuteWriteBuilder WithOperation(SqlWriteOperation operation);
    }

    public interface IHaveSqlReadOperation
    {
        IHavePrimaryKeyValue WithOperation(SqlReadOperation operation);
    }

    public interface IHavePrimaryKeyValue : IExecuteReadBuilder
    {
        IExecuteReadBuilder WithId(Guid id);
    }

    public interface IExecuteReadBuilder
    {
        SqlCommand BuildReader();
    }
    
    public interface IExecuteWriteBuilder
    {
        SqlCommand BuildWritter();
    }
}
