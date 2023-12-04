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
using SmartMetric.Core.Services.RatingOptions;
using SmartMetric.Core.ServicesContracts.RatingOptions;
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

        private readonly Mock<IQuestionRepository> _questionRepositoryMock;
        private readonly IQuestionRepository _questionRepository;

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
            _questionRepository = _questionRepositoryMock.Object;

            var AdderLoggerMock = new Mock<ILogger<RatingOptionAdderService>>();
            var GetterLoggerMock = new Mock<ILogger<RatingOptionGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<RatingOptionDeleterService>>();

            _ratingOptionAdderService = new RatingOptionAdderService(_ratingOptionRepository, _questionRepository, AdderLoggerMock.Object);
            _ratingOptionGetterService = new RatingOptionGetterService(_ratingOptionRepository, GetterLoggerMock.Object);
            _ratingOptionDeleterService = new RatingOptionDeleterService(_ratingOptionRepository, DeleterLoggerMock.Object);
        }

        #region AddRatingOption Tests

        //TESTE: fornecido um ratingOptionDTOAddRequest nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_ObjectIsNull_ShouldThrowException()
        {
            //Arrange
            Guid ratingOption = Guid.NewGuid();
            RatingOptionDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(ratingOption, request);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //TESTE: fornecido um ratingOptionDTOAddRequest com campo QuestionId nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_QuestionIdIsNull_ShouldThrowException()
        {
            //Arrange
            Guid? questionId = null;
            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.PT,
                        Description = "descricao"
                    }
                }
            };

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um ratingOptionDTOAddRequest com campo NumericValue nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_NumericValueIsNull_ShouldThrowException()
        {
            //Arrange
            var questionId = Guid.NewGuid();

            RatingOptionDTOAddRequest request = new()
            {
                
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.PT,
                        Description = "descricao"
                    }
                }
            };

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um ratingOptionDTOAddRequest com campo Translations nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_TranslationIsNull_ShouldThrowException()
        {
            //Arrange
            var questionId = Guid.NewGuid();
            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = null
            };

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um ratingOptionDTOAddRequest com campo Translations vazio, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_TranslationIsEmpty_ShouldThrowException()
        {
            //Arrange
            var questionId = Guid.NewGuid();
            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = new List<TranslationDTOAddRequest>()
            };

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um ratingOptionDTOAddRequest com campo Translations com campo Language nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_TranslationLanguageIsNull_ShouldThrowException()
        {
            //Arrange
            var questionId = Guid.NewGuid();

            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = null,
                        Description = "descricaosssssss"
                    }
                }
            };

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um ratingOptionDTOAddRequest com campo Translations com campo Description nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_TranslationDescriptionIsNull_ShouldThrowException()
        {
            //Arrange
            var questionId = Guid.NewGuid();

            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.PT,
                        Description = null
                    }
                }
            };

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um ratingOptionDTOAddRequest com campo Translations com campo Description nulo, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_TranslationDescriptionIsEmpty_ShouldThrowException()
        {
            //Arrange
            var questionId = Guid.NewGuid();

            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.PT,
                        Description = ""
                    }
                }
            };

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um ratingOptionAddRequest válido mas é fornecido um questionId válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_QuestionIdIsValidButDoesntExist_ShouldThrowException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.PT,
                        Description = "descricao"
                    }
                }
            };

            _questionRepositoryMock.Setup(temp => temp.GetQuestionById(questionId)).ReturnsAsync(null as Question);

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um ratingOptionAddRequest válido mas é fornecido um questionId de uma questão que não é do tipo Rating, deve lançar exceção
        [Fact]
        public async Task AddRatingOption_QuestionIdIsValidButNotRatingQuestion_ShouldThrowException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            Question question = new()
            {
                QuestionId = questionId,
                ResponseType = ResponseType.Text.ToString(),
                IsRequired = false,
                Translations = new List<QuestionTranslation>
                {
                    new QuestionTranslation
                    {
                        QuestionTranslationId = Guid.NewGuid(),
                        QuestionId = questionId,
                        Language = Language.EN.ToString(),
                        Title = "Title",
                        Description = "Description"
                    }
                }
            };

            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.PT,
                        Description = "descricao"
                    }
                }
            };

            _questionRepositoryMock.Setup(temp => temp.GetQuestionById(questionId)).ReturnsAsync(question);

            //Act
            Func<Task> action = async () => await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um ratingOptionAddRequest válido e um questionId válido, retorna sucesso
        [Fact]
        public async Task AddRatingOption_ShouldBeSuccessful()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();
            Guid ratingOptionId = Guid.NewGuid();

            Question question = new()
            {
                QuestionId = questionId,
                ResponseType = ResponseType.Rating.ToString(),
                IsRequired = false,
                Translations = new List<QuestionTranslation>
                {
                    new QuestionTranslation
                    {
                        QuestionTranslationId = Guid.NewGuid(),
                        QuestionId = questionId,
                        Language = Language.EN.ToString(),
                        Title = "Title",
                        Description = "Description"
                    }
                }
            };

            RatingOptionDTOAddRequest request = new()
            {
                NumericValue = 1,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.PT,
                        Description = "descricao"
                    }
                }
            };

            RatingOption ratingOption = request.ToRatingOption();
            RatingOptionDTOResponse response = ratingOption.ToRatingOptionDTOResponse();

            _questionRepositoryMock.Setup(temp => temp.GetQuestionById(questionId)).ReturnsAsync(question);

            _ratingOptionRepositoryMock.Setup(temp => temp.AddRatingOption(ratingOption)).ReturnsAsync(new RatingOption { RatingOptionId = ratingOptionId});

            //Act
            var result = await _ratingOptionAdderService.AddRatingOption(questionId, request);

            //Assert
            Assert.NotNull(result.Data);
            Assert.IsType<RatingOptionDTOResponse>(result.Data);
            result.Data.Equals(response);
        }

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

        //TESTE: recebe um id da questão, válido, mas que não existe
        [Fact]
        public async Task GetRatingOptionByQuestionId_QuestionIdIsValidButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionByQuestionId(It.IsAny<Guid>())).ReturnsAsync(null as List<RatingOption>);

            //Act
            Func<Task> action = async () => await _ratingOptionGetterService.GetRatingOptionsByQuestionId(questionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: recebe um id da questão, válido, mas não tem opções de resposta de classificação para essa pergunta
        [Fact]
        public async Task GetRatingOptionByQuestionId_QuestionIdIsValidWithNoRatingOption_ShouldBeEmptyList()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            _ratingOptionRepositoryMock.Setup(temp => temp.GetRatingOptionByQuestionId(It.IsAny<Guid>())).ReturnsAsync(new List<RatingOption>());

            //Act
            var result = await _ratingOptionGetterService.GetRatingOptionsByQuestionId(questionId);

            //Assert
            result.Data.Should().BeEmpty();
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