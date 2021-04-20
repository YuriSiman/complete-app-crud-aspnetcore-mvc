using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace CompleteApp.App.Extensions.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string EmailDomain { get; set; } = "yurisiman.com.br";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var content = await output.GetChildContentAsync();
            var target = content.GetContent() + "@" + EmailDomain;

            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(target);
        }
    }
}
