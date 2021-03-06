﻿using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class MovieDataLanguageRepository : Repository<MovieDataLanguage>, IMovieDataLanguageRepository
    {
        public MovieDataLanguageRepository(MoviesDB context) : base(context)
        {
        }

        public override async Task<MovieDataLanguage> Create(MovieDataLanguage value)
        {
            var result = await _context.MovieDataLanguage.AddAsync(value);
            return result.Entity;
        }

        public override Task<MovieDataLanguage> Get(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<MovieDataLanguage>> GetAll()
        {
            var result = await _context.MovieDataLanguage.ToListAsync();
            return result;
        }
    }
}
