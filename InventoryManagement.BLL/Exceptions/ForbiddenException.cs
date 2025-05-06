using Microsoft.AspNetCore.Http;

namespace InventoryManagement.BLL.Exceptions
{
    public class ForbiddenException : BaseAppException
    {
        public ForbiddenException(string message)
            : base(message, StatusCodes.Status403Forbidden, "FORBIDDEN")
        {
        }
    }
} 