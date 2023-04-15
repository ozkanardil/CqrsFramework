using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Domain.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Category_Id { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public virtual OrderItemEntity OrderItem { get; set; }
        public virtual ICollection<ShoppingCartEntity> ShoppingCart { get; set; }
    }
}
