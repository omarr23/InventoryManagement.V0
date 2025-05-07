using Microsoft.AspNetCore.Mvc;
using InventoryManagement.BLL.DTO;
using InventoryManagement.BLL.DTO.companyDTO;
using InventoryManagement.BLL.manager.company;
using Microsoft.AspNetCore.Authorization;
namespace InventoryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]   // 🔐 Require authentication for all actions in this controller
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/company
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        // GET: api/company/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
                return NotFound(new { Message = $"Company with ID {id} not found." });

            return Ok(company);
        }

        // POST: api/company
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _companyService.CreateCompanyAsync(dto);
            return CreatedAtAction(nameof(GetAll), null); // you can replace with GetById if needed
        }

        // PUT: api/company
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CompanyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _companyService.UpdateCompanyAsync(dto);
            return NoContent();
        }

        // DELETE: api/company/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return NoContent();
        }
    }
}
