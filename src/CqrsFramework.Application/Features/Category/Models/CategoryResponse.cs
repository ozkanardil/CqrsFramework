using CqrsFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.Category.Models
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
