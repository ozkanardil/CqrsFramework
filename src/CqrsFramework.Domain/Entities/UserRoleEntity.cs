using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Domain.Entities
{
    public class UserRoleEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
