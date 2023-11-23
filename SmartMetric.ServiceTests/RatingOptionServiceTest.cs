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
    public class RatingOptionServiceTest
    {
        //variables
        private readonly IRatingOptionAdderService _ratingOptionAdderService;
        private readonly IRatingOptionGetterService _ratingOptionGetterService;
        private readonly IRatingOptionDeleterService _ratingOptionDeleterService;

        private readonly Mock<IRatingOptionRepository> _ratingOptionRepositoryMock;
        private readonly IRatingOptionRepository _ratingOptionRepository;

        private readonly IQuestionGetterService _questionGetterService;
        private readonly Mock<IQuestionRepository> _questionRepositoryMock;
        private readonly Mock<IQuestionGetterService> _questionGetterServiceMock;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        //constructor
        public RatingOptionServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;

            _ratingOptionRepositoryMock = new Mock<IRatingOptionRepository>();
            _ratingOptionRepository = _ratingOptionRepositoryMock.Object;

            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _questionGetterServiceMock = new Mock<IQuestionGetterService>();

            var AdderLoggerMock = new Mock<ILogger<RatingOptionAdderService>>();
            var GetterLoggerMock = new Mock<ILogger<RatingOptionGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<RatingOptionDeleterService>>();

            _ratingOptionAdderService = new RatingOptionAdderService(_ratingOptionRepository, _questionGetterService, AdderLoggerMock.Object);
            _ratingOptionGetterService = new RatingOptionGetterService(_ratingOptionRepository, GetterLoggerMock.Object);
            _ratingOptionDeleterService = new RatingOptionDeleterService(_ratingOptionRepository, DeleterLoggerMock.Object);
        }

        #region AddRatingOption Tests

        //        //TESTE: recebe um objeto null para adicionar uma opção de resposta de classificação à questão, deve lançar uma exceção
        //        [Fact]
        //        public async Task AddRatingOption_IsNull_ToThrowArgumentNullException()
        //        {
        //            //Arrange
        //            RatingOptionDTOAddRequest? request = null;

        //            //Act
        //            Func<Task> action = async () =>
        //            {
        //                await _ratingOptionAdderService.AddRatingOption(request);
        //            };

        //            //Assert
        //            await action.Should().ThrowAsync<ArgumentNullException>();
        //        }

        //        //TESTE: recebe uma nova opção de resposta de classificação com o campo QuestionId nulo
        //        [Fact]
        //        public async Task AddRatingOption_QuestionIdIsNull_ToThrowArgumentException()
        //        {
        //            //Arrange
        //            RatingOptionDTOAddRequest? request = new RatingOptionDTOAddRequest()
        //            {
        //                NumericValue = 1,
        //            };

        //            //Act
        //            Func<Task> action = async () =>
        //            {
        //                await _ratingOptionAdderService.AddRatingOption(request);
        //            };

        //            //Assert
        //            await action.Should().ThrowAsync<ArgumentException>();
        //        }

        //        //TESTE: recebe uma nova opção de resposta de classificação com o campo NumericValue nulo
        //        [Fact]
        //        public async Task AddRatingOption_NumericValueIsNull_ToThrowArgumentException()
        //        {
        //            //Arrange
        //            RatingOptionDTOAddRequest? request = new RatingOptionDTOAddRequest()
        //            {
        //                QuestionId = Guid.NewGuid(),
        //            };

        //            //Act
        //            Func<Task> action = async () =>
        //            {
        //                await _ratingOptionAdderService.AddRatingOption(request);
        //            };

        //            //Assert
        //            await action.Should().ThrowAsync<ArgumentException>();
        //        }

        //        //TESTE: recebe uma nova opção de resposta de classificação com todos os campos preenchidos
        //        [Fact]
        //        public async Task AddRatingOption_FullDetails_ToBeSuccessful()
        //        {
        //            //Arrange
        //            RatingOptionDTOAddRequest? request = _fixture.Build<RatingOptionDTOAddRequest>().Create();

        //            RatingOption translation = request.ToRatingOption();
        //            RatingOptionDTOResponse response = translation.ToRatingOptionDTOResponse();

        //            _ratingOptionRepositoryMock.Setup(temp => temp.AddRatingOption(It.IsAny<RatingOption>())).ReturnsAsync(translation);

        //            //Act
        //            RatingOptionDTOResponse response_from_add = await _ratingOptionAdderService.AddRatingOption(request);
        //            response.RatingOptionId = response_from_add.RatingOptionId;

        //            //Assert
        //            response_from_add.Should().NotBe(Guid.Empty);
        //            response_from_add.Should().Be(response);
        //        }

        #endregion

        #region GetAllRatingOption Tests

        //TESTE: retornar uma lista vazia
        [Fact]
        public async Task GetAllRatingOption_ToBeEmptyList()
        {
            //Arrange
            var translations = new List<RatingOption>();

            _ratingOptionRepositoryMock.Setup(temp => temp.GetAllRatingOption()).ReturnsAsync(translations);

            //Act
            ApiResponse<List<RatingOptionDTOResponse>> responseFromGet = await _ratingOptionGetterService.GetAllRatingOptions();

            //Assert
            responseFromGet.Data.Should().BeEmpty();
        }

        //TESTE: retornar uma lista de todas as opções de resposta de classificação de todas as questões disponíveis na base de dados
        [Fact]
        public async Task GetAllRatingOption_ToBeSuccessful()
        {
            //Arrange
            List<RatingOption> translations = new List<RatingOption>()
            {
                _fixture.Build<RatingOption>().With(temp => temp.Question, null as Question).With(temp => temp.Translations, null as List<RatingOptionTranslation>).Create(),
            };

            List<RatingOptionDTOResponse> expectedResponse = translations.Select(temp => temp.ToRatingOptionDTOResponse()).ToList();

            _ratingOptionRepositoryMock.Setup(temp => temp.GetAllRatingOption()).ReturnsAsync(translations);

            //Act
            ApiResponse<List<RatingOptionDTOResponse>> actualResponse = await _ratingOptionGetterService.GetAllRatingOptions();

            //Assert
            actualResponse.Data.Should().NotBeNull();
            actualResponse.Data.Should().NotBeEmpty();
            actualResponse.Data.Should().HaveSameCount(expectedResponse);
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetRatingOptionById Tests

        //TESTE: recebe um id da opção de resposta de classificação nulo, logo deve lançar exceção
        [Fact]
        public async Task GetRatingOptionById_RatingOptionIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? id = null;

            //Act
            Func<Task> action = async () => await _ratingOptionGetterService.GetRatingOptionById(id);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: recebe um ID de SingleChoiceOption válido mas que não existe
        [Fact]
        public async Task GetRatingOptionById_RatingOptionIdIsValidButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? ratingOptionId = Guid.NewGuid();

            //Act
            Func<Task> action = async () => await _ratingOptionGetterService.GetRatingOptionById(ratingOptionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: recebe um id da opção de resposta de classificação válido
        [Fact]
        public async Task GetRatingOptionById_RatingOptionIdIsValid_ToBeSuccessful()
        {
            //Arrange
            var questionId = Guid.NewGuid();
            var ratingOptionId = Guid.NewGuid();

            RatingOption ratingOption = new()
            {
                RatingOptionId = ratingOptionId,
                NumericValue = 1,
                QuestionId = questionId,
                Translations = new List<RatingOptionTranslation>
                {
                    new RatingOptionTranslation
                    {
                        RatingOptionTranslationId = Guid.NewGuid(),
                        RatingOptionId = ratingOptionId,
                        Language = Language.EN.ToString(),
                        Description = "description",
                    }
                }
            };

            RatingOptionDTOResponse expectedResponse = ratingOption.ToRatingOptionDTOResponse();

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionById(ratingOptionId)).ReturnsAsync(ratingOption);

            //Act
            ApiResponse<RatingOptionDTOResponse?> actualResponse = await _ratingOptionGetterService.GetRatingOptionById(ratingOptionId);

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetRatingOptionByQuestionId Tests

        //TESTE: recebe um id da questão nulo, deve retornar uma exceção
        [Fact]
        public async Task GetRatingOptionByQuestionId_QuestionIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? questionId = null;

            //Act
            Func<Task> action = async () => await _ratingOptionGetterService.GetRatingOptionsByQuestionId(questionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: recebe um id da questão, válido, mas não tem opções de resposta de classificação para essa pergunta
        [Fact]
        public async Task GetRatingOptionByQuestionId_QuestionIdIsValid_WithNoRatingOption_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionByQuestionId(It.IsAny<Guid>())).ReturnsAsync(new List<RatingOption>());

            //Act
            Func<Task> action = async () => await _ratingOptionGetterService.GetRatingOptionsByQuestionId(questionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: recebe um id da questão válido
        [Fact]
        public async Task GetRatingOptionByQuestionId_QuestionIdIsValid_ToBeSuccessful()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            List<RatingOption> rows = new List<RatingOption>()
            {
                _fixture.Build<RatingOption>().With(temp => temp.Question, null as Question).With(temp => temp.Translations, null as List<RatingOptionTranslation>).Create(),
            };

            List<RatingOptionDTOResponse> expectedResponse = rows.Select(temp => temp.ToRatingOptionDTOResponse()).ToList();

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionByQuestionId(It.IsAny<Guid>())).ReturnsAsync(rows);

            //Act
            ApiResponse<List<RatingOptionDTOResponse>?> actualResponse = await _ratingOptionGetterService.GetRatingOptionsByQuestionId(questionId);

            //Assert
            actualResponse.Data.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion

        #region DeleteRatingOptionById

        //TESTE: recebe um Guid nulo, logo deve retornar falso
        [Fact]
        public async Task DeleteRatingOptionById_RatingOptionIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? ratingOptionId = null;

            //Act
            async Task Act() => await _ratingOptionDeleterService.DeleteRatingOptionById(ratingOptionId);

            //Assert
            await Assert.ThrowsAsync<HttpStatusException>(Act);
        }

        //TESTE: recebe um id válido que não existe, logo deve retornar falso
        [Fact]
        public async Task DeleteRatingOptionById_RatingOptionIdIsValidAndDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            var ratingOptionId = Guid.NewGuid();

            _ratingOptionRepositoryMock
                .Setup(temp => temp.GetRatingOptionById(ratingOptionId))
                .ReturnsAsync(null as RatingOption);

            //Act
            Func<Task> action = async () => await _ratingOptionDeleterService.DeleteRatingOptionById(ratingOptionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: recebe um id válido que existe, logo retorna true
        [Fact]
        public async Task DeleteRatingOptionById_RatingOptionIdIsValidAndExists_ShouldReturnTrue()
        {
            //Arrange
            var ratingOptionId = Guid.NewGuid();

            var ratingOption = new RatingOption
            {
                RatingOptionId = ratingOptionId,
                QuestionId = Guid.NewGuid(),
                NumericValue = 1,
                Translations = new List<RatingOptionTranslation>
                {
                    new RatingOptionTranslation
                    {
                        RatingOptionTranslationId = Guid.NewGuid(),
                        RatingOptionId = ratingOptionId,
                        Language = Language.PT.ToString(),
                        Description = "Description",
                    }
                }
            };

            _ratingOptionRepositoryMock
                .Setup(temp => temp.GetRatingOptionById(ratingOptionId))
                .ReturnsAsync(ratingOption);

            _ratingOptionRepositoryMock
                .Setup(temp => temp.DeleteRatingOptionById(ratingOptionId))
                .ReturnsAsync(true);

            //Act
            var result = await _ratingOptionDeleterService.DeleteRatingOptionById(ratingOptionId);

            //Assert
            result.Data.Should().BeTrue();
            Assert.True(result.Data);
        }

        #endregion
    }
}