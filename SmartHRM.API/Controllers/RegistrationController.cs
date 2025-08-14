using Microsoft.AspNetCore.Mvc;
using SmartHRM.Application.Interfaces;
using SmartHRM.Application.DTOs;
using SmartHRM.Core.Entities;

namespace SmartHRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly IRoleRepository _roles;
        private readonly IOrganizationRepository _orgs;

        public RegistrationController(IUserRepository users, IRoleRepository roles, IOrganizationRepository orgs)
        {
            _users = users;
            _roles = roles;
            _orgs = orgs;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _users.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
        {
            if (dto.OrganizationId == null)
                return BadRequest("OrganizationId is required.");

            // Validate Role
            var role = await _roles.GetByIdAsync(dto.RoleId);
            if (role is null)
                return BadRequest("Role not found.");

            // Validate Organization
            var org = await _orgs.GetByIdAsync(dto.OrganizationId.Value);
            if (org is null)
                return BadRequest("Organization not found.");

            var entity = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.UserName,
                RoleId = dto.RoleId,
                OrganizationId = dto.OrganizationId.Value, // fixed: nullable to int
                IsActive = true
            };

            var created = await _users.AddAsync(entity);
            return Ok(created);
        }
    }
}
