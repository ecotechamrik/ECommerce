#region [ Namespace ]
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
#endregion

namespace EcoTechAdmin.TagHelpers
{
    [HtmlTargetElement("customicon")]
    public class CustomTagHelper: TagHelper
    {
        #region [ Custom Icon Properties ]
        public string Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; } = "More Details";
        #endregion

        #region [ Create Custom Icons from Awesome Font CSS ]
        /// <summary>
        /// Create Custom Icons from Awesome Font CSS
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            
            // Back Button Image
            if (this.Type.ToLower() == "back")
                sb.AppendFormat("<i alt='{0}' title='{0}' class=\"fa fa-chevron-circle-left cc_pointer\" aria-hidden=\"true\"></i>", Title);
            
            // Add Button Image
            else if (this.Type.ToLower() == "add")
                sb.AppendFormat("<i alt='{0}' title='{0}' class=\"fa create_new cc_pointer\" aria-hidden=\"true\"></i>", Title);

            // Edit Button Image
            else if (this.Type.ToLower() == "edit")
                sb.AppendFormat("<i alt='{0}' title='{0}' class=\"fa fa-pencil-square-o cc_pointer\" aria-hidden=\"true\"></i>", Title);
            
            // Delete Button Image
            else if (this.Type.ToLower() == "delete")
                sb.AppendFormat("<i alt='{0}' title='{0}' class=\"fa fa-trash\" aria-hidden=\"true\"></i>", Title);
            
            // Yes Button Image
            else if (this.Type.ToLower() == "yes")
                sb.AppendFormat("<i alt='{0}' title='{0}' class=\"fa fa-check\" aria-hidden=\"true\"></i>", Title);

            // Gallery Image
            else if (this.Type.ToLower() == "gallery")
                sb.AppendFormat("<i alt='{0}' title='{0}' class=\"fa fa-images\" aria-hidden=\"true\"></i>", Title);

            // Question Mark Image
            else if (this.Type.ToLower() == "question")
                sb.AppendFormat("<i alt='{0}' title='{0}' class=\"fa fa-question\" aria-hidden=\"true\"></i>", Title);

            // Loading Image
            else if (this.Type.ToLower() == "user")
                sb.AppendFormat("<i alt='{0}' title='{0}' class=\"far fa-user\" aria-hidden=\"true\"></i>", Title);

            // Loading Image
            else if (this.Type.ToLower() == "loading")
                sb.AppendFormat("<i class=\"fa-refresh fa-spin\" aria-hidden=\"true\"></i>", Title);
            
            // More Details Button Image
            else if (this.Type.ToLower() == "details")
                sb.AppendFormat("<button alt='{0}' title='{0}' class=\"btn\">{1}</button>", Title, Text);

            output.PreContent.SetHtmlContent(sb.ToString());
        }
        #endregion
    }
}
