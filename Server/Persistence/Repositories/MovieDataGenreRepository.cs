using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class MovieDataGenreRepository : Repository<MovieDataGenre>, IMovieDataGenreRepository
    {
        public MovieDataGenreRepository(MoviesDB context) : base(context)
        {
        }

        public override async Task<MovieDataGenre> Create(MovieDataGenre value)
        {
            var result = await _context.MovieDataGenre.AddAsync(value);
            return result.Entity;
        }

        public override Task<MovieDataGenre> Get(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<MovieDataGenre>> GetAll()
        {
            var result = await _context.MovieDataGenre.ToListAsync();
            return result;
        }
    }
}
