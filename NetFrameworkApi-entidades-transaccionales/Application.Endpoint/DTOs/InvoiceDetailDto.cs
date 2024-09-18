using System;

namespace Domain.Endpoint.DTOs
{
    public enum ItemType
    {
        Dish,
        SingleProduct
    }

    public class CreateInvoiceDetailDto
    {
        public Guid ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
