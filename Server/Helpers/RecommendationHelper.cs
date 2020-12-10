using Server.Models;
using Server.Structs;
using System.Linq;
using Server.Persistence;
using AutoMapper;
using System.Collections.Generic;

namespace Server.Helpers
{
    public static class RecommendationHelper
    {
        public static Resources.Recommendation[] FilterRecommendations(Resources.Recommendation[] recommendations)
        {
            Resources.Recommendation[] values = recommendations.OrderByDescending(val => val.Score).Take(10).ToArray();
            foreach (var value in values)
                System.Console.WriteLine("Name: " + value.Movie.Name + ", Score: "+ value.Score.ToString());
            return values;
        }
        public static Resources.Recommendation GetRecommendationData(Resources.UserPoints points, int idMovie, MoviesDB _context, IImagesDB _mongoContext, IMapper mapper)
        {
            MovieData[] movies = _context.MovieData.Where(val => val.IdMovie == idMovie).ToArray();
            MovieData movie = MovieControllerHelper.GetMostRecentData(movies, _context).FirstOrDefault();
            int userId = _context.Movie.Where(val => val.IdMovie == idMovie).FirstOrDefault().IdUser;
            if (movie == null)
                return null;
            int score = RecommendationHelper.getRecommendationScore(points, movie, _context);

            Data completeData = MovieControllerHelper.CreateData(new MovieData[1]{ movie }, _context, mapper)[0];

            Resources.Movie movieData = movie.MapToPresentationModel(
                userId,
                mapper.Map<IEnumerable<Resources.KeyValuePair>,IEnumerable<Genre>>(completeData.Genres).ToArray(),
                mapper.Map<IEnumerable<Resources.KeyValuePair>, IEnumerable<Language>>(completeData.Languages).ToArray(),
                _mongoContext,
                mapper.Map<IEnumerable<Resources.KeyValuePair>, IEnumerable<Style>>(completeData.Styles).ToArray(),
                mapper
            );

            return new Resources.Recommendation(movieData, score);
        }
        private static int getRecommendationScore(Resources.UserPoints points, MovieData movie, MoviesDB _context)
        {
            int imdb = movie.Imdb.GetValueOrDefault();
            int ms = movie.MetaScore.GetValueOrDefault();
            int com = RecommendationHelper.GetMovieCommunityScore(movie.IdMovie, _context);
            int platFav = 0;
            int pop = RecommendationHelper.GetMoviePopularity(movie.IdMovie, _context);

            if (movie.PlatFav)
                platFav = 100;

            int score = (imdb * points.Imdb / 100) * (ms * points.MetaScore / 100) * (com * points.Community / 100) * (platFav * points.PlatFav / 100)
                * (pop * points.Popularity / 100);

            return score;
        }

        public static int GetMovieCommunityScore(int idMovie, MoviesDB _context)
        {
            int score = 0;
            Review[] reviews = _context.Review.Where(val => val.IdMovie == idMovie).ToArray();

            if (reviews.Length == 0)
                return 10;

            foreach (Review review in reviews)
                score += review.Score;

            return score/reviews.Length;
        }
        public static int GetMoviePopularity(int idMovie, MoviesDB _context)
        {
            int score = 0;

            if (RecommendationHelper.isFromThisYear(idMovie, _context))
                score += 20;
            if (RecommendationHelper.isPlataformFavorite(idMovie, _context))
                score += 20;
            score += RecommendationHelper.reviewsScore(idMovie, _context);

            return score;
        }
        private static bool isFromThisYear(int idMovie, MoviesDB _context)
        {
            MovieData[] movies = _context.MovieData.Where(val => val.IdMovie == idMovie).ToArray();
            MovieData movie = MovieControllerHelper.GetMostRecentData(movies, _context).FirstOrDefault();

            return movie.Year == System.DateTime.Now.Year;
        }
        private static int reviewsScore (int idMovie, MoviesDB _context)
        {
            int score = 0;
            int reviews = _context.Review.Where(val => val.IdMovie == idMovie).Count();

            if (reviews > 0)
                score += 15;
            if (reviews > 5)
                score += 10;
            if (reviews > 10)
                score += 15;
            if (reviews > 15)
                score += 20;

            return score;
        }

        private static bool isPlataformFavorite(int idMovie, MoviesDB _context)
        {
            MovieData[] movies = _context.MovieData.Where(val => val.IdMovie == idMovie).ToArray();
            MovieData movie = MovieControllerHelper.GetMostRecentData(movies, _context).FirstOrDefault();

            return movie.PlatFav;
        }
    }
}
