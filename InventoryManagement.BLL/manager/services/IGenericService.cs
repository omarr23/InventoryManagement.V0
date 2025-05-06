using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO;

namespace InventoryManagement.BLL.manager.services
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<PaginatedResultDto<T>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? filter = null,
            string? orderBy = null);
    }
}
