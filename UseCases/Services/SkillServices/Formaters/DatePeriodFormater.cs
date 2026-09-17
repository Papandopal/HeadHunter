using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace UseCases.Services.SkillServices.Formaters
{
    public static class DatePeriodFormater
    {
        public static string Render(string value)
        {
            string[] list = value.Split(Separators.DatePeriodSeparator);
            var firstDate = DateTime.Parse(list[0]);
            var secondDate = DateTime.Parse(list[1]);
            return secondDate.ToString("dd.MM.yyyy") + " - " + firstDate.ToString("dd.MM.yyyy");
        }
    }
}
