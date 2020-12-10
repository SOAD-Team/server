﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Persistence;
using System.IO;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly MoviesDB _context;
        private readonly IImagesDB _mongoContext;

        public ImageController(MoviesDB context, IImagesDB mongoContext)
        {
            _context = context;
            _mongoContext = mongoContext;
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
            data = _mongoContext.Create(data);

            return Ok(data.MapToPresentationModel());
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieImages(string id)
        {
            byte[] images = _mongoContext.Get(id).ObjectImage;
            var file = File(images, "image/jpeg");
            return Ok(file);
        }
    }
}