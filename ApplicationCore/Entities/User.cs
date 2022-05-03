using System;

namespace ApplicationCore.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MothersLastName { get; set; }
        public bool Enabled { get; set; }
        public bool Deleted { get; set; }
        public DateTime? LastAccess { get; set; }
    }
}
