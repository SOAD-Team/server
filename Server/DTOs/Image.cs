using System;

namespace Server.DTOs
{
    public class Image
    {
        public string Id { get; set; }
        public Object ObjectImage { get; set; }
        public string Url { get; set; }

        public Models.Image MapToImage() 
        {
            return new Models.Image
            {
                Id = this.Id,
                ObjectImage = this.ObjectImage
            };
        }
    }
}
