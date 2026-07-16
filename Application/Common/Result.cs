using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    // This class is used to represent the result of an operation, including success/failure status, messages, and errors.
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public List<string> Errors { get; }

        protected Result(bool issuccess, string? message, List<string>? errors = null)
        {
            IsSuccess = issuccess;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public static Result Success(string? msg = null)
        {
            return new Result(true, msg);
        }
        public static Result Failure(string msg)
        {
            return new Result(false, msg);
        }

        public static Result Failure(List<string> errors)
        {
            return new Result(false, "Registration failed", errors);
        }
    }


    public class Result<T> : Result
    {
        public T? Data { get; }

        protected Result(bool isSuccess, string? message, T? data, List<string>? errors = null)
            : base(isSuccess, message, errors)
        {
            Data = data;
        }

        public static Result<T> Success(T data, string? msg = null)
            => new Result<T>(true, msg, data);

        public static new Result<T> Failure(string msg)
            => new Result<T>(false, msg, default);
    }
}