using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
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
    public class SingleChoiceOptionServiceTest
    {
        #region Variables

        private readonly ISingleChoiceOptionsAdderService _singleChoiceOptionAdderService;
        private readonly ISingleChoiceOptionGetterService _singleChoiceOptionGetterService;
        private readonly ISingleChoiceOptionDeleterService _singleChoiceOptionDeleterService;

        private readonly Mock<ISingleChoiceOptionRepository> _singleChoiceOptionRepositoryMock;
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;

        private readonly IQuestionGetterService _questionGetterService;
        private readonly Mock<IQuestionRepository> _questionRepositoryMock;
        private readonly Mock<IQuestionGetterService> _questionGetterServiceMock;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        #endregion

        #region Constructor

        public SingleChoiceOptionServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _fixture = new Fixture();

            _singleChoiceOptionRepositoryMock = new Mock<ISingleChoiceOptionRepository>();
            _singleChoiceOptionRepository = _singleChoiceOptionRepositoryMock.Object;

            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _questionGetterServiceMock = new Mock<IQuestionGetterService>();

            var AdderLoggerMock = new Mock<ILogger<SingleChoiceOptionAdderService>>();
            var GetterLoggerMock = new Mock<ILogger<SingleChoiceOptionGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<SingleChoiceOptionDeleterService>>();

            _singleChoiceOptionAdderService = new SingleChoiceOptionAdderService(_singleChoiceOptionRepository, _questionGetterService, AdderLoggerMock.Object);
            _singleChoiceOptionGetterService = new SingleChoiceOptionGetterService(_singleChoiceOptionRepository, GetterLoggerMock.Object);
            _singleChoiceOptionDeleterService = new SingleChoiceOptionDeleterService(_singleChoiceOptionRepository, _singleChoiceOptionGetterService, DeleterLoggerMock.Object);
        }

        #endregion

        #region AddSingleChoiceOption

        //TESTE: Fornecer um objeto SingleChoiceOptionDTOAddRequest como null, deve lançar um HttpStatusException
        [Fact]
        public async Task AddSingleChoiceOption_WithNullObject_ShouldThrowHttpStatusException()
        {
            //Arrange
            SingleChoiceOptionDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () =>
            {
                await _singleChoiceOptionAdderService.AddSingleChoiceOption(request);
            };

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecer um objeto SingleChoiceOptionDTOAddRequest com QuestionId nulo, deve lançar um HttpStatusException
        [Fact]
        public async Task AddSingleChoiceOption_WithQuestionIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            SingleChoiceOptionDTOAddRequest? request = new SingleChoiceOptionDTOAddRequest
            {
                QuestionId = null,
                Translations = new List<SingleChoiceOptionTranslationDTOAddRequest> 
                { 
                    new SingleChoiceOptionTranslationDTOAddRequest 
                    {
                        SingleChoiceOptionId = Guid.NewGuid(),
                        Language = Language.EN,
                        Description = "Description",
                    } 
                }
            };

            //Act
            Func<Task> action = async () =>
            {
                await _singleChoiceOptionAdderService.AddSingleChoiceOption(request);
            };

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecer um objeto SingleChoiceOptionDTOAddRequest com Translation nulo, deve lançar um HttpStatusException
        [Fact]
        public async Task AddSingleChoiceOption_WithTranslationNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            SingleChoiceOptionDTOAddRequest? request = new SingleChoiceOptionDTOAddRequest
            {
                QuestionId = Guid.NewGuid(),
                Translations = null
            };

            //Act
            Func<Task> action = async () => await _singleChoiceOptionAdderService.AddSingleChoiceOption(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecer um objeto SingleChoiceOptionDTOAddRequest com Translation nulo, deve lançar um HttpStatusException
        [Fact]
        public async Task AddSingleChoiceOption_WithTranslationEmpty_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            Question question = new Question
            {
                QuestionId = questionId,
                Translations = new List<QuestionTranslation>
                {
                    new QuestionTranslation
                    {
                        QuestionTranslationId = Guid.NewGuid(),
                        QuestionId = questionId,
                        Language = Language.EN.ToString(),
                        Description = "Question"
                    }
                }
            };

            SingleChoiceOptionDTOAddRequest? request = new SingleChoiceOptionDTOAddRequest
            {
                QuestionId = questionId,
                Translations = new List<SingleChoiceOptionTranslationDTOAddRequest>()
            };

            _questionRepositoryMock.Setup(temp => temp.GetQuestionById(questionId)).ReturnsAsync(question);

            //Act
            Func<Task> action = async () => await _singleChoiceOptionAdderService.AddSingleChoiceOption(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecer um objeto SingleChoiceOptionDTOAddRequest com um tipo de Question diferente de SingleChoice, deve lançar um HttpStatusException
        [Fact]
        public async Task AddSingleChoiceOption_WithQuestionTypeInvalid_ShouldThrowHttpStatusException()
        {
            //Arrange
            var questionId = Guid.NewGuid();

            Question question = new()
            { 
                QuestionId = questionId,
                ResponseType = ResponseType.Rating.ToString(),
                Translations = new List<QuestionTranslation>
                {
                    new QuestionTranslation
                    {
                        QuestionTranslationId = Guid.NewGuid(),
                        QuestionId = questionId,
                        Language = Language.EN.ToString(),
                        Description = "Question"
                    }
                }
            };

            SingleChoiceOptionDTOAddRequest? request = new SingleChoiceOptionDTOAddRequest
            {
                QuestionId = questionId,
                Translations = new List<SingleChoiceOptionTranslationDTOAddRequest>
                {
                    new SingleChoiceOptionTranslationDTOAddRequest
                    {
                        SingleChoiceOptionId = Guid.NewGuid(),
                        Language = Language.EN,
                        Description = "Description",
                    }
                }
            };

            _questionRepositoryMock.Setup(temp => temp.GetQuestionById(questionId)).ReturnsAsync(question);

            //Act
            Func<Task> action = async () => await _singleChoiceOptionAdderService.AddSingleChoiceOption(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecer um objeto SingleChoiceOptionDTOAddRequest válido
        [Fact]
        public async Task AddSingleChoiceOption_FullDetails_ToBeSuccessful()
        {
            //Arrange
            var singleChoiceOptionId = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            var question = new Question
            {
                ResponseType = ResponseType.SingleChoice.ToString(),
                Translations = new List<QuestionTranslation>
                {
                    new QuestionTranslation
                    {
                        Language = Language.EN.ToString(),
                        Description = "Description",
                    }
                }
            };

            SingleChoiceOptionDTOAddRequest request = new()
            {
                QuestionId = question.QuestionId,
                Translations = new List<SingleChoiceOptionTranslationDTOAddRequest>
                {
                    new SingleChoiceOptionTranslationDTOAddRequest
                    {
                        Language = Language.EN,
                        Description = "description"
                    }
                }
            };

            //SingleChoiceOptionDTOAddRequest request = _fixture
            //    .Build<SingleChoiceOptionDTOAddRequest>()
            //    .With(temp => temp.Translations, null as List<SingleChoiceOptionTranslationDTOAddRequest>)
            //    .Create();

            SingleChoiceOption singleChoiceOption = request.ToSingleChoiceOption();
            SingleChoiceOptionDTOResponse singleChoiceOptionResponse = singleChoiceOption.ToSingleChoiceOptionDTOResponse();

            _questionGetterServiceMock.Setup(temp => temp.GetQuestionById(request.QuestionId)).ReturnsAsync(new ApiResponse<QuestionDTOResponse?> { Data = question.ToQuestionDTOResponse() });
            _singleChoiceOptionRepositoryMock.Setup(temp => temp.AddSingleChoiceOption(It.IsAny<SingleChoiceOption>())).ReturnsAsync(singleChoiceOption);

            //Act
            ApiResponse<SingleChoiceOptionDTOResponse?> response_from_add = await _singleChoiceOptionAdderService.AddSingleChoiceOption(request);
            singleChoiceOptionResponse.SingleChoiceOptionId = response_from_add.Data!.SingleChoiceOptionId;

            //Assert
            response_from_add.Data.Should().NotBe(Guid.Empty);
            response_from_add.Data.Should().BeEquivalentTo(singleChoiceOptionResponse);
        }

        #endregion

        #region GetAllSingleChoiceOption

        //TESTE: GetAllSingleChoiceOption por defeito deve retornar uma lista vazia;
        [Fact]
        public async Task GetAllSingleChoiceOption_ToBeEmptyList()
        {
            //Arrange
            var singleChoiceOption = new List<SingleChoiceOption>();

            _singleChoiceOptionRepositoryMock.Setup(temp => temp.GetAllSingleChoiceOptions()).ReturnsAsync(singleChoiceOption);

            //Act
            ApiResponse<List<SingleChoiceOptionDTOResponse>> response = await _singleChoiceOptionGetterService.GetAllSingleChoiceOption();

            //Assert
            response.Data.Should().BeEmpty();
        }

        //TESTE: Simular uma adição de algumas traduções e chamar GetAllSingleChoiceOption, deve retornar as mesmas traduções que foram adicionadas.
        [Fact]
        public async Task GetAllSingleChoiceOption_WithSomeTranslations_ToBeSuccessful()
        {
            //Arrange
            var questionId = Guid.NewGuid();
            var firstSingleChoiceOptionId = Guid.NewGuid();
            var secondSingleChoiceOptionId = Guid.NewGuid();

            List<SingleChoiceOptionTranslation> translationsForFirst = new List<SingleChoiceOptionTranslation>
            {
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = firstSingleChoiceOptionId, Language = "PT", Description = "Sample Description" },
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = firstSingleChoiceOptionId, Language = "EN", Description = "Sample Description" },
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = firstSingleChoiceOptionId, Language = "ES", Description = "Sample Description" }
            };

            List<SingleChoiceOptionTranslation> translationsForSecond = new List<SingleChoiceOptionTranslation>
            {
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = secondSingleChoiceOptionId, Language = "PT", Description = "Sample Description" },
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = secondSingleChoiceOptionId, Language = "EN", Description = "Sample Description" },
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = secondSingleChoiceOptionId, Language = "ES", Description = "Sample Description" }
            };

            List<SingleChoiceOption> singleChoiceOptions = new List<SingleChoiceOption>
            {
                new SingleChoiceOption { SingleChoiceOptionId = firstSingleChoiceOptionId, QuestionId = questionId, Translations = translationsForFirst },
                new SingleChoiceOption { SingleChoiceOptionId = secondSingleChoiceOptionId, QuestionId = questionId, Translations = translationsForSecond }
            };

            List<SingleChoiceOptionDTOResponse> expectedResponse = singleChoiceOptions.Select(temp => temp.ToSingleChoiceOptionDTOResponse()).ToList();

            _singleChoiceOptionRepositoryMock.Setup(temp => temp.GetAllSingleChoiceOptions()).ReturnsAsync(singleChoiceOptions);

            //Act
            ApiResponse<List<SingleChoiceOptionDTOResponse>> actualResponse = await _singleChoiceOptionGetterService.GetAllSingleChoiceOption();

            //Assert
            actualResponse.Data.Should().HaveSameCount(expectedResponse);
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetSingleChoiceOptionById

        //TESTE: Fornecido um SingleChoiceOptionTranslationId nulo, deve lançar um ArgumentNullException
        [Fact]
        public async Task GetSingleChoiceOptionById_WithNullId_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? singleChoiceOptionId = null;

            //Act
            Func<Task> action = async () => await _singleChoiceOptionGetterService.GetSingleChoiceOptionById(singleChoiceOptionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um SingleChoiceOptionId válido mas que não existe, deve retornar null como SingleChoiceOptionDTOResponse
        [Fact]
        public async Task GetSingleChoiceOptionById_WithValidIdButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? singleChoiceOptionId = Guid.NewGuid();

            //Act
            Func<Task> action = async () => await _singleChoiceOptionGetterService.GetSingleChoiceOptionById(singleChoiceOptionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um SingleChoiceOptionId válido e que existe, deve retornar uma lista
        [Fact]
        public async Task GetSingleChoiceOptionById_WithValidIdAndExist_ToBeSuccessful()
        {
            //Arrange
            var questionId = Guid.NewGuid();
            var singleChoiceOptionId = Guid.NewGuid();

            List<SingleChoiceOptionTranslation> translations = new List<SingleChoiceOptionTranslation>
            {
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = singleChoiceOptionId, Language = "PT", Description = "Sample Description" },
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = singleChoiceOptionId, Language = "EN", Description = "Sample Description" },
                new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = singleChoiceOptionId, Language = "ES", Description = "Sample Description" }
            };

            SingleChoiceOption singleChoiceOption = new()
            {
                SingleChoiceOptionId = singleChoiceOptionId, 
                QuestionId = questionId, 
                Translations = translations
            };

            SingleChoiceOptionDTOResponse expectedResponse = singleChoiceOption.ToSingleChoiceOptionDTOResponse();

            _singleChoiceOptionRepositoryMock.Setup(temp => temp.GetSingleChoiceOptionById(singleChoiceOptionId)).ReturnsAsync(singleChoiceOption);

            //Act
            ApiResponse<SingleChoiceOptionDTOResponse?> actualResponse = await _singleChoiceOptionGetterService.GetSingleChoiceOptionById(singleChoiceOptionId);

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetSingleChoiceOptionByQuestionId

        //TESTE: Fornecido um QuestionId nulo, deve lançar uma exceção
        [Fact]
        public async Task GetSingleChoiceOptionByQuestionId_WithNullId_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? questionId = null;

            //Act
            Func<Task> action = async () => await _singleChoiceOptionGetterService.GetSingleChoiceOptionByQuestionId(questionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um ID de Questão, válido, mas que não existe para essa pergunta
        [Fact]
        public async Task GetSingleChoiceOptionByQuestionId_WithValidIdButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            _singleChoiceOptionRepositoryMock
                .Setup(temp => temp.GetSingleChoiceOptionsByQuestionId(questionId))
                .ReturnsAsync(null as List<SingleChoiceOption>);

            //Act
            Func<Task> action = async () => await _singleChoiceOptionGetterService.GetSingleChoiceOptionByQuestionId(questionId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: Fornecido um ID de Questão válido
        [Fact]
        public async Task GetSingleChoiceOptionByQuestionId_WithValidIdAndExist_ToBeSuccessful()
        {
            //Arrange
            Guid firstQuestionId = Guid.NewGuid();
            Guid secondQuestionId = Guid.NewGuid();
            Guid firstSingleChoiceOptionId = Guid.NewGuid();
            Guid secondSingleChoiceOptionId = Guid.NewGuid();
            Guid thirdSingleChoiceOptionId = Guid.NewGuid();

            List<SingleChoiceOption> singleChoiceOptionList = new List<SingleChoiceOption>()
            {
                new SingleChoiceOption 
                { 
                    SingleChoiceOptionId = firstSingleChoiceOptionId, 
                    QuestionId = firstQuestionId, 
                    Translations =  new List<SingleChoiceOptionTranslation>
                    {
                        new SingleChoiceOptionTranslation
                        {
                            SingleChoiceOptionTranslationId = Guid.NewGuid(),
                            SingleChoiceOptionId = firstSingleChoiceOptionId,
                            Language = Language.PT.ToString(),
                            Description = "Sim",
                        },
                        new SingleChoiceOptionTranslation
                        {
                            SingleChoiceOptionTranslationId = Guid.NewGuid(),
                            SingleChoiceOptionId = firstSingleChoiceOptionId,
                            Language = Language.EN.ToString(),
                            Description = "Yes",
                        },
                    }
                },
                new SingleChoiceOption
                {
                    SingleChoiceOptionId = secondSingleChoiceOptionId,
                    QuestionId = firstQuestionId,
                    Translations =  new List<SingleChoiceOptionTranslation>
                    {
                        new SingleChoiceOptionTranslation
                        {
                            SingleChoiceOptionTranslationId = Guid.NewGuid(),
                            SingleChoiceOptionId = secondSingleChoiceOptionId,
                            Language = Language.EN.ToString(),
                            Description = "No",
                        },
                    }
                },
                new SingleChoiceOption
                {
                    SingleChoiceOptionId = thirdSingleChoiceOptionId,
                    QuestionId = secondQuestionId,
                    Translations =  new List<SingleChoiceOptionTranslation>
                    {
                        new SingleChoiceOptionTranslation
                        {
                            SingleChoiceOptionTranslationId = Guid.NewGuid(),
                            SingleChoiceOptionId = thirdSingleChoiceOptionId,
                            Language = Language.EN.ToString(),
                            Description = "Good",
                        },
                    }
                },
            };

            List<SingleChoiceOptionDTOResponse> expectedResponse = singleChoiceOptionList
                .Select(temp => temp.ToSingleChoiceOptionDTOResponse())
                .Where(sco => sco.QuestionId == firstQuestionId)
                .ToList();

            _singleChoiceOptionRepositoryMock.Setup(temp => temp.GetSingleChoiceOptionsByQuestionId(It.IsAny<Guid>())).ReturnsAsync(singleChoiceOptionList);

            //Act
            ApiResponse<List<SingleChoiceOptionDTOResponse>?> actualResponse = await _singleChoiceOptionGetterService.GetSingleChoiceOptionByQuestionId(firstQuestionId);

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region DeleteSingleChoiceOptionById

        //TESTE: recebe um Guid nulo, logo deve retornar exception
        [Fact]
        public async Task DeleteSingleChoiceOptionById_SingleChoiceOptionIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? singleChoiceOptionId = null;

            //Act
            async Task Act() => await _singleChoiceOptionDeleterService.DeleteSingleChoiceOptionById(singleChoiceOptionId);

            //Assert
            await Assert.ThrowsAsync<HttpStatusException>(Act);
        }

        //TESTE: recebe um id válido que não existe, logo deve retornar falso
        [Fact]
        public async Task DeleteSingleChoiceOptionById_SingleChoiceOptionIdIsValidAndDoesntExist_ShouldReturnFalse()
        {
            //Arrange
            var nonExistingSingleChoiceOptionId = Guid.NewGuid();

            //_singleChoiceOptionRepositoryMock
            //    .Setup(temp => temp.GetSingleChoiceOptionById(nonExistingSingleChoiceOptionId))
            //    .ReturnsAsync(null as SingleChoiceOption);

            _singleChoiceOptionRepositoryMock
                .Setup(temp => temp.DeleteSingleChoiceOptionById(nonExistingSingleChoiceOptionId))
                .ReturnsAsync(false);

            //Act
            var result =  await _singleChoiceOptionDeleterService.DeleteSingleChoiceOptionById(nonExistingSingleChoiceOptionId);

            //Assert
            result.Data.Should().BeFalse();
        }

        //TESTE: recebe um id válido que existe, logo retorna true
        [Fact]
        public async Task DeleteSingleChoiceOptionById_SingleChoiceOptionIdIsValidAndExists_ShouldReturnTrue()
        {
            //Arrange
            var singleChoiceOptionId = Guid.NewGuid();

            _singleChoiceOptionRepositoryMock
                .Setup(temp => temp.DeleteSingleChoiceOptionById(singleChoiceOptionId))
                .ReturnsAsync(true);

            //Act
            var result = await _singleChoiceOptionDeleterService.DeleteSingleChoiceOptionById(singleChoiceOptionId);

            //Assert
            result.Data.Should().BeTrue();
        }

        #endregion
    }
}
