using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.Formaters.Interfaces
{
    public interface IOneOfManyFormater
    {
        public string[] Format(string value);
    }
}
