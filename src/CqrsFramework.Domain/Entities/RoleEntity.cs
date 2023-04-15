
namespace CqrsFramework.Domain.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public virtual ICollection<UserRoleEntity> UserRole { get; set; }
    }

}
