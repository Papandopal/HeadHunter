using AspNetCoreGeneratedDocument;
using Domain.Enums;

namespace ITransitionProject
{
    public class SupportedAccessRuleSkillTypes(IWebHostEnvironment env)
    {
        public IEnumerable<SkillTypes> Types()
        {
            var res = new List<SkillTypes>();
            string dirPath = Path.Combine(env.ContentRootPath, "Views", "Shared", "AccessRules", "Add");
            var pages = Directory.GetFiles(dirPath);
            for (int i = 0;i<pages.Length;i++)
            {
                var line = pages[i];
                line = line.Substring(line.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                line = line.Substring(0, line.LastIndexOf('.'));
                pages[i] = line;
            }
            
            foreach (var page in pages)
            {
                SkillTypes temp;
                if (Enum.TryParse(page, out temp)) res.Add(temp);
            }
            return res;
        } 
    }
}
