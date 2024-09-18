using System;
using System.Collections.Generic;

namespace Domain.Endpoint.Entities
{
    public class Invoice : AuditableEntity
    {
        public Invoice(Guid id, Guid createdBy)
        {
            Id = id;
            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
        }

        public Invoice() { }

        public string Number { get; set; }
        public string CustomerName { get; set; }
        public string Notes { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        public void AddDetail(BaseItem item, int quantity, decimal discount)
        {
            var detail = new InvoiceDetail
            {
                Id = Guid.NewGuid(),
                InvoiceId = Id,
                ItemType = item.GetItemType(),
                Quantity = quantity,
                Price = item.Price,
                Subtotal = quantity * item.Price,
                Discount = discount,
                Total = (quantity * item.Price) - discount,
                CreatedBy = CreatedBy,
                CreatedAt = DateTime.Now,
            };

            detail.SetItemId(item.Id);

            if (InvoiceDetails is null) { InvoiceDetails = new List<InvoiceDetail>(); }

            InvoiceDetails.Add(detail);

            Quantity++;
            Subtotal += detail.Total;
            Total = Subtotal - Discount;
        }

        public void ClearDetail()
        {
            if (InvoiceDetails is null) return;

            InvoiceDetails.Clear();

            Quantity = 0;
            Subtotal = 0;
            Total = 0;
        }
    }
}
