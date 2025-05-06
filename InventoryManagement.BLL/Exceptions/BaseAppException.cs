namespace InventoryManagement.BLL.Exceptions
{
    public abstract class BaseAppException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }
        public string[] Errors { get; }

        protected BaseAppException(string message, int statusCode, string errorCode, string[]? errors = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Errors = errors ?? Array.Empty<string>();
        }
    }
}
