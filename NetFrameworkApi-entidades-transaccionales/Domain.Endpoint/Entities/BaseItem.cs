using Domain.Endpoint.Exceptions;
using System.Collections.Generic;

namespace Domain.Endpoint.Entities
{
    public class BaseItem : AuditableEntity
    {
        public const string Dish = "Dish";
        public const string SingleProduct = "SingleProduct";

        public decimal Price { get; set; }

        private string itemType = string.Empty;

        public virtual string GetItemType()
        {
            return itemType;
        }

        public virtual void SetItemType(string value)
        {
            ICollection<string> items = new List<string> { Dish, SingleProduct };
            if (!items.Contains(value)) throw new ItemTypeNotAllowedException(value);

            itemType = value;
        }
    }
}
