using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class MovieDataRepository : Repository<MovieData>, IMovieDataRepository
    {
        public MovieDataRepository(MoviesDB context) : base(context) { }
        public override async Task<MovieData> Create(MovieData data)
        {
            data.RegisterDate = DateTime.Now;
            var result = await _context.MovieData.AddAsync(data);
            return result.Entity;
        }

        public async Task<MovieData> GetByMovieId(int id)
        {
            var result = await _context.MovieData.Where(m => m.IdMovie == id).ToListAsync();
            MovieData latest = null;
            foreach (var movieData in result)
            {
                if(latest == null)
                {
                    latest = movieData;
                }
                else
                {
                    if(latest.RegisterDate < movieData.RegisterDate)
                    {
                        latest = movieData;
                    }
                }
            }
            return latest;
        }

        public async override Task<MovieData> Get(int id)
        {
            var result = await _context.MovieData.Where(m => m.IdMovieData == id).FirstOrDefaultAsync();
            return result;

        }

        public async override Task<IEnumerable<MovieData>> GetAll()
        {
            var movies = new List<MovieData>();
            await _context.MovieData
                .Include(md => md.MovieDataLanguage)
                .Include(md => md.MovieDataGenre)
                .Select(val => val).ForEachAsync(data =>
                {
                    var existingMovie = movies.Where(m => m.IdMovie == data.IdMovie).FirstOrDefault();
                    if (existingMovie == null)
                        movies.Add(data);
                    else
                        if (data.RegisterDate > existingMovie.RegisterDate)
                        movies[movies.IndexOf(existingMovie)] = data;
                });

            return movies;
        }

        public async Task<IEnumerable<MovieData>> GetByUserId(int id)
        {
            var movies = new List<MovieData>();

            await _context.MovieData
                .Include(md => md.MovieDataLanguage)
                .Include(md => md.MovieDataGenre)
                .Join(_context.Movie, md => md.IdMovie, m => m.IdMovie,
                (md, m) => new { md, m }).Where(v => v.m.IdUser == id)
                .Select(val => val.md).ForEachAsync(data =>
                {
                    var existingMovie = movies.Where(m => m.IdMovie == data.IdMovie).FirstOrDefault();
                    if (existingMovie == null)
                        movies.Add(data);
                    else
                        if (data.RegisterDate > existingMovie.RegisterDate)
                        movies[movies.IndexOf(existingMovie)] = data;
                });

            return movies;
        }
    }
}
