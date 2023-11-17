//using AutoFixture;
//using FluentAssertions;
//using Microsoft.Extensions.Logging;
//using Moq;
//using SmartMetric.Core.Domain.Entities;
//using SmartMetric.Core.Domain.RepositoryContracts;
//using SmartMetric.Core.DTO.AddRequest;
//using SmartMetric.Core.DTO.Response;
//using SmartMetric.Core.Services.Adders;
//using SmartMetric.Core.Services.Getters;
//using SmartMetric.Core.ServicesContracts.Adders;
//using SmartMetric.Core.ServicesContracts.Getters;
//using Xunit.Abstractions;

//namespace SmartMetric.ServiceTests
//{
//    public class QuestionServiceTest
//    {
//        #region Variables

//        private readonly IQuestionAdderService _questionAdderService;
//        private readonly IQuestionGetterService _questionGetterService;

//        private readonly Mock<IQuestionRepository> _questionRepositoryMock;
//        private readonly IQuestionRepository _questionRepository;

//        private readonly ITestOutputHelper _testOutputHelper;
//        private readonly IFixture _fixture;

//        #endregion

//        #region Constructor

//        public QuestionServiceTest(ITestOutputHelper testOutputHelper)
//        {
//            _fixture = new Fixture();
//            _questionRepositoryMock = new Mock<IQuestionRepository>();
//            _questionRepository = _questionRepositoryMock.Object;
//            _testOutputHelper = testOutputHelper;

//            var AdderLoggerMock = new Mock<ILogger<QuestionAdderService>>();
//            var GetterLoggerMock = new Mock<ILogger<QuestionGetterService>>();

//            _questionAdderService = new QuestionAdderService(_questionRepository, AdderLoggerMock.Object);
//            _questionGetterService = new QuestionGetterService(_questionRepository, GetterLoggerMock.Object);
//        }

//        #endregion

//        #region AddQuestion Tests

//        //TESTE: recebe um objeto null para adicionar uma questão, deve lançar uma exceção
//        [Fact]
//        public async Task AddQuestion_IsNull_ToThrowArgumentNullException()
//        {
//            //Arrange
//            QuestionDTOAddRequest? request = null;

//            //Act
//            Func<Task> action = async () => await _questionAdderService.AddQuestionToFormTemplate(request);

//            //Assert
//            await action.Should().ThrowAsync<ArgumentNullException>();
//        }

//        //TODO: The other tests

//        #endregion

//        #region GetAllQuestion Tests

//        //TESTE: retornar uma lista vazia
//        [Fact]
//        public async Task GetAllQuestion_ToBeEmptyList()
//        {
//            //Arrange
//            var question = new List<Question>();
//            _questionRepositoryMock.Setup(temp => temp.GetAllQuestion()).ReturnsAsync(question);

//            //Act
//            List<QuestionDTOResponse> responseFromGet = await _questionGetterService.GetAllQuestion();

//            //Assert
//            responseFromGet.Should().BeEmpty();
//        }

//        //TODO: finish the implementation of this test
//        //TESTE: retornar uma lista de todas as questões disponíveis na base de dados
//        //[Fact]
//        //public async Task GetAllQuestion_ToBeSuccessful()
//        //{
//        //    //Arrange
//        //    List<Question> question = new List<Question>()
//        //    {
//        //        _fixture.Build<Question>().Create()
//        //    };

//        //    //Act
//        //    //Assert
//        //}

//        #endregion

//        #region GetQuestionById Tests



//        #endregion

//        #region GetQuestionByFormTemplateId Tests

//        //TESTE: recebe um id de FormTemplate nulo, deve retornar uma exceção
//        [Fact]
//        public async Task GetQuestionByFormTemplateId_FormTemplateIsNull_ToThrowArgumentNullException()
//        {
//            //Arrange
//            Guid? formTemplateId = null;

//            //Act
//            Func<Task> action = async () => await _questionGetterService.GetQuestionByFormTemplateId(formTemplateId);

//            //Assert
//            await action.Should().ThrowAsync<ArgumentNullException>();
//        }

//        //TESTE: recebe um id do FormTemplate, válido, mas não tem questões para esse formTemplate
//        [Fact]
//        public async Task GetQuestionByFormTemplateId_FormTemplateIdIsValid_WithNoQuestion_ToBeEmpty()
//        {
//            //Arrange
//            Guid formTemplateId = Guid.NewGuid();
//            _questionRepositoryMock.Setup(temp => temp.GetQuestionByFormTemplateId(It.IsAny<Guid>())).ReturnsAsync(new List<Question>());

//            //Act
//            List<QuestionDTOResponse>? response = await _questionGetterService.GetQuestionByFormTemplateId(formTemplateId);

//            //Assert
//            response.Should().BeEmpty();
//        }

//        //TESTE: recebe um id do formTemplate válido
//        [Fact]
//        public async Task GetQuestionByFormTemplateId_FormTemplateIdIsValid_ToBeSuccessful()
//        {
//            //Arrange
//            Guid formTemplateId = Guid.NewGuid();

//            List<Question> rows = new List<Question>()
//            {
//                _fixture
//                    .Build<Question>()
//                    //.With(temp => temp.FormTemplateQuestions, null as List<FormTemplateQuestion>)
//                    //.With(temp => temp.ReviewQuestions, null as List<ReviewQuestion>)
//                    .With(temp => temp.Translations, null as List<QuestionTranslation>)
//                    .With(temp => temp.RatingOptions, null as List<RatingOption>)
//                    .With(temp => temp.SingleChoiceOptions, null as List<SingleChoiceOption>)
//                    .Create(),
//            };

//            List<QuestionDTOResponse> expectedResponse = rows.Select(temp => temp.ToQuestionDTOResponse()).ToList();
//            _questionRepositoryMock.Setup(temp => temp.GetQuestionByFormTemplateId(It.IsAny<Guid>())).ReturnsAsync(rows);

//            //Act
//            List<QuestionDTOResponse>? actualResponse = await _questionGetterService.GetQuestionByFormTemplateId(formTemplateId);

//            //Assert
//            actualResponse.Should().BeEquivalentTo(expectedResponse);
//        }

//        #endregion
//    }
//}
