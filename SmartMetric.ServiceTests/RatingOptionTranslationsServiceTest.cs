using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Services.Adders;
using SmartMetric.Core.Services.Deleters;
using SmartMetric.Core.Services.Getters;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using Xunit.Abstractions;

namespace SmartMetric.ServiceTests
{
    public class RatingOptionTranslationsServiceTest
    {
        //variables
        private readonly IRatingOptionTranslationAdderService _translationsAdderService;
        private readonly IRatingOptionTranslationGetterService _translationsGetterService;
        private readonly IRatingOptionTranslationDeleterService _translationsDeleterService;

        private readonly Mock<IRatingOptionTranslationsRepository> _translationsRepositoryMock;
        private readonly IRatingOptionTranslationsRepository _translationsRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        //constructor
        public RatingOptionTranslationsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _translationsRepositoryMock = new Mock<IRatingOptionTranslationsRepository>();
            _translationsRepository = _translationsRepositoryMock.Object;
            _testOutputHelper = testOutputHelper;

            var AdderLoggerMock = new Mock<ILogger<RatingOptionTranslationsAdderService>>();
            var GetterLoggerMock = new Mock<ILogger<RatingOptionTranslationsGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<RatingOptionTranslationDeleterService>>();

            _translationsAdderService = new RatingOptionTranslationsAdderService(_translationsRepository, AdderLoggerMock.Object);
            _translationsGetterService = new RatingOptionTranslationsGetterService(_translationsRepository, GetterLoggerMock.Object);
            _translationsDeleterService = new RatingOptionTranslationDeleterService(_translationsRepository, DeleterLoggerMock.Object);
        }

        #region AddRatingOptionTranslation Tests

