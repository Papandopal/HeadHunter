using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.Formaters.Interfaces
{
    public interface IUILocalizationFormater
    {
        public string Format(string value);
        public string UILanguage();
        public IEnumerable<string> GetSupportedLanguages();
    }
}
