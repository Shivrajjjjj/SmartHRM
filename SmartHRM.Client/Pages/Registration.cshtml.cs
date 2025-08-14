using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using SmartHRM.Application.DTOs; // ✅ Using your shared DTOs

public class RegistrationModel : PageModel
{
    private readonly HttpClient _httpClient;

    public RegistrationModel(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("MyAPI");
    }

    [BindProperty] public string FirstName { get; set; } = "";
    [BindProperty] public string LastName { get; set; } = "";
    [BindProperty] public string Email { get; set; } = "";
    [BindProperty] public string UserName { get; set; } = "";
    [BindProperty] public int SelectedRoleId { get; set; }
    [BindProperty] public int SelectedOrgId { get; set; }

    public List<RoleDto> Roles { get; set; } = new();
    public List<OrganizationDto> Organizations { get; set; } = new();
    public List<UserDto> Users { get; set; } = new();

    public async Task OnGetAsync()
    {
        Roles = await _httpClient.GetFromJsonAsync<List<RoleDto>>("api/roles") ?? new();
        Organizations = await _httpClient.GetFromJsonAsync<List<OrganizationDto>>("api/Organizations") ?? new();
        Users = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/registration/users") ?? new();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var newUser = new UserRegistrationDto
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            UserName = UserName,
            RoleId = SelectedRoleId,
            OrganizationId = SelectedOrgId
        };

        await _httpClient.PostAsJsonAsync("api/registration", newUser);
        return RedirectToPage();
    }

    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = "";
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string UserName { get; set; } = "";
        public RoleDto? Role { get; set; }
        public OrganizationDto? Organization { get; set; } // ✅ Now uses shared OrganizationDto with OrganizationName
    }

    public class UserRegistrationDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string UserName { get; set; } = "";
        public int RoleId { get; set; }
        public int? OrganizationId { get; set; }
    }
}
