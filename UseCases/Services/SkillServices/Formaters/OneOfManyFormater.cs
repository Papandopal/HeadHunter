using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.SkillServices.Formaters
{
    public static class OneOfManyFormater
    {
        public static string[] Render(string value)
        {
            return value.Split(";");
        }
    }
}
