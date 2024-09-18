using Domain.Endpoint.DTOs;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Domain.Endpoint.Services
{
    public class FacturaService
    {
        private readonly IInvoicesRepository invoicesRepository;
        private readonly IProductDetailsRepository productDetailsRepository;
        private readonly IDishesRepository dishesRepository;

        public FacturaService(IInvoicesRepository invoicesRepository, IProductDetailsRepository productDetailsRepository, IDishesRepository dishesRepository)
        {
            this.invoicesRepository = invoicesRepository;
            this.productDetailsRepository = productDetailsRepository;
            this.dishesRepository = dishesRepository;
        }

        public async Task<Invoice> CreateAsync(CrearFacturaDto facturaDto, Guid UserId)
        {
            Invoice factura = new Invoice(Guid.NewGuid(), UserId)
            {
                CustomerName = facturaDto.Customer,
                Discount = facturaDto.Discount,
                Number = facturaDto.Number,
                Notes = facturaDto.Notes,
                CreatedAt = DateTime.Now,
            };

            foreach (CrearDetalleDto item in facturaDto.Items)
            {
                ProductDetail detalleProducto = await productDetailsRepository.GetByIdAsync(item.ItemId);
                if (detalleProducto is null) throw new Exception("El producto no existe");

                factura.AddDetail(detalleProducto, item.Quantity, item.Discount);
                detalleProducto.Quantity -= item.Quantity;
                await productDetailsRepository.UpdateAsync(detalleProducto);
            }

            await invoicesRepository.CreateAsync(factura);
            return factura;
        }
    }
}
