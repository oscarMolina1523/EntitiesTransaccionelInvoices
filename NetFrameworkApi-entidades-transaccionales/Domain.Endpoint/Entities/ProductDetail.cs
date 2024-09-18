using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Entities
{
    public class ProductDetail : BaseItem
    {
        public int LotNumber { get; set; }
        public Guid ProductId { get; set; }
        //public decimal Price { get; set; }
        public string Notes { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
