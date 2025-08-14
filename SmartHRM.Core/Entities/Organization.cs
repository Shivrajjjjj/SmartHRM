namespace SmartHRM.Core.Entities
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = null!;
        public string OrganizationCode { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}