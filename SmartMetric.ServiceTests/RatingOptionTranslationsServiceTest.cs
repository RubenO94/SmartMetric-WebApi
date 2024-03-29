﻿using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.Services.RatingOptionTranslations;
using SmartMetric.Core.ServicesContracts.RatingOptionTranslations;
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

        private readonly Mock<IRatingOptionRepository> _ratingOptionRepositoryMock;
        private readonly IRatingOptionRepository _ratingOptionRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        //constructor
        public RatingOptionTranslationsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;

            _translationsRepositoryMock = new Mock<IRatingOptionTranslationsRepository>();
            _translationsRepository = _translationsRepositoryMock.Object;

            _ratingOptionRepositoryMock = new Mock<IRatingOptionRepository>();
            _ratingOptionRepository = _ratingOptionRepositoryMock.Object;

            var AdderLoggerMock = new Mock<ILogger<RatingOptionTranslationsAdderService>>();
            var GetterLoggerMock = new Mock<ILogger<RatingOptionTranslationsGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<RatingOptionTranslationDeleterService>>();

            _translationsAdderService = new RatingOptionTranslationsAdderService(_translationsRepository, _ratingOptionRepository, AdderLoggerMock.Object);
            _translationsGetterService = new RatingOptionTranslationsGetterService(_translationsRepository, GetterLoggerMock.Object);
            _translationsDeleterService = new RatingOptionTranslationDeleterService(_translationsRepository, _ratingOptionRepository, DeleterLoggerMock.Object);
        }

        #region AddRatingOptionTranslation Tests

        //TESTE: fornecido um TranslationDTOAddRequest nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOptionTranslation_ObjectIsNull_ShouldThrowException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();
            TranslationDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(questionId, request);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
        //TESTE: fornecido um TranslationDTOAddRequest com campo Language nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOptionTranslation_LanguageIsNull_ShouldThrowException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();
            Guid ratingOptionId = Guid.NewGuid();

            TranslationDTOAddRequest request = new()
            {
                Language = null,
                Description = "description",
            };

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(questionId, request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um TranslationDTOAddRequest com campo Description nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOptionTranslation_DescriptionIsNull_ShouldThrowException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();
            Guid ratingOptionId = Guid.NewGuid();

            TranslationDTOAddRequest request = new()
            {
                Language = Language.PT,
                Description = null,
            };

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(questionId, request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um TranslationDTOAddRequest com campo Description vazio, deve lançar exceção
        [Fact]
        public async Task AddRatingOptionTranslation_DescriptionIsEmpty_ShouldThrowException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();
            Guid ratingOptionId = Guid.NewGuid();

            TranslationDTOAddRequest request = new()
            {
                Language = Language.EN,
                Description = "",
            };

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(questionId, request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um TranslationDTOAddRequest com campo RatingOptionId nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOptionTranslation_RatingOptionIdIsNull_ShouldThrowException()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();

            TranslationDTOAddRequest request = new()
            {
                Language = Language.EN,
                Description = "descricao",
            };

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(ratingOptionId, request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();

        }

        //TESTE: fornecido um TranslationDTOAddRequest com campo RatingOptionId válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task AddRatingOptionTranslation_RatingOptionIdIsValidButDoesntExist_ShouldThrowException()
        {
            //Arrange
            Guid ratingOptionNonExistingId = Guid.NewGuid();
            Guid ratingOptionId = Guid.NewGuid();

            List<RatingOptionTranslation> ratingOptionTranslationExists = new()
            {
                new RatingOptionTranslation
                {
                    RatingOptionTranslationId = Guid.NewGuid(),
                    RatingOptionId = ratingOptionId,
                    Language = Language.EN.ToString(),
                    Description = "description"
                }
            };

            RatingOption ratingOption = new()
            {
                RatingOptionId = ratingOptionId,
                QuestionId = Guid.NewGuid(),
                NumericValue = 1,
                Translations = ratingOptionTranslationExists
            };

            TranslationDTOAddRequest request = new()
            {
                Language = Language.PT,
                Description= "description"
            };

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionById(ratingOptionNonExistingId)).ReturnsAsync(null as RatingOption);

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(ratingOptionNonExistingId, request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um TranslationDTOAddRequest válido mas idioma já existe, deve lançar exceção
        [Fact]
        public async Task AddRatingOptionTranslation_LanguageAlreadyExists_ShouldThrowException()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();

            List<RatingOptionTranslation> ratingOptionTranslationExists = new()
            {
                new RatingOptionTranslation
                {
                    RatingOptionTranslationId = Guid.NewGuid(),
                    RatingOptionId = ratingOptionId,
                    Language = Language.EN.ToString(),
                    Description = "description"
                }
            };

            RatingOption ratingOption = new()
            {
                RatingOptionId = ratingOptionId,
                QuestionId = Guid.NewGuid(),
                NumericValue = 1,
                Translations = ratingOptionTranslationExists
            };

            TranslationDTOAddRequest request = new()
            {
                Language = Language.EN,
                Description = "description"
            };

            var ratingOptionTranslation = request.ToRatingOptionTranslation();
            var response = ratingOptionTranslation.ToTranslationDTOResponse();

            _ratingOptionRepositoryMock
                .Setup(temp => temp.GetRatingOptionById(ratingOptionId))
                .ReturnsAsync(ratingOption);

            _translationsRepositoryMock
                .Setup(temp => temp.GetRatingOptionTranslationByRatingOptionId(ratingOptionId))
                .ReturnsAsync(ratingOptionTranslationExists);

            _translationsRepositoryMock
                .Setup(temp => temp.AddRatingOptionTranslation(ratingOptionTranslation))
                .ReturnsAsync(ratingOptionTranslation);

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddRatingOptionTranslation(ratingOptionId,request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um TranslationDTOAddRequest válido, adiciona com sucesso, retorna mensagem
        [Fact]
        public async Task AddRatingOptionTranslation_ShouldBeSuccessful()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();

            List<RatingOptionTranslation> ratingOptionTranslationExists = new()
            {
                new RatingOptionTranslation
                {
                    RatingOptionTranslationId = Guid.NewGuid(),
                    RatingOptionId = ratingOptionId,
                    Language = Language.EN.ToString(),
                    Description = "description"
                }
            };

            RatingOption ratingOption = new()
            {
                RatingOptionId = ratingOptionId,
                QuestionId = Guid.NewGuid(),
                NumericValue = 1,
                Translations = ratingOptionTranslationExists
            };

            TranslationDTOAddRequest request = new()
            {
                Language = Language.PT,
                Description = "descricao exemplo"
            };

            var ratingOptionTranslation = request.ToRatingOptionTranslation();
            var response = ratingOptionTranslation.ToTranslationDTOResponse();

            _ratingOptionRepositoryMock
                .Setup(temp => temp.GetRatingOptionById(ratingOptionId))
                .ReturnsAsync(ratingOption);

            _translationsRepositoryMock
                .Setup(temp => temp.GetRatingOptionTranslationByRatingOptionId(ratingOptionId))
                .ReturnsAsync(ratingOptionTranslationExists);

            _translationsRepositoryMock
                .Setup(temp => temp.AddRatingOptionTranslation(ratingOptionTranslation))
                .ReturnsAsync(ratingOptionTranslation);

            //Act
            var result = await _translationsAdderService.AddRatingOptionTranslation(ratingOptionId, request);

            //Assert
            result.Data.Should().Be(response);
        }

        #endregion

        #region GetAllRatingOptionTranslation Tests

        //TESTE: retornar uma lista vazia
        [Fact]
        public async Task GetAllRatingOptionTranslation_ShouldBeEmptyList()
        {
            //Arrange
            List<RatingOptionTranslation> translations = new List<RatingOptionTranslation>();

            _translationsRepositoryMock.Setup(temp => temp.GetAllRatingOptionTranslations()).ReturnsAsync(translations);

            //Act
            ApiResponse<List<TranslationDTOResponse>> responseFromGet = await _translationsGetterService.GetAllRatingOptionTranslations();

            //Assert
            responseFromGet.Data.Should().BeEmpty();
        }

        //TESTE: retornar uma lista
        [Fact]
        public async Task GetAllRatingOptionTranslation_ShouldBeSuccessful()
        {
            //Arrange
            List<RatingOptionTranslation> translations = new()
            {
                _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOption, null as RatingOption).Create(),
                _fixture.Build<RatingOptionTranslation>().With(temp => temp.RatingOption, null as RatingOption).Create(),
            };

            List<TranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToTranslationDTOResponse()).ToList();

            _translationsRepositoryMock.Setup(temp => temp.GetAllRatingOptionTranslations()).ReturnsAsync(translations);

            //Act
            ApiResponse<List<TranslationDTOResponse>> actualResponse = await _translationsGetterService.GetAllRatingOptionTranslations();

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetRatingOptionTranslationById Tests

        //TESTE: fornecido um Guid nulo, deve lançar exceção
        [Fact]
        public async Task GetRatingOptionTranslationById_RatingOptionTranslationIdIsNull_ShouldThrowException()
        {
            //Arrange
            Guid? ratingOptionTranslationId = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptionTranslationById(ratingOptionTranslationId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task GetRatingOptionTranslationById_RatingOptionTranslationIdIsValidButDoesntExist_ShouldThrowException()
        {
            //Arrange
            Guid ratingOptionTranslationId = Guid.NewGuid();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationById(ratingOptionTranslationId)).ReturnsAsync(null as RatingOptionTranslation);

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptionTranslationById(ratingOptionTranslationId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, deve lançar exceção
        [Fact]
        public async Task GetRatingOptionTranslationsById_ToBeSuccessful()
        {
            //Arrange
            Guid ratingOptionTranslationId = Guid.NewGuid();

            RatingOptionTranslation ratingOptionTranslation = new()
            {
                RatingOptionTranslationId = ratingOptionTranslationId,
                RatingOptionId = Guid.NewGuid(),
                Language = Language.PT.ToString(),
                Description = "Description",
            };

            TranslationDTOResponse expectedResponse = ratingOptionTranslation.ToTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationById(ratingOptionTranslationId)).ReturnsAsync(ratingOptionTranslation);

            //Act
            ApiResponse<TranslationDTOResponse?> actualResponse = await _translationsGetterService.GetRatingOptionTranslationById(ratingOptionTranslationId);

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetRatingOptionTranslationByRatingOptionId Tests

        //TESTE: Fornecido um RatingOptionId nulo, deve lançar um HttpStatusException
        [Fact]
        public async Task GetRatingOptionTranslationsByRatingOptionId_RatingOptionIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? ratingOptionId = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptionTranslationsByRatingOptionId(ratingOptionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um RatingOptionId válido, que não existe, deve lançar exceção
        [Fact]
        public async Task GetRatingOptionTranslationsByRatingOptionId_RatingOptionIdIsValidButDoesntExist_ShouldThrowException()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationByRatingOptionId(ratingOptionId))!.ReturnsAsync(null as List<RatingOptionTranslation>);

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetRatingOptionTranslationsByRatingOptionId(ratingOptionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um RatingOptionId válido, mas sem traduções associadas, deve retornar uma lista vazia de TranslationDTOResponse
        [Fact]
        public async Task GetRatingOptionTranslationsByRatingOptionId_RatingOptionIdIsValidWithNoTranslations_ToBeEmptyList()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationByRatingOptionId(ratingOptionId))!.ReturnsAsync(new List<RatingOptionTranslation>());

            //Act
            var result = await _translationsGetterService.GetRatingOptionTranslationsByRatingOptionId(ratingOptionId);

            //Assert
            result.Data.Should().BeEmpty();
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

            List<TranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToTranslationDTOResponse()).ToList();

            _translationsRepositoryMock.Setup(temp => temp.GetRatingOptionTranslationByRatingOptionId(It.IsAny<Guid>())).ReturnsAsync(translations);

            //Act
            ApiResponse<List<TranslationDTOResponse>?> actualResponse = await _translationsGetterService.GetRatingOptionTranslationsByRatingOptionId(ratingOptionId);

            //Assert
            actualResponse.Data.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion

        #region DeleteRatingOptionById Tests

        //TESTE: fornecido um Guid nulo, deve lançar exceção
        [Fact]
        public async Task DeleteRatingOptionTranslationById_RatingOptionIdIsNull_ShouldThrowException()
        {
            //Arrange
            Guid? ratingOptionId = null;
            Language language = Language.PT;

            //Act
            Func<Task> action = async () => await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task DeleteRatingOptionTranslationById_RatingOptionIdIsValidButDoesntExist_ShouldThrowException()
        {
            //Arrange
            Guid? ratingOptionId = Guid.NewGuid();
            Language language = Language.PT;

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionById(ratingOptionId)).ReturnsAsync(null as RatingOption);

            //Act
            Func<Task> action = async () => await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, não pode remover pois só existe um idioma, logo deve lançar exceção
        [Fact]
        public async Task DeleteRatingOptionTranslationById_RatingOptionIdIsValidButOnlyOneTranslation_ShouldThrowException()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();
            Language language = Language.PT;

            RatingOption ratingOption = new()
            {
                RatingOptionId = ratingOptionId,
                NumericValue = 1,
                QuestionId = Guid.NewGuid(),
                Translations = new List<RatingOptionTranslation>
                {
                    new RatingOptionTranslation
                    {
                        RatingOptionTranslationId = Guid.NewGuid(),
                        RatingOptionId = ratingOptionId,
                        Language = language.ToString(),
                        Description = "Description",
                    }
                }
            };

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionById(ratingOptionId)).ReturnsAsync(ratingOption);

            //Act
            Func<Task> action = async () => await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, language não existe, logo deve lançar exceção
        [Fact]
        public async Task DeleteRatingOptionTranslationById_LanguageDoesntExist_ShouldThrowException()
        {
            //Arrange
            Guid ratingOptionId = Guid.NewGuid();
            Language language = Language.PT;

            RatingOption ratingOption = new()
            {
                RatingOptionId = ratingOptionId,
                NumericValue = 1,
                QuestionId = Guid.NewGuid(),
                Translations = new List<RatingOptionTranslation>
                {
                    new RatingOptionTranslation
                    {
                        RatingOptionTranslationId = Guid.NewGuid(),
                        RatingOptionId = ratingOptionId,
                        Language = Language.EN.ToString(),
                        Description = "Description",
                    },
                    new RatingOptionTranslation
                    {
                        RatingOptionTranslationId = Guid.NewGuid(),
                        RatingOptionId = ratingOptionId,
                        Language = Language.FR.ToString(),
                        Description = "Description",
                    }
                }
            };

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionById(ratingOptionId)).ReturnsAsync(ratingOption);

            //Act
            Func<Task> action = async () => await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, logo sucesso
        [Fact]
        public async Task DeleteRatingOptionTranslationById_ShouldBeSuccessful()
        {
            //Arrange
            Guid ratingOptionTranslationId = Guid.NewGuid();
            Guid ratingOptionId = Guid.NewGuid();
            Language language = Language.EN;

            RatingOption ratingOption = new()
            {
                RatingOptionId = ratingOptionId,
                NumericValue = 1,
                QuestionId = Guid.NewGuid(),
                Translations = new List<RatingOptionTranslation>
                {
                    new RatingOptionTranslation
                    {
                        RatingOptionTranslationId = ratingOptionTranslationId,
                        RatingOptionId = ratingOptionId,
                        Language = Language.EN.ToString(),
                        Description = "Description",
                    },
                    new RatingOptionTranslation
                    {
                        RatingOptionTranslationId = Guid.NewGuid(),
                        RatingOptionId = ratingOptionId,
                        Language = Language.FR.ToString(),
                        Description = "Description",
                    }
                }
            };

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionById(ratingOptionId)).ReturnsAsync(ratingOption);

            _translationsRepositoryMock.Setup(temp => temp.DeleteRatingOptionTranslationById(ratingOptionTranslationId)).ReturnsAsync(true);

            //Act
            var result = await _translationsDeleterService.DeleteRatingOptionTranslationById(ratingOptionId, language);

            //Assert
            Assert.True(result.Data);
        }

        #endregion
    }
}