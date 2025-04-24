using InventoryManagement.BLL.DTO;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Repository.CompanyRepository;
using InventoryManagement.BLL.manager.company;
using InventoryManagement.BLL.DTO.companyDTO;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies.Select(c => new CompanyDto
        {
            CompanyId = c.CompanyId,
            CompanyName = c.CompanyName,
            Address = c.Address,
            ContactInfo = c.ContactInfo
        });
    }

    public async Task<CompanyDto?> GetCompanyByIdAsync(int id)
    {
        var c = await _companyRepository.GetByIdAsync(id);
        if (c == null) return null;

        return new CompanyDto
        {
            CompanyId = c.CompanyId,
            CompanyName = c.CompanyName,
            Address = c.Address,
            ContactInfo = c.ContactInfo
        };
    }

    public async Task CreateCompanyAsync(CreateCompanyDto dto)
    {
        var company = new Company
        {
            CompanyName = dto.CompanyName,
            Address = dto.Address,
            ContactInfo = dto.ContactInfo
        };

        await _companyRepository.AddAsync(company);
    }

    public async Task UpdateCompanyAsync(CompanyDto dto)
    {
        var existing = await _companyRepository.GetByIdAsync(dto.CompanyId);
        if (existing == null) return;

        existing.CompanyName = dto.CompanyName;
        existing.Address = dto.Address;
        existing.ContactInfo = dto.ContactInfo;
        existing.UpdatedAt = DateTime.UtcNow;

        await _companyRepository.UpdateAsync(existing);
    }

    public async Task DeleteCompanyAsync(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company != null)
        {
            await _companyRepository.DeleteAsync(company);
        }
    }
}
