using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using Domain.Endpoint.Interfaces.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly IInvoicesRepository invoicesRepository;
        private readonly IProductDetailsRepository productDetailsRepository;
        private readonly IDishesRepository dishesRepository;

        public InvoicesService(IInvoicesRepository invoicesRepository, IProductDetailsRepository productDetailsRepository, IDishesRepository dishesRepository)
        {
            this.invoicesRepository = invoicesRepository;
            this.productDetailsRepository = productDetailsRepository;
            this.dishesRepository = dishesRepository;
        }

        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            Invoice newInvoice = Clone(invoice);
            var tempDetails = newInvoice.InvoiceDetails;
            newInvoice.ClearDetail();

            await invoicesRepository.CreateAsync(newInvoice);
            return newInvoice;
        }

        public Task DeleteAsync(Invoice invoice)
        {
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Invoice invoice)
        {
            return Task.CompletedTask;
        }

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
