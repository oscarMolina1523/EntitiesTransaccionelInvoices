using System.Collections.Generic;

namespace Domain.Endpoint.DTOs
{
    public class CreateInvoiceDto
    {
        public string Customer { get; set; }
        public string Number { get; set; }
        public string Notes { get; set; }
        public decimal Discount { get; set; }

        public List<CreateInvoiceDetailDto> Items { get; set; }
    }
}
