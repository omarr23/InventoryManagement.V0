namespace InventoryManagement.BLL.DTO
{
    public class PaginatedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PaginationMetadataDto Metadata { get; set; }

        public PaginatedResultDto(IEnumerable<T> items, PaginationMetadataDto metadata)
        {
            Items = items;
            Metadata = metadata;
        }
    }
} 