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
    public class ProductDetailsRepository : GenericRepository<ProductDetail>, IProductDetailsRepository
    {
        public ProductDetailsRepository(ISqlDbConnection sqlDbConnection, ISqlCommandOperationBuilder operationBuilder) : base(sqlDbConnection, operationBuilder)
        {
        }

        public async Task<List<ProductDetail>> GetAsync()
        {
            DataTable dataTable = await GetDataTableAsync();
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();
        }

        public async Task<ProductDetail> GetByIdAsync(Guid id)
        {
            DataTable dataTable = await GetDataTableByIdAsync(id);
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .FirstOrDefault();
        }

        private ProductDetail MapEntityFromDataRow(DataRow row)
        {
            ProductDetail productDetail = new ProductDetail
            {
                Id = sqlDbConnection.GetDataRowValue<Guid>(row, "Id"),
                LotNumber = sqlDbConnection.GetDataRowValue<int>(row, "LotNumber"),
                Price = sqlDbConnection.GetDataRowValue<decimal>(row, "Price"),
                Quantity = sqlDbConnection.GetDataRowValue<int>(row, "Quantity"),
                Notes = sqlDbConnection.GetDataRowValue<string>(row, "Notes"),
                ExpiryDate = sqlDbConnection.GetDataRowValue<DateTime?>(row, "ExpiryDate"),
                ProductId = sqlDbConnection.GetDataRowValue<Guid>(row, "ProductId"),
                CreatedAt = sqlDbConnection.GetDataRowValue<DateTime>(row, "CreatedAt"),
                CreatedBy = sqlDbConnection.GetDataRowValue<Guid>(row, "CreatedBy"),
                UpdatedAt = sqlDbConnection.GetDataRowValue<DateTime?>(row, "UpdatedAt"),
                UpdatedBy = sqlDbConnection.GetDataRowValue<Guid?>(row, "UpdatedBy"),
            };

            return productDetail;
        }
    }
}
