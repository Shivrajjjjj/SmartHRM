namespace SmartHRM.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        // ✅ Added Organization relationship
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
