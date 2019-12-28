using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Exceptions
{
    public class BadRequestEx : Exception
    {
        public BadRequestEx(string message) : base(message)
        {

        }
    }
}
