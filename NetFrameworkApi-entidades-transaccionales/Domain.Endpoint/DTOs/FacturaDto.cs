using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Endpoint.DTOs
{
    public class CrearFacturaDto
    {
        public string Customer { get; set; }
        public string Number { get; set; }
        public string Notes { get; set; }
        public decimal Discount { get; set; }

        public List<CrearDetalleDto> Items { get; set; }
    }

    public enum ItemType
    {
        Dish,
        SingleProduct
    }

    public class CrearDetalleDto
    {
        public Guid ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
