﻿using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class LanguageRepository : Repository<Language>
    {
        public LanguageRepository(MoviesDB context) : base(context)
        {

        }

        public override Task<Language> Create(Language value)
        {
            throw new System.NotImplementedException();
        }

        public override Task<Language> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<Language>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
