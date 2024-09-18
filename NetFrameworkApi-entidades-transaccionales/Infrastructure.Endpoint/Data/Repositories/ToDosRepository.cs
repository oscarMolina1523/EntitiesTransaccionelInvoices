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
    public class ToDosRepository : GenericRepository<ToDo>, IToDosRepository
    {
        public ToDosRepository(ISqlDbConnection sqlDbConnection, ISqlCommandOperationBuilder operationBuilder)
            : base(sqlDbConnection, operationBuilder) { }

        public async Task<List<ToDo>> GetAsync()
        {
            DataTable dataTable = await GetDataTableAsync();
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();
        }
        
        public async Task<ToDo> GetByIdAsync(Guid id)
        {
            DataTable dataTable = await GetDataTableByIdAsync(id);
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .FirstOrDefault();
        }

        private ToDo MapEntityFromDataRow(DataRow row)
        {
            ToDo toDo = new ToDo
            {
                Id = sqlDbConnection.GetDataRowValue<Guid>(row, "Id"),
                Title = sqlDbConnection.GetDataRowValue<string>(row, "Title"),
                Description = sqlDbConnection.GetDataRowValue<string>(row, "Description"),
                Done = sqlDbConnection.GetDataRowValue<bool>(row, "Done"),
                Status = (ToDoStatus)Enum.Parse(typeof(ToDoStatus), sqlDbConnection.GetDataRowValue<string>(row, "Status")),
                CreatedAt = sqlDbConnection.GetDataRowValue<DateTime>(row, "CreatedAt"),
                StartedAt = sqlDbConnection.GetDataRowValue<DateTime?>(row, "StartedAt"),
                UpdatedAt = sqlDbConnection.GetDataRowValue<DateTime?>(row, "UpdatedAt"),
            };

            return toDo;
        }
    }
}
