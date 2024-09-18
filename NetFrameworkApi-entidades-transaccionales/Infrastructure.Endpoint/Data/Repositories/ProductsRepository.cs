using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Infrastructure.Endpoint.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductsRepository(ISqlDbConnection sqlDbConnection, ISqlCommandOperationBuilder operationBuilder) : base(sqlDbConnection, operationBuilder)
        {
        }

        public async Task<List<Product>> GetAsync()
        {
            DataTable dataTable = await GetDataTableAsync();
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            DataTable dataTable = await GetDataTableByIdAsync(id);
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .FirstOrDefault();
        }

        private Product MapEntityFromDataRow(DataRow row)
        {
            Product product = new Product
            {
                Id = sqlDbConnection.GetDataRowValue<Guid>(row, "Id"),
                Name = sqlDbConnection.GetDataRowValue<string>(row, "Name"),
                Description = sqlDbConnection.GetDataRowValue<string>(row, "Description"),
                Active = sqlDbConnection.GetDataRowValue<bool>(row, "Active"),
                CreatedAt = sqlDbConnection.GetDataRowValue<DateTime>(row, "CreatedAt"),
                CreatedBy = sqlDbConnection.GetDataRowValue<Guid>(row, "CreatedBy"),
                UpdatedAt = sqlDbConnection.GetDataRowValue<DateTime?>(row, "UpdatedAt"),
                UpdatedBy = sqlDbConnection.GetDataRowValue<Guid?>(row, "UpdatedBy"),
            };

            return product;
        }
    }
}