        /// <summary>
        /// Teste que recebe um objeto null para adicionar uma tradução à resposta de classificação (RatingOptionTranslationsDTOAddRequest == null)
        /// </summary>
        /// <returns>Deve lançar uma exceção do tipo ArgumentNullException</returns>
        [Fact]
        public async Task AddRatingOptionTranslation_NullTranslation_ToBeArgumentNullException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddRatingOptionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        /// <summary>
        /// Teste para adicionar uma tradução a uma resposta de classificação (objeto do tipo RatingOptionTranslation), mas recebe um valor null no campo Language
        /// </summary>
        /// <returns>Deve lançar uma exceção do tipo ArgumentException</returns>
        [Fact]
        public async Task AddRatingOptionTranslation_LanguageIsNull_ToBeArgumentNullException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest request = new RatingOptionTranslationDTOAddRequest
            {
                RatingOptionId = Guid.NewGuid(),
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddRatingOptionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        /// <summary>
        /// Teste para adicionar uma tradução a uma resposta de classificação (objeto do tipo RatingOptionTranslation), mas recebe um valor null no campo RatingOptionId
        /// </summary>
        /// <returns>Deve lançar uma exceção do tipo ArgumentException</returns>
        [Fact]
        public async Task AddRatingOptionTranslation_RatingOptionIdIsNull_ToBeArgumentNullException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest request = new RatingOptionTranslationDTOAddRequest
            {
                Language = Language.PT,
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddRatingOptionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        /// <summary>
        /// Teste para adicionar uma tradução a uma resposta de classificação (objeto do tipo RatingOptionTranslation), mas recebe um valor null no campo Description
        /// </summary>
        /// <returns>Deve lançar uma exceção do tipo ArgumentException</returns>
        [Fact]
        public async Task AddRatingOptionTranslation_DescriptionIsNull_ToBeArgumentNullException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest request = new RatingOptionTranslationDTOAddRequest
            {
                RatingOptionId = Guid.NewGuid(),
                Language = Language.PT
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddRatingOptionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        /// <summary>
        /// Teste para adicionar uma tradução a uma resposta de classificação (objeto do tipo RatingOptionTranslation), recebendo todos os campos preenchidos
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddRatingOptionTranslation_FullDetails_ToBeArgumentNullException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest? request = _fixture.Build<RatingOptionTranslationDTOAddRequest>().With(temp => temp.Language, Language.PT).Create();

            RatingOptionTranslation translation = request.ToRatingOptionTranslation();
            RatingOptionTranslationDTOResponse response = translation.ToRatingOptionTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.AddRatingOptionTranslation(It.IsAny<RatingOptionTranslation>())).ReturnsAsync(translation);

            //Act
            RatingOptionTranslationDTOResponse response_from_add = await _translationsAdderService.AddRatingOptionTranslation(request);
            response.RatingOptionTranslationId = response_from_add.RatingOptionTranslationId;

            //Assert
            response_from_add.Should().NotBe(Guid.Empty);
            response_from_add.Should().Be(response);
        }

        #endregion

        #region GetAllRatingOptionTranslation Tests

        /// <summary>
        /// Teste sobre a função que retorna uma lista de todas as traduções
        /// </summary>
        /// <returns>Deve retornar uma lista vazia por defeito</returns>
        [Fact]
        public async Task GetAllRatingOptionTranslation_ToBeEmptyList()
        {
            //Arrange
            var translations = new List<RatingOptionTranslation>();
            _translationsRepositoryMock.Setup(temp => temp.GetAllRatingOptionTranslations()).ReturnsAsync(translations);

            //Act
            List<RatingOptionTranslationDTOResponse> responseFromGet = await _translationsGetterService.GetAllRatingOptionTranslations();

            //Assert
            responseFromGet.Should().BeEmpty();
        }

        /// <summary>
        /// Teste sobre a função que retorna uma lista de todas as traduções
        /// </summary>
        /// <returns>Deve retornar as traduções inseridas previamente</returns>
        [Fact]
        public async Task GetAllRatingOptionTranslation_WithSomeTranslations_ToBeSuccessfull()
        {
            //Arrange
            List<RatingOptionTranslation> translations = new List<RatingOptionTranslation>()
            {
                _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOption, null as RatingOption).Create(),
                _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOption, null as RatingOption).Create(),
                _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOption, null as RatingOption).Create(),
            };

            List<RatingOptionTranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToRatingOptionTranslationDTOResponse()).ToList();

            //Log of the expected response
            _testOutputHelper.WriteLine("Expected Response:");
            foreach (var item in expectedResponse)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }
            _translationsRepositoryMock.Setup(temp => temp.GetAllRatingOptionTranslations()).ReturnsAsync(translations);

            //Act
            List<RatingOptionTranslationDTOResponse> actualResponse = await _translationsGetterService.GetAllRatingOptionTranslations();

            //Log of the actual response
            _testOutputHelper.WriteLine("Actual Response:");
            foreach (var item in actualResponse)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            //Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion

        #region GetRatingOptionTranslationById Tests

        /// <summary>
        /// Teste sobre a função que retorna um objeto do tipo RatingOptionTranslation recebendo por parâmetro o seu Id
        /// </summary>
        /// <returns>Deve retornar uma exceção do tipo ArgumentNullException</returns>
        [Fact]
        public async Task GetRatingOptionTranslationsById_NullRatingOptionTranslationId_ToBeNull()
        {
            //Arrange
            Guid? id = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptionTranslationById(id);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        /// <summary>
        /// Teste sobre a função que retorna um objeto do tipo RatingOptionTranslation recebendo por parâmetro o seu Id
        /// </summary>
        /// <returns>Deve retornar um objeto do tipo RatingOptionTranslationDTOResponse correspondendo com o Id recebido como parâmetro</returns>
        [Fact]
        public async Task GetRatingOptionTranslationsById_WithValidId_ToBeSuccessful()
        {
            //Arrange
            RatingOptionTranslation translation = _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOption, null as RatingOption).Create();

            RatingOptionTranslationDTOResponse expectedResponse = translation.ToRatingOptionTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationById(It.IsAny<Guid>())).ReturnsAsync(translation);

            //Act
            RatingOptionTranslationDTOResponse? actualResponse = await _translationsGetterService.GetRatingOptionTranslationById(translation.RatingOptionTranslationId);

            //Assert
            actualResponse.Should().Be(expectedResponse);
        }

        #endregion

        #region GetRatingOptionTranslationByRatingOptionId Tests

        /// <summary>
        /// </summary>
        /// <returns>Deve lançar uma exceção do tipo ArgumentNullException quando recebe como parâmetro um RatingOptionId nulo</returns>
        [Fact]
        public async Task GetRatingOptionTranslationsByRatingOptionId_NullRatingOptionId_ToThrowArgumentNullException()
        {
            //Arrange
            Guid? ratingOptionId = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptiontranslationsByRatingOptionId(ratingOptionId);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Deve retornar uma lista vazia quando apesar de receber um RatingOptionId válido, não tem traduções associadas a este Id</returns>
        [Fact]
        public async Task GetRatingOptionTranslationsByRatingOptionId_WithValidId_NoTranslations_ToBeEmptyList()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationByRatingOptionId(It.IsAny<Guid>())).ReturnsAsync(new List<RatingOptionTranslation>());

            //Act
            List<RatingOptionTranslationDTOResponse>? response = await _translationsGetterService.GetRatingOptiontranslationsByRatingOptionId(ratingOptionId);

            //Assert
            response.Should().BeEmpty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Deve retornar uma lista de objetos do tipo RatingOptionTranslationDTOResponse correspondente com o parâmetro passado (ratingOptionId) </returns>
        [Fact]
        public async Task GetRatingOptionTranslationsByRatingOptionId_WithValidId_WithTranslations_ToBeSuccessful()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();

            List<RatingOptionTranslation> translations = new List<RatingOptionTranslation>()
            {
                _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOptionId, ratingOptionId).With(temp => temp.RatingOption, null as RatingOption).Create(),
                _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOptionId, ratingOptionId).With(temp => temp.RatingOption, null as RatingOption).Create(),
                _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOptionId, ratingOptionId).With(temp => temp.RatingOption, null as RatingOption).Create(),
            };

            List<RatingOptionTranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToRatingOptionTranslationDTOResponse()).ToList();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationByRatingOptionId(It.IsAny<Guid>())).ReturnsAsync(translations);

            //Act
            List<RatingOptionTranslationDTOResponse>? actualResponse = await _translationsGetterService.GetRatingOptiontranslationsByRatingOptionId(ratingOptionId);

            //Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion

        #region DeleteRatingOptionById Tests

        //TESTE: recebe um Guid nulo, logo deve retornar falso
        [Fact]
        public async Task DeleteRatingOptionTranslationById_RatingOptionTranslationIdIsNull_ShouldReturnFalse()
        {
            //Arrange
            Guid? ratingOptionTranslationId = null;

            //Act
            var result = await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionTranslationId);

            //Assert
            Assert.False(result);
        }

        //TESTE: recebe um Guid válido que não existe, logo deve retornar falso
        [Fact]
        public async Task DeleteRatingOptionTranslationById_RatingOptionTranslationIdIsValidAndDoesntExist_ShouldReturnFalse()
        {
            //Arrange
            var ratingOptionTranslationId = Guid.NewGuid();

            _translationsRepositoryMock
                .Setup(temp => temp.DeleteRatingOptionTranslationById(ratingOptionTranslationId))
                .ReturnsAsync(false);

            //Act
            var result = await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionTranslationId);

            //Assert
            Assert.False(result);
        }

        //TESTE: recebe um id válido que existe, logo retorna true
        [Fact]
        public async Task DeleteRatingOptionTranslationById_RatingOptionTranslationIdIsValidAndExists_ShouldReturnTrue()
        {
            //Arrange
            var ratingOptionTranslationId = Guid.NewGuid();

            _translationsRepositoryMock
                .Setup(temp => temp.DeleteRatingOptionTranslationById(ratingOptionTranslationId))
                .ReturnsAsync(true);

            //Act
            var result = await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionTranslationId);

            //Assert
            Assert.True(result);
        }

        #endregion
    }
}
