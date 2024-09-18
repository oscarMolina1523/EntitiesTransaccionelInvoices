using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Data.Builders;
using Infrastructure.Endpoint.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace Infrastructure.Endpoint.Data.Services
{

    public class EntitiesService : IEntitiesService
    {
        private Dictionary<Type, SqlEntitySettings> entities = new Dictionary<Type, SqlEntitySettings>();
        private readonly ISqlEntitySettingsBuilder builder;

        public EntitiesService(ISqlEntitySettingsBuilder builder)
        {
            this.builder = builder;
            BuildEntities();
        }

        public SqlEntitySettings GetSettings<T>() where T : BaseEntity
        {
            //if (typeof(ToDo).Equals(typeof(T))) return BuildToDoSettings();
            if (!entities.ContainsKey(typeof(T))) throw new ArgumentOutOfRangeException(nameof(T), "Not Mapped Entity");

            return entities[typeof(T)];
        }

        private void BuildEntities()
        {
            SqlEntitySettings toDoSettings = BuildToDoSettings();
            SqlEntitySettings productSettings = BuildProductSettings();
            SqlEntitySettings productDetailSettings = BuildProductDetailSettings();
            SqlEntitySettings dishSettings = BuildDishSettings();
            SqlEntitySettings invoiceSettings = BuildInvoiceSettings();
            SqlEntitySettings invoiceDetailSettings = BuildInvoiceDetailSettings();

            entities.Add(typeof(ToDo), toDoSettings);
            entities.Add(typeof(Product), productSettings);
            entities.Add(typeof(ProductDetail), productDetailSettings);
            entities.Add(typeof(Dish), dishSettings);
            entities.Add(typeof(Invoice), invoiceSettings);
            entities.Add(typeof(InvoiceDetail), invoiceDetailSettings);
        }

        private SqlEntitySettings BuildToDoSettings()
        {
            return builder.Entity<ToDo>(entity =>
            {
                entity.Table("ToDos");
                entity.Property(property => property.Id)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AsPrimaryKey()
                    .AddProperty();
                entity.Property(property => property.Title)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();

                entity.Property(property => property.Description)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();

                entity.Property(property => property.Status)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();

                entity.Property(property => property.Done)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.Bit)
                    .AddProperty();

                entity.Property(property => property.CreatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();

                entity.Property(property => property.StartedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();

                entity.Property(property => property.UpdatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
            }).Build();
        }

        private SqlEntitySettings BuildProductSettings()
        {
            return builder.Entity<Product>(entity =>
            {
                entity.Table("Products");
                entity.Property(property => property.Id)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AsPrimaryKey()
                    .AddProperty();
                entity.Property(property => property.Name)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.Description)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.Active)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.Bit)
                    .AddProperty();
                entity.Property(property => property.CreatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.CreatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.UpdatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.UpdatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
            }).Build();
        }

        private SqlEntitySettings BuildProductDetailSettings()
        {
            return builder.Entity<ProductDetail>(entity =>
            {
                entity.Table("ProductDetails");
                entity.Property(property => property.Id)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AsPrimaryKey()
                    .AddProperty();
                entity.Property(property => property.Notes)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.LotNumber)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.Int)
                    .AsComputed()
                    .AddProperty();
                entity.Property(property => property.ProductId)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.Price)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.Decimal)
                    .AddProperty();
                entity.Property(property => property.Quantity)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.Int)
                    .AddProperty();
                entity.Property(property => property.ExpiryDate)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.CreatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.CreatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.UpdatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.UpdatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
            }).Build();
        }

        private SqlEntitySettings BuildDishSettings()
        {
            return builder.Entity<Dish>(entity =>
            {
                entity.Table("Dishes");
                entity.Property(property => property.Id)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AsPrimaryKey()
                    .AddProperty();
                entity.Property(property => property.Name)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.Description)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.Price)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.Decimal)
                    .AddProperty();
                entity.Property(property => property.CreatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.CreatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.UpdatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.UpdatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
            }).Build();
        }

        private SqlEntitySettings BuildInvoiceSettings()
        {
            return builder.Entity<Invoice>(entity =>
            {
                entity.Table("Invoices");
                entity.Property(property => property.Id)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AsPrimaryKey()
                    .AddProperty();
                entity.Property(property => property.CustomerName)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.Notes)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.Number)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.Quantity)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.Int)
                    .AddProperty();
                entity.Property(property => property.Discount)
                   .SetDefaultName()
                   .WithSqlDbType(SqlDbType.Decimal)
                   .AddProperty();
                entity.Property(property => property.Subtotal)
                   .SetDefaultName()
                   .WithSqlDbType(SqlDbType.Decimal)
                   .AddProperty();
                entity.Property(property => property.Total)
                   .SetDefaultName()
                   .WithSqlDbType(SqlDbType.Decimal)
                   .AddProperty();
                entity.Property(property => property.CreatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.CreatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.UpdatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.UpdatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
            }).Build();
        }

        private SqlEntitySettings BuildInvoiceDetailSettings()
        {
            return builder.Entity<InvoiceDetail>(entity =>
            {
                entity.Table("InvoiceDetails");
                entity.Property(property => property.Id)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AsPrimaryKey()
                    .AddProperty();
                entity.Property(property => property.InvoiceId)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.ProductDetail)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.DishId)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.ItemType)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.NVarChar)
                    .AddProperty();
                entity.Property(property => property.Price)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
                entity.Property(property => property.Quantity)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.Int)
                    .AddProperty();
                entity.Property(property => property.Discount)
                   .SetDefaultName()
                   .WithSqlDbType(SqlDbType.Decimal)
                   .AddProperty();
                entity.Property(property => property.Subtotal)
                   .SetDefaultName()
                   .WithSqlDbType(SqlDbType.Decimal)
                   .AddProperty();
                entity.Property(property => property.Total)
                   .SetDefaultName()
                   .WithSqlDbType(SqlDbType.Decimal)
                   .AddProperty();
                entity.Property(property => property.CreatedAt)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.DateTime)
                    .AddProperty();
                entity.Property(property => property.CreatedBy)
                    .SetDefaultName()
                    .WithSqlDbType(SqlDbType.UniqueIdentifier)
                    .AddProperty();
            }).Build();
        }
    }
}
