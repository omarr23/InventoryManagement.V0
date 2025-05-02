using Microsoft.AspNetCore.Http;

namespace InventoryManagement.BLL.Exceptions
{
    public class NotFoundException : BaseAppException
    {
        public NotFoundException(string message)
            : base(message, StatusCodes.Status404NotFound, "NOT_FOUND")
        {
        }
    }
}
