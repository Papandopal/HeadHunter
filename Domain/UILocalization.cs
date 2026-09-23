using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class UILocalization
    {
        private static readonly Dictionary<string, Dictionary<string, string>> packs = new Dictionary<string, Dictionary<string, string>>
        {
            {"EN", new Dictionary<string, string>
                {
                    
                }
            },
            {"RU", new Dictionary<string, string>
                {
                    
                }
            }
        };
        public static string GetSentence(string language, string sentenceCode)
        {
            return packs[language][sentenceCode];
        }
    }
}
