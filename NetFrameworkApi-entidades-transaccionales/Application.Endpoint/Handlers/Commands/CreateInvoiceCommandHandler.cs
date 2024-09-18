using Application.Endpoint.Commands;
using Domain.Endpoint.DTOs;
using Domain.Endpoint.Entities;
using Domain.Endpoint.Interfaces.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Endpoint.Handlers.Commands
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Invoice>
    {
        private readonly IInvoicesService invoicesService;

        public CreateInvoiceCommandHandler(IInvoicesService invoicesService)
        {
            this.invoicesService = invoicesService;
        }

        public Task<Invoice> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = new Invoice(Guid.NewGuid(), request.UserId);
            invoice.CustomerName = request.InvoiceDto.Customer;
            invoice.Discount = request.InvoiceDto.Discount;
            invoice.Notes = request.InvoiceDto.Notes;
            invoice.Number = request.InvoiceDto.Number;
            invoice.CreatedAt = DateTime.Now;

            foreach (var itemDto in request.InvoiceDto.Items)
            {
                var item = GetItem(itemDto, request.UserId);
                invoice.AddDetail(item, itemDto.Quantity, itemDto.Discount);
            }

            return invoicesService.CreateAsync(invoice);
        }

        private BaseItem GetItem(CreateInvoiceDetailDto detail, Guid userId)
        {
            string type = detail.ItemType == ItemType.Dish
                ? BaseItem.Dish
                : BaseItem.SingleProduct;
            var item = new BaseItem
            {
                Id = detail.ItemId,
                Price = 0,
                CreatedBy = userId,
                CreatedAt = DateTime.Now,
            };

            item.SetItemType(type);
            return item;
        }
    }
}
