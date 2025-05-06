namespace InventoryManagement.BLL.DTO
{
    public class PaginatedResult<T>(IEnumerable<T> items, PaginationMetadata metadata)
  {
    public IEnumerable<T> Items { get; set; } = items;
    public PaginationMetadata Metadata { get; set; } = metadata;
  }
} 