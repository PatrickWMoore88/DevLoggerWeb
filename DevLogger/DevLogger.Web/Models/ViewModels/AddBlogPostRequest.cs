using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevLogger.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public DateTime PublishedDate { get; set; }
        public bool Visible { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string UrlHandle { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }

        //Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        //Collect Tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
