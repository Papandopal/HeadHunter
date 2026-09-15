using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig;

namespace UseCases.Services.SkillServices.Formaters
{

    public static class MarkdownTextFormater
    {
        private static readonly MarkdownPipeline Pipeline = 
            new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();

        public static string Render(string markdownText)
        {
            if (string.IsNullOrWhiteSpace(markdownText))
                return string.Empty;

            return Markdown.ToHtml(markdownText, Pipeline);
        }

    }

}
