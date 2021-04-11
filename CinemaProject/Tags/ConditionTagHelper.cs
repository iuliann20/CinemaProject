using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CinemaProject.Tags
{
   [HtmlTargetElement(Attributes = "asp-conditional")]
   public class ConditionTagHelper : TagHelper
   {
      [HtmlAttributeName("for-condition")]
      public bool Condition { get; set; }

      public override void Process(TagHelperContext context, TagHelperOutput output)
      {
         if (!Condition) {
            output.SuppressOutput();
         }
      }
   }
}
