using DevLogger.Web.Models.Domain;
using DevLogger.Web.Models.ViewModels;
using DevLogger.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevLogger.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Get Tags from Repository
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Map view model to domain model
            var blogPostDomainModel = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible
            };

            //Map tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetByIdAsync(selectedTagIdGuid);

                if(existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            //Mapping Tags back to domain model
            blogPostDomainModel.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogPostDomainModel);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //Call the repository 
            var blogPosts = await blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (blogPost != null)
            {
                var editBlogPostRequestModel = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name, Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(editBlogPostRequestModel);
            }

            return View(null);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Visible = editBlogPostRequest.Visible,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                UrlHandle = editBlogPostRequest.UrlHandle,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in editBlogPostRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTagId, out var tag)){
                    var foundTag = await tagRepository.GetByIdAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            //Mapping Tags back to domain model
            blogPostDomainModel.Tags = selectedTags;

            var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if (updatedBlogPost != null)
            {
                //Show Success Notification
                return RedirectToAction("List");
            }
            else
            {
                //Show Error Notification
                return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var deletedTag = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);

            if (deletedTag != null)
            {
                //Show Success Notification
                return RedirectToAction("List");
            }

            //Show Error Notification
            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        }
    }
}
