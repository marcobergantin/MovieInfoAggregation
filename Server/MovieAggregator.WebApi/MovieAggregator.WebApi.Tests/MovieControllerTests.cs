using System;
using System.IO;
using System.Linq;
using System.Web.Http.Results;
using System.Threading.Tasks;
using MovieAggregator.WebApi.Controllers;
using MovieAggregator.Contracts;
using MovieAggregator.WebApi.Services;
using MovieAggregator.WebApi.Cache;
using MovieAggregator.DTOs;
using Moq;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Newtonsoft.Json;


namespace MovieAggregator.WebApi.Tests
{
    public class MovieControllerTests
    {
        const string SupportDatasetFile = "SupportFiles\\starwarsepisode.json";
        const string MatchingMovieTitle = "Star Wars";
        const string NoDataMovieTitle = "Star Trek";

        IUnityContainer _container;
        MovieContentDTO _supportDataset;

        [OneTimeSetUp]
        public void Setup()
        {
            LoadSupportDataset();
            RegisterTypes();
            SetupMocks();
        }

        private void LoadSupportDataset()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, SupportDatasetFile);
            _supportDataset = JsonConvert.DeserializeObject<MovieContentDTO>(File.ReadAllText(path));
        }

        private void RegisterTypes()
        {
            _container = new UnityContainer();
            _container.RegisterType<IMovieCacheRepository, InMemoryCacheRepository>();
            _container.RegisterType<IMovieCacheEntityFactory, InMemoryMovieCacheFactory>();
            _container.RegisterType<IMovieCacheService, MovieCacheService>();
            _container.RegisterType<IMovieInfoAggregator, MovieAggregatorService>();
            _container.RegisterType<MovieController>();          
        }

        private void SetupMocks()
        {
            _container.RegisterInstance(GetMovieInfoProviderMock());
            _container.RegisterInstance(GetMovieTrailerProviderMock());
        }

        public IMovieInfoProvider GetMovieInfoProviderMock()
        {
            Mock<IMovieInfoProvider> mockInfoProvider = new Mock<IMovieInfoProvider>();
            mockInfoProvider.Setup(x => x.GetInfo(It.IsAny<string>())).Returns((string title) =>
                {
                    var matchingEntries = _supportDataset.Entries.Where(e => e.Info.Title.Contains(title)).Select(m => m.Info);
                    return Task.FromResult(matchingEntries);
                }
            );

            return mockInfoProvider.Object;
        }

        public ITrailerProvider GetMovieTrailerProviderMock()
        {
            Mock<ITrailerProvider> mockTrailerProvider = new Mock<ITrailerProvider>();
            mockTrailerProvider.Setup(x => x.GetTrailer(It.IsAny<string>(), It.IsAny<DateTime?>())).Returns((string title, DateTime? d) =>
                {
                    var trailerDTO = _supportDataset.Entries.FirstOrDefault(e => e.Info.Title.Contains(title));
                    return Task.FromResult(trailerDTO == null ? null : trailerDTO.Trailer);
                }
            );

            return mockTrailerProvider.Object;
        }

        [Test]
        public async Task RequestInfo_EmptyTitle_ShouldReturnBadRequest()
        { 
            var data = await _container.Resolve<MovieController>().Get(string.Empty);
            Assert.IsInstanceOf<BadRequestResult>(data);
        }

        [Test]
        public async Task RequestInfo_KnownTitle_ShouldReturnData()
        {
            var data = await _container.Resolve<MovieController>().Get(MatchingMovieTitle);
            Assert.IsInstanceOf<OkNegotiatedContentResult<MovieContentDTO>>(data);
            var convertedData = data as OkNegotiatedContentResult<MovieContentDTO>;
            Assert.Greater(convertedData.Content.NumberOfPages, 0);
            Assert.Greater(convertedData.Content.Entries.Count(), 0);
            Assert.IsNotNull(convertedData.Content.Entries.First().Info);
            Assert.IsNotNull(convertedData.Content.Entries.First().Trailer);
            Assert.IsNotEmpty(convertedData.Content.Entries.First().Info.Title);
            Assert.IsNotEmpty(convertedData.Content.Entries.First().Trailer.EmbedURL);
        }

        [Test]
        public async Task RequestInfo_UnknownTitle_ShouldReturnNotFound()
        {
            var data = await _container.Resolve<MovieController>().Get(NoDataMovieTitle);
            Assert.IsInstanceOf<NotFoundResult>(data);
        }
    }
}