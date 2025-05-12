using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.Helper
{
    public class Result <T>
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get;private set; }
        public T Value { get;private set; }

        public bool IsFailure => !IsSuccess;
        private Result(bool isSuccess, string errorMessage, T value )
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage ?? string.Empty; ;
            Value = value;
        }
        public static Result<T> Success(T value) => new Result<T>(true, null, value);
        public static Result<T> Failure(string errorMessage) => new Result<T>(false, errorMessage, default);
        public override string ToString() => IsSuccess ? $"Success: {Value}" : $"Failure: {ErrorMessage}";

    }
}
