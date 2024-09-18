using System.Collections.Generic;

namespace Domain.Endpoint.Entities
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
