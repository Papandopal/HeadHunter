using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using UseCases.Services.Formaters.Interfaces;

namespace UseCases.Services.Formaters
{
    public class OneOfManyFormater : IOneOfManyFormater
    {
        string[] IOneOfManyFormater.Format(string value)
        {
            return value.Split(Separators.OneOfManySeparator);
        }
    }
}
