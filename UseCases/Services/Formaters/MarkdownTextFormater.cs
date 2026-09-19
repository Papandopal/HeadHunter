using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig;
using UseCases.Services.Formaters.Interfaces;

namespace UseCases.Services.Formaters
{

    public class MarkdownTextFormater : IMarkdownTextFormater
    {
        private readonly MarkdownPipeline Pipeline = 
            new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();

        string IMarkdownTextFormater.Format(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return Markdown.ToHtml(value, Pipeline);
        }
    }

}
