using NUnit.Framework;
using Server.Models;
using Moq;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using Server.Persistence;
using AutoMapper;
using Server.Persistence.Repositories;
using Server.Mapping;

namespace Server.Controllers.Tests
{
    [TestFixture()]
    public class TestController
    {

        private IMapper mockMapper;
        private Mock<IMovieRepository> mockMovieRepository;
        private Mock<IMovieDataRepository> mockMovieDataRepository;
        private Mock<IGenreRepository> mockGenreRepository;
        private Mock<IImageRepository> mockImageRepository;
        private Mock<ILanguageRepository> mockLanguageRepository;
        private Mock<IMovieDataGenreRepository> mockMovieDataGenreRepository;
        private Mock<IMovieDataLanguageRepository> mockMovieDataLanguageRepository;
        private Mock<IReviewRepository> mockReviewRepository;
        private Mock<IStyleRepository> mockStyleRepository;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IUnitOfWork> mockUnitOfWork;

        [SetUp]
        public void Setup()
        {
            this.mockMovieRepository = new Mock<IMovieRepository>(MockBehavior.Loose);
            List<Movie> emptyMovies = new List<Movie>();
            emptyMovies.Add(Movie.Empty);
            mockMovieRepository.Setup(_ => _.Create(It.IsAny<Movie>())).Returns<Movie>(m => Task.FromResult(m));
            mockMovieRepository.Setup(_ => _.Get(It.IsAny<int>())).Returns<Movie>(m => Task.FromResult(Movie.Empty));
            mockMovieRepository.Setup(_ => _.GetAll()).ReturnsAsync(emptyMovies);

            this.mockMovieDataRepository = new Mock<IMovieDataRepository>(MockBehavior.Loose);
            List<MovieData> emptyMovieData = new List<MovieData>();
            emptyMovieData.Add(MovieData.Empty);
            mockMovieDataRepository.Setup(_ => _.Create(It.IsAny<MovieData>())).Returns<MovieData>(m => Task.FromResult(m));
            mockMovieDataRepository.Setup(_ => _.GetByMovieId(It.IsAny<int>())).Returns<MovieData>(m => Task.FromResult(MovieData.Empty));
            mockMovieDataRepository.Setup(_ => _.Get(It.IsAny<int>())).Returns<MovieData>(m => Task.FromResult(MovieData.Empty));
            mockMovieDataRepository.Setup(_ => _.GetByUserId(It.IsAny<int>())).ReturnsAsync(emptyMovieData);
            mockMovieDataRepository.Setup(_ => _.GetAll()).ReturnsAsync(emptyMovieData);

            this.mockGenreRepository = new Mock<IGenreRepository>(MockBehavior.Loose);
            List<Genre> emptyGenre = new List<Genre>();
            emptyGenre.Add(Genre.Empty);
            mockGenreRepository.Setup(_ => _.Create(It.IsAny<Genre>())).Returns<Genre>(g => Task.FromResult(g));
            mockGenreRepository.Setup(_ => _.Get(It.IsAny<int>())).Returns<Genre>(g => Task.FromResult(Genre.Empty));
            mockGenreRepository.Setup(_ => _.GetAll()).ReturnsAsync(emptyGenre);

            this.mockImageRepository = new Mock<IImageRepository>(MockBehavior.Loose);
            List<Image> emptyImage = new List<Image>();
            emptyImage.Add(Image.Empty);
            mockImageRepository.Setup(_ => _.Create(It.IsAny<Image>())).Returns<Image>(i => Task.FromResult(i));
            mockImageRepository.Setup(_ => _.Get(It.IsAny<string>())).Returns<Genre>(g => Task.FromResult(Image.Empty));
            mockImageRepository.Setup(_ => _.GetAll()).ReturnsAsync(emptyImage);

            this.mockLanguageRepository = new Mock<ILanguageRepository>(MockBehavior.Loose);
            List<Language> emptyLanguage = new List<Language>();
            emptyLanguage.Add(Language.Empty);
            mockLanguageRepository.Setup(_ => _.Create(It.IsAny<Language>())).Returns<Language>(l => Task.FromResult(l));
            mockLanguageRepository.Setup(_ => _.Get(It.IsAny<int>())).Returns<Genre>(l => Task.FromResult(Language.Empty));
            mockLanguageRepository.Setup(_ => _.GetAll()).ReturnsAsync(emptyLanguage);

            this.mockMovieDataGenreRepository = new Mock<IMovieDataGenreRepository>(MockBehavior.Loose);
            List<MovieDataGenre> emptyMDGenre = new List<MovieDataGenre>();
            emptyMDGenre.Add(MovieDataGenre.Empty);
            mockMovieDataGenreRepository.Setup(_ => _.Create(It.IsAny<MovieDataGenre>())).Returns<MovieDataGenre>(mg => Task.FromResult(mg));
            mockMovieDataGenreRepository.Setup(_ => _.GetAll()).ReturnsAsync(emptyMDGenre);

            this.mockMovieDataLanguageRepository = new Mock<IMovieDataLanguageRepository>(MockBehavior.Loose);
            List<MovieDataLanguage> emptyMDLanguage = new List<MovieDataLanguage>();
            emptyMDLanguage.Add(MovieDataLanguage.Empty);
            mockMovieDataLanguageRepository.Setup(_ => _.Create(It.IsAny<MovieDataLanguage>())).Returns<MovieDataLanguage>(ml => Task.FromResult(ml));
            mockMovieDataLanguageRepository.Setup(_ => _.GetAll()).ReturnsAsync(emptyMDLanguage);

            this.mockReviewRepository = new Mock<IReviewRepository>(MockBehavior.Loose);
            List<Review> emptyReview = new List<Review>();
            emptyReview.Add(Review.Empty);
            mockReviewRepository.Setup(_ => _.GetbyMovieId(It.IsAny<int>())).ReturnsAsync(emptyReview);

            Mock<IStyleRepository> mockStyleRepository = new Mock<IStyleRepository>(MockBehavior.Loose);
            List<Style> emptyStyle = new List<Style>();
            emptyStyle.Add(Style.Empty);
            mockStyleRepository.Setup(_ => _.Get(It.IsAny<int>())).Returns<Style>(s => Task.FromResult(Style.Empty));
            mockStyleRepository.Setup(_ => _.GetAll()).ReturnsAsync(emptyStyle);

            this.mockUserRepository = new Mock<IUserRepository>(MockBehavior.Loose);
            List<User> emptyUser = new List<User>();
            emptyUser.Add(User.Empty);
            mockUserRepository.Setup(_ => _.Create(It.IsAny<User>())).Returns<User>(u => Task.FromResult(u));
            mockUserRepository.Setup(_ => _.Get(It.IsAny<int>())).Returns<User>(s => Task.FromResult(User.Empty));
            mockUserRepository.Setup(_ => _.GetByEmail(It.IsAny<string>())).Returns<User>(s => Task.FromResult(User.Empty));

            this.mockUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Loose);

            this.mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile(
                        this.mockMovieRepository.Object,
                        this.mockGenreRepository.Object,
                        this.mockLanguageRepository.Object,
                        this.mockStyleRepository.Object,
                        this.mockReviewRepository.Object,
                        this.mockMovieDataRepository.Object,
                        this.mockMovieDataGenreRepository.Object,
                        this.mockMovieDataLanguageRepository.Object,
                        this.mockUnitOfWork.Object
                        ));
                }).CreateMapper();
        }
    }
}
