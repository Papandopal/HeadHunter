using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using UseCases.Services.Formaters.Interfaces;

namespace UseCases.Services.Formaters
{
    public class DatePeriodFormater : IDatePeriodFormater
    {
        string IDatePeriodFormater.Format(string value)
        {
            string[] list = value.Split(Separators.DatePeriodSeparator);
            var firstDate = DateTime.Parse(list[0]);
            var secondDate = DateTime.Parse(list[1]);
            return secondDate.ToString("dd.MM.yyyy") + " - " + firstDate.ToString("dd.MM.yyyy");
        }
    }
}
