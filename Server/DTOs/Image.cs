﻿using System.IO;
using Newtonsoft.Json;
using Server.Models;

namespace Server.DTOs
{
    public class Image
    {
        public string Id { get; set; }
        [JsonProperty]
        public FileModel ObjectImage { get; set; }
        public string Url { get; set; }

        public Models.Image MapToImage() 
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                this.ObjectImage.FormFile.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            return new Models.Image
            (
                this.Id,
                fileBytes
            );
        }
    }
}
