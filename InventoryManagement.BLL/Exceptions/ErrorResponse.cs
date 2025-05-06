namespace InventoryManagement.BLL.Exceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public required string Message { get; set; }
        public string? Detail { get; set; } // Optional, included only in Development
    }
}
