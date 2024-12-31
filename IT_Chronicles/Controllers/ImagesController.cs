using IT_Chronicles.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IT_Chronicles.Controllers
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

        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            //call image repository
            var imageUrl = await imageRepository.UploadAsync(file);

            if (imageUrl == null)
            {

                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);

            }

            return new JsonResult(new {link = imageUrl});
        }
    }
}
