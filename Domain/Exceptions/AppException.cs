using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AppException : Exception
    {
        public int Statuscode { get; }
        public AppException(string msg, int statuscode = 400) : base(msg)
        {
            Statuscode = statuscode;
        }
    }
}
