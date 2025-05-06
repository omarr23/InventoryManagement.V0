using Microsoft.AspNetCore.Http;

namespace InventoryManagement.BLL.Exceptions
{
    public class UnauthorizedException : BaseAppException
    {
        public UnauthorizedException(string message)
            : base(message, StatusCodes.Status401Unauthorized, "UNAUTHORIZED")
        {
        }
    }
} 