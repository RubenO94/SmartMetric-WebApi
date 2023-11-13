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
using SmartMetric.Core.Services.Getters;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SmartMetric.ServiceTests
{
    public class QuestionTranslationsServiceTest
    {
        //variables
        private readonly IQuestionTranslationAdderService _translationsAdderService;
        private readonly IQuestionTranslationGetterService _translationsGetterService;

        private readonly Mock<IQuestionTranslationsRepository> _translationsRepositoryMock;
        private readonly IQuestionTranslationsRepository _translationsRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        //constructor
        public QuestionTranslationsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _translationsRepositoryMock = new Mock<IQuestionTranslationsRepository>();
            _translationsRepository = _translationsRepositoryMock.Object;
            _testOutputHelper = testOutputHelper;

            var AdderLoggerMock = new Mock<ILogger<QuestionTranslationAdderService>>();
            var GetterLoggerMock = new Mock<ILogger<QuestionTranslationGetterService>>();

            _translationsAdderService = new QuestionTranslationAdderService(_translationsRepository, AdderLoggerMock.Object);
            _translationsGetterService = new QuestionTranslationGetterService(_translationsRepository, GetterLoggerMock.Object);
        }

        #region AddQuestionTranslation Tests

        //TESTE: recebe um objeto null para adicionar uma tradução à questão, deve lançar uma exceção
        [Fact]
        public async Task AddQuestionTranslation_NullTranslation_ToBeArgumentNullException()
        {
            //Arrange
            QuestionTranslationDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddQuestionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>(); 
        }

        //TESTE: recebe uma nova tradução com o campo QuestionID null
        [Fact]
        public async Task AddQuestionTranslation_QuestionIdIsNull_ToBeArgumentException()
        {
            //Arrange
            QuestionTranslationDTOAddRequest? request = new QuestionTranslationDTOAddRequest()
            {
                Language = Language.PT,
                Title = "Sample Title",
                Description = "Sample Description",
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddQuestionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: recebe uma nova tradução com o campo Language null
        [Fact]
        public async Task AddQuestionTranslation_LanguageIsNull_ToBeArgumentException()
        {
            //Arrange
            QuestionTranslationDTOAddRequest? request = new QuestionTranslationDTOAddRequest()
            {
                QuestionId = Guid.NewGuid(),
                Title = "Sample Title",
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddQuestionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: recebe uma tradução com o campo Title null
        [Fact]
        public async Task AddQuestionTranslation_TitleIsNull_ToBeArgumentException()
        {
            //Arrange
            QuestionTranslationDTOAddRequest? request = new QuestionTranslationDTOAddRequest()
            {
                QuestionId = Guid.NewGuid(),
                Language = Language.PT,
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddQuestionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: recebe uma tradução com todos os campos preenchidos
        [Fact]
        public async Task AddQuestionTranslation_FullDetails()
        {
            //Arrange
            QuestionTranslationDTOAddRequest? request = _fixture.Build<QuestionTranslationDTOAddRequest>().With(temp => temp.Language, Language.PT).Create();

            QuestionTranslation translation = request.ToQuestionTranslation();
            QuestionTranslationDTOResponse response = translation.ToQuestionTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.AddQuestionTranslation(It.IsAny<QuestionTranslation>())).ReturnsAsync(translation);

            //Act
            QuestionTranslationDTOResponse response_from_add = await _translationsAdderService.AddQuestionTranslation(request);
            response.QuestionTranslationId = response_from_add.QuestionTranslationId;

            //Assert
            response_from_add.Should().NotBe(Guid.Empty);
            response_from_add.Should().Be(response);
        }

        #endregion

        #region GetAllQuestionTranslation Tests

        //TESTE: retornar uma lista vazia
        [Fact]
        public async Task GetAllQuestionTranslation_ToBeEmptyList()
        {
            //Arrange
            var translations = new List<QuestionTranslation>();
            _translationsRepositoryMock.Setup(temp => temp.GetAllQuestionTranslations()).ReturnsAsync(translations);

            //Act
            List<QuestionTranslationDTOResponse> responseFromGet = await _translationsGetterService.GetAllQuestionTranslations();

            //Assert
            responseFromGet.Should().BeEmpty();
        }

        //TESTE: retornar lista de todas as traduções de todas as qeustões disponíveis na base de dados
        [Fact]
        public async Task GetAllQuestionTranslation_WithTranslations_ToBeSuccessfull()
        {
            //Arrange
            List<QuestionTranslation> translations = new List<QuestionTranslation>()
            {
                _fixture.Build<QuestionTranslation>().With(temp => temp.Question, null as Question).Create(),
                _fixture.Build<QuestionTranslation>().With(temp => temp.Question, null as Question).Create(),
                _fixture.Build<QuestionTranslation>().With(temp => temp.Question, null as Question).Create(),
            };

            List<QuestionTranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToQuestionTranslationDTOResponse()).ToList();

            //Log of the expected response
            _testOutputHelper.WriteLine("Expected Response:");
            foreach (var item in expectedResponse)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }
            _translationsRepositoryMock.Setup(temp => temp.GetAllQuestionTranslations()).ReturnsAsync(translations);

            //Act
            List<QuestionTranslationDTOResponse> actualResponse = await _translationsGetterService.GetAllQuestionTranslations();

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

        #region GetQuestionTranslationById Tests

        //TESTE: recebe um Id da tradução da questão null, logo deve lançar uma exceção 
        [Fact]
        public async Task GetQuestionTranslationsById_QuestionTranslationIdIsNull_ToBeArgumentNullException()
        {
            //Arrange
            Guid? id = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetQuestionTranslationById(id);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //TESTE: recebe um Id da tradução da questão válido
        [Fact]
        public async Task GetQuestionTranslationsById_QuestionTranslationId_IsValid()
        {
            //Arrange
            QuestionTranslation translation = _fixture.Build<QuestionTranslation>().With(temp => temp.Question, null as Question).Create();

            QuestionTranslationDTOResponse expectedResponse = translation.ToQuestionTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.GetQuestionTranslationsById(It.IsAny<Guid>())).ReturnsAsync(translation);

            //Act
            QuestionTranslationDTOResponse? actualResponse = await _translationsGetterService.GetQuestionTranslationById(translation.QuestionTranslationId);

            //Assert
            actualResponse.Should().Be(expectedResponse);
        }

        #endregion

        #region GetQuestionTranslationByQuestionId Tests

        //TESTE: recebe um Id da questão nulo logo deve lançar uma exceção
        [Fact]
        public async Task GetQuestionTranslationByQuestionId_QuestionIdIsNull_ToThrowArgumentNullException()
        {
            //Arrange
            Guid? questionId = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetQuestionTranslationByQuestionId(questionId);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //TESTE: recebe um Id válido mas não existem traduções logo, deve retornar uma lista vazia
        [Fact]
        public async Task GetQuestionTranslationByQuestionId_QuestionIdIsValid_NoTranslation_ToBeEmptyList()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            _translationsRepositoryMock.Setup(temp => temp.GetQuestionTranslationsByQuestionId(It.IsAny<Guid>())).ReturnsAsync(new List<QuestionTranslation>());

            //Act
            List<QuestionTranslationDTOResponse>? response = await _translationsGetterService.GetQuestionTranslationByQuestionId(questionId);

            //Assert
            response.Should().BeEmpty();
        }

        //TESTE: recebe um Id válido onde existem traduções
        [Fact]
        public async Task GetQuestionTranslationByQuestionId_QuestionIdIsValid_WithTranslation_ToBeSuccessful()
        {
            //Arrange
            Guid questionId = Guid.NewGuid();

            List<QuestionTranslation> translations = new List<QuestionTranslation>()
            {
                _fixture.Build<QuestionTranslation>().With(temp => temp.QuestionId, questionId).With(temp => temp.Question, null as Question).Create(),
                _fixture.Build<QuestionTranslation>().With(temp => temp.QuestionId, questionId).With(temp => temp.Question, null as Question).Create(),
                _fixture.Build<QuestionTranslation>().With(temp => temp.QuestionId, questionId).With(temp => temp.Question, null as Question).Create(),
            };

            List<QuestionTranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToQuestionTranslationDTOResponse()).ToList();

            _translationsRepositoryMock.Setup(temp => temp.GetQuestionTranslationsByQuestionId(It.IsAny<Guid>())).ReturnsAsync(translations);

            //Act
            List<QuestionTranslationDTOResponse>? actualResponse = await _translationsGetterService.GetQuestionTranslationByQuestionId(questionId);

            //Assert
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        #endregion
    }
}
