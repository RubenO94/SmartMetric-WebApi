﻿using AutoFixture;
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
using Xunit.Abstractions;

namespace SmartMetric.ServiceTests
{
    public class FormTemplateTranslationsServiceTest
    {
        private readonly IFormTemplateTranslationsAdderService _translationsAdderService;
        private readonly IFormTemplateTranslationsGetterService _translationsGetterService;

        private readonly Mock<IFormTemplateTranslationsRepository> _translationsRepositoryMock;
        private readonly IFormTemplateTranslationsRepository _translationsRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public FormTemplateTranslationsServiceTest(ITestOutputHelper testOutputHelper)
        {

            _fixture = new Fixture();
            _translationsRepositoryMock = new Mock<IFormTemplateTranslationsRepository>();
            _translationsRepository = _translationsRepositoryMock.Object;
            _testOutputHelper = testOutputHelper;

            var AdderloggerMock = new Mock<ILogger<FormTemplateTranslationsAdderService>>();
            var GetterloggerMock = new Mock<ILogger<FormTemplateTranslationsGetterService>>();

            _translationsAdderService = new FormTemplateTranslationsAdderService(_translationsRepository, AdderloggerMock.Object);
            _translationsGetterService = new FormTemplateTranslationsGetterService(_translationsRepository, GetterloggerMock.Object);
        }

        #region AddFormTemplateTranslation

