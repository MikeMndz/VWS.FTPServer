using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("User_Role")]
    public class UserRole : BaseEntity
    {
        public int Id { get; set; }
        public int FkUser { get; set; }
        public int FkRole { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }

    }
}
