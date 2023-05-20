using DevLogger.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevLogger.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            //call a repository
            var imageURL = await imageRepository.UploadAsync(file);

            if(imageURL == null)
            {
                return Problem("Image Upload Was NOT Successful.", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageURL });
        }
    }
}
