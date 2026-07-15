using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class IdentityOperationResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new();



        public static IdentityOperationResult Success()
        {
            return new IdentityOperationResult
            {
                Succeeded = true,
            };
        }

        public static IdentityOperationResult Failure(List<string> errors)
        {
            return new IdentityOperationResult
            {
                Succeeded = false,
                Errors = errors
            };
        }
    }
}
