using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.LocalizationServices.Interfaces
{
    public interface IUILocalizationService
    {
        public void ChangeUILanguage(string language);
        public string GetUILanguage();
    }
}
