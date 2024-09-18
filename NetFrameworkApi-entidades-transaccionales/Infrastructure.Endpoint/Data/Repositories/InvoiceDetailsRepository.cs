using Domain.Endpoint.Entities;
using Infrastructure.Endpoint.Data.Interfaces;

namespace Infrastructure.Endpoint.Data.Repositories
{
    public class InvoiceDetailsRepository : GenericRepository<InvoiceDetail>
    {
        public InvoiceDetailsRepository(ISqlDbConnection sqlDbConnection, ISqlCommandOperationBuilder operationBuilder) : base(sqlDbConnection, operationBuilder)
        {
        }
    }
}
