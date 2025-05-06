using Microsoft.AspNetCore.Http;

namespace InventoryManagement.BLL.Exceptions
{
    public class ValidationException : BaseAppException
    {
        public ValidationException(string message, string[]? errors = null)
            : base(message, StatusCodes.Status400BadRequest, "VALIDATION_ERROR", errors)
        {
        }
    }
}
