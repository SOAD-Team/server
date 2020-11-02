﻿using System.Collections.Generic;

namespace Server.Models
{
    public interface IImagesDB
    {
        public List<Image> Get();

        public Image Get(string id);

        public Image Create(Image Image);

        public void Update(string id, Image ImageIn);

        public void Remove(Image ImageIn);

        public void Remove(string id);
    }
}