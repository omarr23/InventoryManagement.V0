using InventoryManagement.DAL.Models;

namespace InventoryManagement.DAL.Repository.CompanyRepository
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(int id);
        Task<IEnumerable<Company>> GetAllAsync();
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(Company company);
    }
}
