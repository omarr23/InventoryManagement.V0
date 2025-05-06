using InventoryManagement.BLL.DTO;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.BLL.manager.services
{
    public static class PaginationService
    {
        public static async Task<PaginatedResultDto<T>> GetPaginatedResultAsync<T>(
            IQueryable<T> query,
            int pageNumber = 1,
            int pageSize = 10,
            string? filter = null,
            string? orderBy = null)
        {
            // Apply filtering if provided
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(filter);
            }

            // Apply sorting if provided
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Calculate pagination
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages));

            // Apply pagination
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Create metadata
            var metadata = new PaginationMetadataDto
            {
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return new PaginatedResultDto<T>(items, metadata);
        }
    }
} 