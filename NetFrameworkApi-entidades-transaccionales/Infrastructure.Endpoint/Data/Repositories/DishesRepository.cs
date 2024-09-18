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
    public class DishesRepository : GenericRepository<Dish>, IDishesRepository
    {
        public DishesRepository(ISqlDbConnection sqlDbConnection, ISqlCommandOperationBuilder operationBuilder) : base(sqlDbConnection, operationBuilder)
        {
        }

        public async Task<List<Dish>> GetAsync()
        {
            DataTable dataTable = await GetDataTableAsync();
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();
        }

        public async Task<Dish> GetByIdAsync(Guid id)
        {
            DataTable dataTable = await GetDataTableByIdAsync(id);
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .FirstOrDefault();
        }

        private Dish MapEntityFromDataRow(DataRow row)
        {
            Dish dish = new Dish
            {
                Id = sqlDbConnection.GetDataRowValue<Guid>(row, "Id"),
                Name = sqlDbConnection.GetDataRowValue<string>(row, "Name"),
                Price = sqlDbConnection.GetDataRowValue<decimal>(row, "Price"),
                Description = sqlDbConnection.GetDataRowValue<string>(row, "Description"),
                CreatedAt = sqlDbConnection.GetDataRowValue<DateTime>(row, "CreatedAt"),
                CreatedBy = sqlDbConnection.GetDataRowValue<Guid>(row, "CreatedBy"),
                UpdatedAt = sqlDbConnection.GetDataRowValue<DateTime?>(row, "UpdatedAt"),
                UpdatedBy = sqlDbConnection.GetDataRowValue<Guid?>(row, "UpdatedBy"),
            };

            return dish;
        }
    }
}
