using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Persistence.Repositories;
using System.IO;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly IMapper _mapper;

        public ImageController(IImageRepository imageRepository, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage([FromForm] IFormFile image)
        {
            System.Console.WriteLine(image.Name);
            byte[] fileBytes;

            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                fileBytes = stream.ToArray();
            }

            Image data = new Image(fileBytes);
            data = await imageRepository.Create(data);

            return Ok(_mapper.Map<Resources.Image>(data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieImages(string id)
        {
            var result = await imageRepository.Get(id);
            byte[] images = result.ObjectImage;
            var file = File(images, "image/jpeg");
            return file;
        }
    }
}
