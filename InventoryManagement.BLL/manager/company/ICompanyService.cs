
using  InventoryManagement.BLL.DTO.companyDTO;
using InventoryManagement.BLL.Helper;

namespace InventoryManagement.BLL.manager.company
{
  public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
    Task<CompanyDto?> GetCompanyByIdAsync(int id);
    Task CreateCompanyAsync(CreateCompanyDto dto);
    Task UpdateCompanyAsync(CompanyDto dto);
    Task DeleteCompanyAsync(int id);
}
}

//Interfaces decouple the layers from specific implementations.