using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Result
{
    public class CustomResult<T>
    {
        private readonly T? _value;
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T Value
        {
            get
            {
                if (IsFailure)
                    throw new InvalidOperationException("There is Now Value for failure");
                return _value!;
            }
            private init => _value = value;
        }
        public CustomError Error { get; }
        private CustomResult(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = CustomError.None;
        }
        private CustomResult(CustomError error)
        {
            if (error == CustomError.None)
                throw new ArgumentException("InValid Error Message");
            Error = error;
            IsSuccess = false;
        }
        public static CustomResult<T> Success(T value) => new CustomResult<T>(value);
        public static CustomResult<T> Failure(CustomError error) => new CustomResult<T>(error);
    }
    public class CustomResult
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public CustomError Error { get; }

        private CustomResult(bool isSuccess, CustomError error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static CustomResult Success()
            => new CustomResult(true, CustomError.None);

        public static CustomResult Failure(CustomError error)
        {
            if (error == CustomError.None)
                throw new ArgumentException("Invalid error message");
            return new CustomResult(false, error);
        }
    }
}

