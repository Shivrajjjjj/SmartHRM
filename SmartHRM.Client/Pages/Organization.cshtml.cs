using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using SmartHRM.Application.DTOs;   // For OrganizationDto
using SmartHRM.Core.Entities;

public class OrganizationModel : PageModel
{
    private readonly HttpClient _http;

    public OrganizationModel(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("SmartHRMApi");
    }

    [BindProperty]
    public string OrganizationName { get; set; }

    [BindProperty]
    public string OrganizationCode { get; set; }

    [BindProperty]
    public bool IsActive { get; set; }

    public List<OrganizationDto> Organizations { get; set; } = new();

    public async Task OnGetAsync()
    {
        Organizations = await _http.GetFromJsonAsync<List<OrganizationDto>>("api/Organizations") ?? new();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var newOrg = new OrganizationDto
        {
            OrganizationName = OrganizationName,
            OrganizationCode = OrganizationCode,
            IsActive = IsActive
        };

        var res = await _http.PostAsJsonAsync("api/Organizations", newOrg);

        if (res.IsSuccessStatusCode)
        {
            return RedirectToPage(); // refresh the page and load updated list
        }

        ModelState.AddModelError("", "Error saving organization");
        await OnGetAsync();
        return Page();
    }
}
