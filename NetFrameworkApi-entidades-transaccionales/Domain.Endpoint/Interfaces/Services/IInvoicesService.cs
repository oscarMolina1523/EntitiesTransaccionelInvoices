using Domain.Endpoint.Entities;
using System.Threading.Tasks;

namespace Domain.Endpoint.Interfaces.Services
{
    public interface IInvoicesService
    {
        Task<Invoice> CreateAsync(Invoice invoice);
        Task DeleteAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
    }
}