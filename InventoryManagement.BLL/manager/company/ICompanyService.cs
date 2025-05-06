using InventoryManagement.BLL.DTO;
using InventoryManagement.BLL.DTO.companyDTO;

namespace InventoryManagement.BLL.manager.company
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<CompanyDto?> GetCompanyByIdAsync(int id);
        Task CreateCompanyAsync(CreateCompanyDto dto);
        Task UpdateCompanyAsync(CompanyDto dto);
        Task DeleteCompanyAsync(int id);
        Task<PaginatedResultDto<CompanyDto>> GetPaginatedCompaniesAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? filter = null,
            string? orderBy = null);
    }
}

//Interfaces decouple the layers from specific implementations.