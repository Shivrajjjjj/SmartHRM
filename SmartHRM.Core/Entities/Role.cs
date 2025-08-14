namespace SmartHRM.Core.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
