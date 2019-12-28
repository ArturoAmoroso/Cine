using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Exceptions
{
    public class NotFoundEx : Exception
    {
        public NotFoundEx(string message) : base(message)
        {

        }
    }
}
