namespace SmartHRM.Application.DTOs;

public class UserRegistrationDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public int? OrganizationId { get; set; }
}
