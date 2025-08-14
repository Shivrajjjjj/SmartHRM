using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace SmartHRM.Client.Pages
{
    public class RolesModel : PageModel
    {
        private readonly HttpClient _http;

        public RolesModel(IHttpClientFactory clientFactory)
        {
            _http = clientFactory.CreateClient("SmartHRMApi");
        }

        [BindProperty]
        public string RoleName { get; set; } = string.Empty;

        [BindProperty]
        public RoleDto Role { get; set; } = new();

        public List<RoleDto> Roles { get; set; } = new();

        public async Task OnGetAsync()
        {
            Roles = await _http.GetFromJsonAsync<List<RoleDto>>("api/roles") ?? new List<RoleDto>();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            var newRole = new RoleDto { RoleName = RoleName, IsActive = true };
            await _http.PostAsJsonAsync("api/roles", newRole);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int roleId)
        {
            var roleToUpdate = Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (roleToUpdate != null)
            {
                await _http.PutAsJsonAsync($"api/roles/{roleId}", roleToUpdate);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int roleId)
        {
            await _http.DeleteAsync($"api/roles/{roleId}");
            return RedirectToPage();
        }

        public class RoleDto
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public bool IsActive { get; set; }
        }
    }
}
