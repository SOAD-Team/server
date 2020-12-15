using Server.Models;
using System.Linq;
using Server.Persistence;

namespace Server.Helpers
{
    public static class ScoreHelper
    {
        public static int GetRecommendationScore(Resources.UserPoints points, Resources.Movie movie, MovieDataRepository movieDataRepository, ReviewRepository reviewRepository)
        {
            int imdb = movie.Imdb.GetValueOrDefault();
            int ms = movie.MetaScore.GetValueOrDefault();
            int com = ScoreHelper.GetMovieCommunityScore(movie.IdMovie.Value, reviewRepository);
            int platFav = 0;
            int pop = ScoreHelper.GetMoviePopularity(movie.IdMovie.Value, movieDataRepository, reviewRepository);

            if (movie.PlatFav)
                platFav = 100;

            int score = (imdb * points.Imdb / 100) + (ms * points.MetaScore / 100) + (com * points.Community / 100) + (platFav * points.PlatFav / 100)
                + (pop * points.Popularity / 100);

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

            if (ScoreHelper.isFromThisYear(idMovie, movieDataRepository))
                score += 20;
            if (ScoreHelper.isPlataformFavorite(idMovie, movieDataRepository))
                score += 20;
            score += ScoreHelper.reviewsScore(idMovie, reviewRepository);

            return score;
        }
        private static bool isFromThisYear(int idMovie, MovieDataRepository movieDataRepository)
        {
            MovieData movie = movieDataRepository.GetByMovieId(idMovie).Result;

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
            return movie.PlatFav;
        }
    }
}
