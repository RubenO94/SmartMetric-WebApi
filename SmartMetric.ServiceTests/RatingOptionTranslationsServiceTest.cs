using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
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

        private readonly IRatingOptionGetterService _ratingOptionGetterService;

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
            _translationsGetterService = new RatingOptionTranslationsGetterService(_translationsRepository, _ratingOptionGetterService, GetterLoggerMock.Object);
            _translationsDeleterService = new RatingOptionTranslationDeleterService(_translationsRepository, _ratingOptionGetterService, DeleterLoggerMock.Object);
        }

        #region AddRatingOptionTranslation Tests

        //TESTE: Fornecido um objeto RatingOptionTranslationDTOAddRequest como null, deve lançar um HttpStatusException
        [Fact]
        public async Task AddRatingOptionTranslation_NullTranslation_ShouldThrowHttpStatusException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um objeto RatingOptionTranslationDTOAddRequest com campo Language nulo, deve lançar um HttpStatusException
        [Fact]
        public async Task AddRatingOptionTranslation_LanguageIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest request = new RatingOptionTranslationDTOAddRequest
            {
                RatingOptionId = Guid.NewGuid(),
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um objeto RatingOptionTranslationDTOAddRequest com campo RatingOptionId nulo, deve lançar um HttpStatusException
        [Fact]
        public async Task AddRatingOptionTranslation_RatingOptionIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest request = new RatingOptionTranslationDTOAddRequest
            {
                Language = Language.PT,
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um objeto RatingOptionTranslationDTOAddRequest com campo Description nulo, deve lançar um HttpStatusException
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
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um objeto RatingOptionTranslationDTOAddRequest com detalhes corretos, deve inserir o objeto na lista de traduções e retornar um objeto RatingOptionTranslationDTOResponse que inclua o recente RatingOptionTranslation Id gerado.
        [Fact]
        public async Task AddRatingOptionTranslation_FullDetails_ToBeArgumentNullException()
        {
            //Arrange
            RatingOptionTranslationDTOAddRequest? request = _fixture.Build<RatingOptionTranslationDTOAddRequest>().With(temp => temp.Language, Language.PT).Create();

            RatingOptionTranslation translation = request.ToRatingOptionTranslation();
            RatingOptionTranslationDTOResponse response = translation.ToRatingOptionTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.AddRatingOptionTranslation(It.IsAny<RatingOptionTranslation>())).ReturnsAsync(translation);

            //Act
            ApiResponse<RatingOptionTranslationDTOResponse?> response_from_add = await _translationsAdderService.AddRatingOptionTranslation(request);
            response.RatingOptionTranslationId = response_from_add.Data!.RatingOptionTranslationId;

            //Assert
            response_from_add.Data.Should().NotBe(Guid.Empty);
            response_from_add.Data.Should().Be(response);
        }

        #endregion

        #region GetAllRatingOptionTranslation Tests

        //TESTE: GetAllRatingOptionTranslations por defeito deve retornar uma lista vazia;
        [Fact]
        public async Task GetAllRatingOptionTranslation_ToBeEmptyList()
        {
            //Arrange
            var translations = new List<RatingOptionTranslation>();
            _translationsRepositoryMock.Setup(temp => temp.GetAllRatingOptionTranslations()).ReturnsAsync(translations);

            //Act
            ApiResponse<List<RatingOptionTranslationDTOResponse>> responseFromGet = await _translationsGetterService.GetAllRatingOptionTranslations();

            //Assert
            responseFromGet.Data.Should().BeEmpty();
        }

        //TESTE: Simular uma adição de algumas traduções e chamar GetAllRatingOptionTranslations, deve retornar as mesmas traduções que foram adicionadas.
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
            ApiResponse<List<RatingOptionTranslationDTOResponse>> actualResponse = await _translationsGetterService.GetAllRatingOptionTranslations();

            //Log of the actual response
            _testOutputHelper.WriteLine("Actual Response:");
            foreach (var item in actualResponse.Data!)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            //Assert
            actualResponse.Data.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion

        #region GetRatingOptionTranslationById Tests

        //TESTE: Fornecido um RatingOptionTranslationId nulo, deve lançar um HttpStatusException
        [Fact]
        public async Task GetRatingOptionTranslationsById_NullRatingOptionTranslationId_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? id = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptionTranslationById(id);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um RatingOptionTranslationId válido, mas sem tradução associada, deve retornar null como RatingOptionTranslationDTOResponse
        [Fact]
        public async Task GetRatingOptionTranslationsById_WithvalidId_NoTranslation_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid ratingOptionTranslationId = Guid.NewGuid();

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptionTranslationById(ratingOptionTranslationId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um RatingOptionTranslationId válido, com tradução associada, deve retornar a tradução correspondente
        [Fact]
        public async Task GetRatingOptionTranslationsById_WithValidId_ToBeSuccessful()
        {
            //Arrange
            RatingOptionTranslation translation = _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOption, null as RatingOption).Create();

            RatingOptionTranslationDTOResponse expectedResponse = translation.ToRatingOptionTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationById(It.IsAny<Guid>())).ReturnsAsync(translation);

            //Act
            ApiResponse<RatingOptionTranslationDTOResponse?> actualResponse = await _translationsGetterService.GetRatingOptionTranslationById(translation.RatingOptionTranslationId);

            //Assert
            actualResponse.Data.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion

        #region GetRatingOptionTranslationByRatingOptionId Tests

        //TESTE: Fornecido um RatingOptionId nulo, deve lançar um HttpStatusException
        [Fact]
        public async Task GetRatingOptionTranslationsByRatingOptionId_NullRatingOptionId_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? ratingOptionId = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptionTranslationsByRatingOptionId(ratingOptionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um RatingOptionId válido, mas sem traduções associadas, deve retornar uma lista vazia de RatingOptionTranslationDTOResponse
        [Fact]
        public async Task GetRatingOptionTranslationsByRatingOptionId_WithValidId_NoTranslations_ToBeEmptyList()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();

            List<RatingOptionTranslation> translations = new()
            {
                new RatingOptionTranslation() {}
            };

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationByRatingOptionId(It.IsAny<Guid>())).ReturnsAsync(translations);

            //Act
            ApiResponse<List<RatingOptionTranslationDTOResponse>?> response = await _translationsGetterService.GetRatingOptionTranslationsByRatingOptionId(ratingOptionId);

            //Assert
            response.Data.Should().BeEmpty();
        }

        //TESTE: Fornecido um RatingOptionId válido, com algumas traduções associadas, deve retornar a lista de traduções correspondentes
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
            ApiResponse<List<RatingOptionTranslationDTOResponse>?> actualResponse = await _translationsGetterService.GetRatingOptionTranslationsByRatingOptionId(ratingOptionId);

            //Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion

        #region DeleteRatingOptionById Tests

        //TESTE: recebe um Guid nulo, logo deve lançar HttpStatusException
        [Fact]
        public async Task DeleteRatingOptionTranslationById_RatingOptionTranslationIdIsNull_ShouldReturnFalse()
        {
            //Arrange
            Guid? ratingOptionId = null;
            Language language = Language.EN;

            //Act
            async Task Act() => await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);

            //Assert
            await Assert.ThrowsAsync<HttpStatusException>(Act);
        }

        //TESTE: recebe um Guid válido que não existe, logo deve retornar falso
        //[Fact]
        //public async Task DeleteRatingOptionTranslationById_RatingOptionTranslationIdIsValidAndDoesntExist_ShouldReturnFalse()
        //{
        //    //Arrange
        //    var ratingOptionTranslationId = Guid.NewGuid();

        //    _translationsRepositoryMock
        //        .Setup(temp => temp.DeleteRatingOptionTranslationById(ratingOptionTranslationId))
        //        .ReturnsAsync(false);

        //    //Act
        //    var result = await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionTranslationId);

        //    //Assert
        //    Assert.False(result);
        //}

        //TESTE: recebe um id válido que existe, logo retorna true
        //[Fact]
        //public async Task DeleteRatingOptionTranslationById_RatingOptionTranslationIdIsValidAndExists_ShouldReturnTrue()
        //{
        //    //Arrange
        //    var ratingOptionTranslationId = Guid.NewGuid();

        //    _translationsRepositoryMock
        //        .Setup(temp => temp.DeleteRatingOptionTranslationById(ratingOptionTranslationId))
        //        .ReturnsAsync(true);

        //    //Act
        //    var result = await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionTranslationId);

        //    //Assert
        //    Assert.True(result);
        //}

        #endregion
    }
}