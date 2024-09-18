using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Infrastructure.Endpoint.Data.Builders;
using Infrastructure.Endpoint.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class InvoicesRepository : GenericRepository<Invoice>, IInvoicesRepository
    {
        public InvoicesRepository(ISqlDbConnection sqlDbConnection, ISqlCommandOperationBuilder operationBuilder) : base(sqlDbConnection, operationBuilder)
        {
        }

        public override async Task CreateAsync(Invoice invoice)
        {
            string query = "INSERT INTO [dbo].[Invoices] (,,,,) VALUES (@Id, @Quantity, ...)";
            SqlCommand facturaComando = new SqlCommand(query);
            List<SqlParameter> parameters = ObtenerFacturaParametros(invoice);
            facturaComando.Parameters.AddRange(parameters.ToArray());

            StringBuilder detalleQuery = new StringBuilder("INSER INTO [dbo].[InvoiceDetails] (,,,,)");
            int counter = 0;
            List<SqlParameter> detalleParameters = new List<SqlParameter>();

            foreach (var detalle in invoice.InvoiceDetails.ToList())
            {
                counter++;
                detalleQuery.Append($" VALUES (@Quantity{counter}, @Total{counter}");
                SqlParameter param = new SqlParameter()
                {
                    IsNullable = false,
                    DbType = DbType.Decimal,
                    ParameterName = $"@Total{counter}",
                    Value = detalle.Total,
                };

                SqlParameter param1 = new SqlParameter()
                {
                    IsNullable = false,
                    DbType = DbType.Decimal,
                    ParameterName = $"@Quantity{counter}",
                    Value = detalle.Total,
                };

                detalleParameters.Add(param);
                detalleParameters.Add(param1);
            }

            SqlCommand detalleComando = new SqlCommand(detalleQuery.ToString());
            detalleComando.Parameters.AddRange(detalleParameters.ToArray());
            SqlCommand[] sqlCommands = new SqlCommand[]
            {
                facturaComando,
                detalleComando
            };

            await sqlDbConnection.RunTransactionAsync(sqlCommands);
        }

        private List<SqlParameter> ObtenerFacturaParametros(Invoice invoice)
        {
            return new List<SqlParameter>
            {
                new SqlParameter {
                    ParameterName = "@Numero",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = invoice.Number
                },
                new SqlParameter
                {
                    ParameterName = "@FechaActualizacion",
                    SqlDbType = SqlDbType.DateTime,
                    Value = ObtenerValor(invoice.UpdatedAt)
                },
                new SqlParameter
                {
                    ParameterName = "@ActualizadoPor",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = ObtenerValor(invoice.UpdatedBy)
                }
            };
        }

        private object ObtenerValor(object valor)
        {
            return valor is null ? DBNull.Value : valor;
        } 

        public async Task<List<Invoice>> GetAsync()
        {
            DataTable dataTable = await GetDataTableAsync();
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .ToList();
        }

        public async Task<Invoice> GetByIdAsync(Guid id)
        {
            DataTable dataTable = await GetDataTableByIdAsync(id);
            return dataTable.AsEnumerable()
                .Select(MapEntityFromDataRow)
                .FirstOrDefault();
        }

        private Invoice MapEntityFromDataRow(DataRow row)
        {
            Invoice invoice = new Invoice
            {
                Id = sqlDbConnection.GetDataRowValue<Guid>(row, "Id"),
                Number = sqlDbConnection.GetDataRowValue<string>(row, "Number"),
                CustomerName = sqlDbConnection.GetDataRowValue<string>(row, "CustomerName"),
                Notes = sqlDbConnection.GetDataRowValue<string>(row, "Notes"),
                Quantity = sqlDbConnection.GetDataRowValue<int>(row, "Quantity"),
                Subtotal = sqlDbConnection.GetDataRowValue<decimal>(row, "Subtotal"),
                Discount = sqlDbConnection.GetDataRowValue<decimal>(row, "Discount"),
                Total = sqlDbConnection.GetDataRowValue<decimal>(row, "Total"),
                CreatedAt = sqlDbConnection.GetDataRowValue<DateTime>(row, "CreatedAt"),
                CreatedBy = sqlDbConnection.GetDataRowValue<Guid>(row, "CreatedBy"),
                UpdatedAt = sqlDbConnection.GetDataRowValue<DateTime?>(row, "UpdatedAt"),
                UpdatedBy = sqlDbConnection.GetDataRowValue<Guid?>(row, "UpdatedBy"),
            };

            return invoice;
        }
    }
}
