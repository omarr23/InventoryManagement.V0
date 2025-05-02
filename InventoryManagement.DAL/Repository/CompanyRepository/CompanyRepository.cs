using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.DAL.Repository.CompanyRepository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly InventoryDbContext _context;

        public CompanyRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Companies
                .Include(c => c.Users) //  eager load users
                .FirstOrDefaultAsync(c => c.CompanyId == id);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies
                .Include(c => c.Users) // optional
                .ToListAsync();
        }

        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Company company)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }
    }
}
