using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Domain.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
