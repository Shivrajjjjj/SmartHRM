using Microsoft.AspNetCore.Mvc;
using SmartHRM.Application.Interfaces;
using SmartHRM.Application.DTOs;
using SmartHRM.Core.Entities;

namespace SmartHRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _repo;
    public RolesController(IRoleRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
    {
        var roles = await _repo.GetAllAsync();
        return Ok(roles.Select(r => new RoleDto { RoleId = r.RoleId, RoleName = r.RoleName, IsActive = r.IsActive }));
    }

    [HttpPost]
    public async Task<ActionResult<RoleDto>> Post([FromBody] RoleDto dto)
    {
        var role = new Role { RoleName = dto.RoleName, IsActive = dto.IsActive };
        var created = await _repo.AddAsync(role);
        return CreatedAtAction(nameof(Get), new { id = created.RoleId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] RoleDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return NotFound();
        existing.RoleName = dto.RoleName;
        existing.IsActive = dto.IsActive;
        await _repo.UpdateAsync(existing);
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _repo.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
