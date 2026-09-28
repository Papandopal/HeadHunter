using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime;
using Domain;
using UseCases.Services.Formaters.Interfaces;
using UseCases.Services.LocalizationServices.Interfaces;

namespace UseCases.Services.Formaters
{
    public class UILocalizationFormater(IUILocalizationService UILocalizationService) : IUILocalizationFormater
    {
        string IUILocalizationFormater.Format(string value)
        {
            return UILocalization.GetSentence(UILocalizationService.GetUILanguage(), value);
        }

        IEnumerable<string> IUILocalizationFormater.GetSupportedLanguages()
        {
            return UILocalization.GetSupportedLanguages();
        }

        string IUILocalizationFormater.UILanguage()
        {
            return UILocalizationService.GetUILanguage();
        }
    }
}
