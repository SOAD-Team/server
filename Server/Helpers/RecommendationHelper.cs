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
        public static Resources.Recommendation GetRecommendationData(Resources.UserPoints points, int idMovie, MovieDataRepository movieDataRepository, MovieRepository movieRepository, ReviewRepository reviewRepository, IMapper mapper)
        {
            MovieData movie = movieDataRepository.GetByMovieId(idMovie).Result;
            //MovieData movie = MovieControllerHelper.GetMostRecentData(movies, _context).FirstOrDefault();
            int userId = movieRepository.Get(idMovie).Result.IdUser;
            if (movie == null)
                return null;
            int score = RecommendationHelper.getRecommendationScore(points, movie, movieDataRepository, reviewRepository);

            Resources.Movie data = mapper.Map<Resources.Movie>(movie);

            return new Resources.Recommendation { Movie = data, Score = score };
        }
        private static int getRecommendationScore(Resources.UserPoints points, MovieData movie, MovieDataRepository movieDataRepository, ReviewRepository reviewRepository)
        {
            int imdb = movie.Imdb.GetValueOrDefault();
            int ms = movie.MetaScore.GetValueOrDefault();
            int com = RecommendationHelper.GetMovieCommunityScore(movie.IdMovie, reviewRepository);
            int platFav = 0;
            int pop = RecommendationHelper.GetMoviePopularity(movie.IdMovie, movieDataRepository, reviewRepository);

            if (movie.PlatFav)
                platFav = 100;

            int score = (imdb * points.Imdb / 100) * (ms * points.MetaScore / 100) * (com * points.Community / 100) * (platFav * points.PlatFav / 100)
                * (pop * points.Popularity / 100);

            return score;
        }

        public static int GetMovieCommunityScore(int idMovie, ReviewRepository reviewRepository)
        {
            int score = 0;
            Review[] reviews = reviewRepository.GetbyMovieId(idMovie).Result.ToArray();

            if (reviews.Length == 0)
                return 10;

            foreach (Review review in reviews)
                score += review.Score;

            return score/reviews.Length;
        }
        public static int GetMoviePopularity(int idMovie, MovieDataRepository movieDataRepository, ReviewRepository reviewRepository)
        {
            int score = 0;

            if (RecommendationHelper.isFromThisYear(idMovie, movieDataRepository))
                score += 20;
            if (RecommendationHelper.isPlataformFavorite(idMovie, movieDataRepository))
                score += 20;
            score += RecommendationHelper.reviewsScore(idMovie, reviewRepository);

            return score;
        }
        private static bool isFromThisYear(int idMovie, MovieDataRepository movieDataRepository)
        {
            MovieData movie = movieDataRepository.GetByMovieId(idMovie).Result;
            //MovieData movie = MovieControllerHelper.GetMostRecentData(movies, _context).FirstOrDefault();

            return movie.Year == System.DateTime.Now.Year;
        }
        private static int reviewsScore (int idMovie, ReviewRepository reviewRepository)
        {
            int score = 0;
            int reviews = reviewRepository.GetbyMovieId(idMovie).Result.ToList().Count();

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

        private static bool isPlataformFavorite(int idMovie, MovieDataRepository movieDataRepository)
        {
            MovieData movie = movieDataRepository.GetByMovieId(idMovie).Result;
            //MovieData movie = MovieControllerHelper.GetMostRecentData(movies, _context).FirstOrDefault();
            return movie.PlatFav;
        }
    }
}
