using System;

namespace Domain.Endpoint.Entities
{
    public class InvoiceDetail : BaseEntity, IHaveCreationData
    {
        public Guid InvoiceId { get; set; }
        public Guid? ProductDetailId { get; set; }
        public Guid? DishId { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid GetItemId()
        {
            switch (ItemType)
            {
                case BaseItem.Dish: return DishId ?? Guid.Empty;
                case BaseItem.SingleProduct: return ProductDetailId ?? Guid.Empty;
                default: return Guid.Empty;
            }
        }

        public void SetItemId(Guid id)
        {
            if (string.IsNullOrEmpty(ItemType)) throw new ArgumentNullException(nameof(ItemType));

            switch (ItemType)
            {
                case BaseItem.Dish: DishId = id; return;
                case BaseItem.SingleProduct: ProductDetailId = id; return;
            }
        }

        public virtual Invoice Invoice { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
        public virtual Dish Dish { get; set; }
    }
}