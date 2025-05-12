using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.Helper
{
    public sealed class ResultT<TValue> : Result
    {
        private readonly TValue? _value;

        private ResultT(TValue value) : base()
        {
            _value = value;
        }

        private ResultT(ErrorMassege error) : base(error)
        {
            _value = default;
        }

        public TValue Value =>
            IsSuccess ? _value! : throw new InvalidOperationException("Cannot access value when result is failure");

        public static implicit operator ResultT<TValue>(ErrorMassege error) =>
            new(error);

        public static implicit operator ResultT<TValue>(TValue value) =>
            new(value);

        public static ResultT<TValue> Success(TValue value) =>
            new(value);

        public static new ResultT<TValue> Failure(ErrorMassege error) =>
            new(error);
    }

}
