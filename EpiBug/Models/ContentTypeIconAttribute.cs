using System;

namespace EpiBug.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ContentTypeIconAttribute : Attribute
    {
        public string CssClass { get; set; }

        public ContentTypeIconAttribute(string cssClass)
        {
            CssClass = $"customContentTypeIcon customContentTypeIcon--{cssClass}";
        }
    }
}