        //TESTE: Fornecido um valor nulo como FormTemplateTranslationDTOAddRquest, deve lançar um ArgumentNullException
        [Fact]
        public async Task AddFormTemplateTranslation_NullTranslation_ToBeArgumentNullException()
        {
            //Arrange
            FormTemplateTranslationDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddFormTemplateTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //TESTE: Fornecido um valor nulo no campo Language, deve lançar um ArgumentNullException
        [Fact]
        public async Task AddFormTemplateTranslation_LanguageIsNull_ToBeArgumentNullException()
        {
            //Arrange
            FormTemplateTranslationDTOAddRequest request = new FormTemplateTranslationDTOAddRequest
            {
                FormTemplateId = Guid.NewGuid(),
                Title = "Sample Title",
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddFormTemplateTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: Fornecido um valor nulo no campo FormTemplateId, deve lançar um ArgumentNullException
        [Fact]
        public async Task AddFormTemplateTranslation_FormTemplateIdIsNull_ToBeArgumentNullException()
        {
            //Arrange
            FormTemplateTranslationDTOAddRequest request = new FormTemplateTranslationDTOAddRequest
            {
                Language = Language.PT,
                Title = "Sample Title",
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddFormTemplateTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: Fornecido um valor nulo no campo Title, deve lançar um ArgumentNullException
        [Fact]
        public async Task AddFormTemplateTranslation_TitleIsNull_ToBeArgumentNullException()
        {
            //Arrange
            FormTemplateTranslationDTOAddRequest request = new FormTemplateTranslationDTOAddRequest
            {
                FormTemplateId = Guid.NewGuid(),
                Language = Language.PT,
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddFormTemplateTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: Fornecido um objeto FormTemplateTranslationDTOAddRequest com os detalhes corretos, deve inserir o objeto na lista de traduções e retornar um objeto FormTemplateTranslationDTOResponse que inclua o recente FormTemplateTranslation Id gerado.
        [Fact]
        public async Task AddFormTemplateTranslation_FullDetails_ToBeSuccessful()
        {
            //Arranje
            FormTemplateTranslationDTOAddRequest? request = _fixture.Build<FormTemplateTranslationDTOAddRequest>().With(temp => temp.Language, Language.PT).Create();

            FormTemplateTranslation translation = request.ToFormTemplateTranslation();
            FormTemplateTranslationDTOResponse response = translation.ToFormTemplateTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.AddFormTemplateTranslation(It.IsAny<FormTemplateTranslation>())).ReturnsAsync(translation);

            //Act
            FormTemplateTranslationDTOResponse response_from_add = await _translationsAdderService.AddFormTemplateTranslation(request);

            response.FormTemplateTranslationId = response_from_add.FormTemplateTranslationId;

            //Assert
            response_from_add.Should().NotBe(Guid.Empty);
            response_from_add.Should().Be(response);

        }
        #endregion

        #region GetAllFormTemplateTranslations

        //TEST: GetAllFormTemplateTranslations por defeito deve retornar uma lista vazia;
        [Fact]
        public async Task GetAllFormTemplateTranslations_ToBeEmptyList()
        {
            //Arrange
            var translations = new List<FormTemplateTranslation>();

            _translationsRepositoryMock.Setup(temp => temp.GetAllFormTemplateTranslations()).ReturnsAsync(translations);

            //Act
            List<FormTemplateTranslationDTOResponse> response_from_get = await _translationsGetterService.GetAllFormTemplateTranslations();

            //Assert
            response_from_get.Should().BeEmpty();
        }

        //TESTE: Simular uma adição de algumas traduções e chamar GetAllFormTemplatesTranslations, deve retornar as mesmas traduções que foram adicionadas.
        [Fact]
        public async Task GetAllFormTemplateTranslations_WithSomeTranslations_ToBeSuccessful()
        {
            //Arrange
            List<FormTemplateTranslation> translations = new List<FormTemplateTranslation>()
            {
                _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
                 _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
                  _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
            };

            List<FormTemplateTranslationDTOResponse> expected_response = translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();

            //Log expected_response:
            _testOutputHelper.WriteLine("Expected Response:");
            foreach (var item in expected_response)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            _translationsRepositoryMock.Setup(temp => temp.GetAllFormTemplateTranslations()).ReturnsAsync(translations);

            //Act
            List<FormTemplateTranslationDTOResponse> actual_response = await _translationsGetterService.GetAllFormTemplateTranslations();

            //Log actual_response:
            _testOutputHelper.WriteLine("Actual Response:");
            foreach (var item in actual_response)
            {
                _testOutputHelper.WriteLine(item.ToString());
            }

            //Assert
            actual_response.Should().BeEquivalentTo(expected_response);
        }
        #endregion

        #region GetFormTemplateTranslationById

        //TESTE: Fornecido um FormTemplateTranslationId null, deve lançar um ArgumenteNullException
        [Fact]
        public async Task GetFormTemplateTranslationById_NullFormTemplateTranslationId_ToBeNull()
        {
            //Arrange
            Guid? id = null;

            //Act
            Func<Task> action = async () =>
            {
                await _translationsGetterService.GetFormTemplateTranslationById(id);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //TESTE: Fornecido um FormTemplateTranslationId válido deve retornar o objeto FormTemplateTranslationDTOResponse correspondente ao id
        [Fact]
        public async Task GetFormTemplateTranslationById_WithValidId_ToBeSuccessful()
        {
            //Arrange
            FormTemplateTranslation translation = _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create();

            FormTemplateTranslationDTOResponse expected_response = translation.ToFormTemplateTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.GetFormTemplateTranslationById(It.IsAny<Guid>())).ReturnsAsync(translation);

            //Act
            FormTemplateTranslationDTOResponse? actual_response = await _translationsGetterService.GetFormTemplateTranslationById(translation.FormTemplateTranslationId);

            //Assert
            actual_response.Should().Be(expected_response);
        }
        #endregion

        #region GetTranslationsByFormTemplateId

        //TESTE: Fornecido um FormTemplateId nulo, deve lançar um ArgumenteNullException
        [Fact]
        public async Task GetTranslationsByFormTemplateId_NullFormTemplateId_ToThrowArgumentNullException()
        {
            //Arrange
            Guid? formTemplateId = null;

            //Act
            Func<Task> action = async () =>
            {
                await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //TESTE: Fornecido um FormTemplateId válido, mas sem traduções associadas, deve retornar uma lista vazia de FormTemplateTranslationDTOResponse
        [Fact]
        public async Task GetTranslationsByFormTemplateId_WithValidId_NoTranslations_ToBeEmptyList()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsByFormTemplateId(It.IsAny<Guid>())).ReturnsAsync(new List<FormTemplateTranslation>());

            //Act
            List<FormTemplateTranslationDTOResponse>? response = await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);

            //Assert
            response.Should().BeEmpty();
        }

        //TESTE: Fornecido um FormTemplateId válido, com traduções associadas, deve retornar a lista correspondente de FormTemplateTranslationDTOResponse
        [Fact]
        public async Task GetTranslationsByFormTemplateId_WithValidId_WithTranslations_ToBeSuccessful()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            List<FormTemplateTranslation> translations = new List<FormTemplateTranslation>()
            {
                _fixture.Build<FormTemplateTranslation>()
                .With(temp => temp.FormTemplateId, formTemplateId)
                .With(temp => temp.FormTemplate, null as FormTemplate)
                .Create(),
                _fixture.Build<FormTemplateTranslation>()
                .With(temp => temp.FormTemplateId, formTemplateId)
                .With(temp => temp.FormTemplate, null as FormTemplate)
                .Create(),
                _fixture.Build<FormTemplateTranslation>()
                .With(temp => temp.FormTemplateId, formTemplateId)
                .With(temp => temp.FormTemplate, null as FormTemplate)
                .Create(),
            };

            List<FormTemplateTranslationDTOResponse> expected_response = translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();

            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsByFormTemplateId(It.IsAny<Guid>())).ReturnsAsync(translations);

            //Act
            List<FormTemplateTranslationDTOResponse>? actual_response = await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);

            //Assert
            actual_response.Should().BeEquivalentTo(expected_response);
        }

        #endregion


    }
}