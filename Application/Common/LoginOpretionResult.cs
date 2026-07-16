using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class LoginOpretionResult<T>
    {
        public bool Succeeded { get; set; }

        public T? User { get; set; }

        public string Error { get; set; } = string.Empty;


        public static LoginOpretionResult<T> Success(T user)
            => new LoginOpretionResult<T>() { Succeeded = true, User = user };

        public static LoginOpretionResult<T> Failure(string msg)
            => new LoginOpretionResult<T> { Succeeded = false, Error = msg };
    }
}
