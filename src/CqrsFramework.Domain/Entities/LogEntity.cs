using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Domain.Entities
{
    public class LogEntity
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string ip { get; set; }
        public DateTime logdate { get; set; }
        public string controller { get; set; }
        public string parameters { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string type { get; set; }
    }
}
