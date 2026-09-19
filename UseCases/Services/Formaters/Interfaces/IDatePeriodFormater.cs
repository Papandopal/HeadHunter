using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.Formaters.Interfaces
{
    public interface IDatePeriodFormater 
    {
        public string Format(string value);
    }
}
