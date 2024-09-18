using Domain.Endpoint.DTOs;
using Domain.Endpoint.Entities;
using MediatR;
using System;

namespace Application.Endpoint.Commands
{
    public class CreateInvoiceCommand : IRequest<Invoice>
    {
        public CreateInvoiceDto InvoiceDto { get; set; }
        public Guid UserId { get; set; }
        public CreateInvoiceCommand(CreateInvoiceDto invoiceDto, Guid userId)
        {
            InvoiceDto = invoiceDto;
            UserId = userId;
        }
    }
}
