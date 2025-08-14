using Microsoft.AspNetCore.Mvc;
using SmartHRM.Application.Interfaces;
using SmartHRM.Core.Entities;

namespace SmartHRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationRepository _repo;

        public OrganizationsController(IOrganizationRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Organizations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetAll()
        {
            var organizations = await _repo.GetAllAsync();
            return Ok(organizations);
        }

        // GET: api/Organizations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetById(int id)
        {
            var organization = await _repo.GetByIdAsync(id);
            if (organization == null)
                return NotFound();

            return Ok(organization);
        }

        // POST: api/Organizations
        [HttpPost]
        public async Task<ActionResult<Organization>> Create([FromBody] Organization model)
        {
            var newOrg = new Organization
            {
                OrganizationName = model.OrganizationName,
                OrganizationCode = model.OrganizationCode,
                IsActive = model.IsActive
            };

            var createdOrg = await _repo.AddAsync(newOrg);
            return CreatedAtAction(nameof(GetById), new { id = createdOrg.OrganizationId }, createdOrg);
        }

        // PUT: api/Organizations/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Organization>> Update(int id, [FromBody] Organization model)
        {
            var existingOrg = await _repo.GetByIdAsync(id);
            if (existingOrg == null)
                return NotFound();

            existingOrg.OrganizationName = model.OrganizationName;
            existingOrg.OrganizationCode = model.OrganizationCode;
            existingOrg.IsActive = model.IsActive;

            var updatedOrg = await _repo.UpdateAsync(existingOrg);
            return Ok(updatedOrg);
        }

        // DELETE: api/Organizations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repo.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
