using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Exceptions
{
    public class NotEqualItemVersionException : Exception
    {
        public NotEqualItemVersionException(string message) : base(message)
        {

        }
    }
}
