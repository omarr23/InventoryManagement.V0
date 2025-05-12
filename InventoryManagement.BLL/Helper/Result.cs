using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.Helper
{
    public class Result
    {
        protected Result()
        {
            IsSuccess = true;
            Error = default;
        }

        protected Result(ErrorMassege error)
        {
            IsSuccess = false;
            Error = error;
        }

        public bool IsSuccess { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ErrorMassege? Error { get; }

        public static implicit operator Result(ErrorMassege error) =>
            new(error);

        public static Result Success() =>
            new();

        public static Result Failure(ErrorMassege error) =>
            new(error);
    }

}
