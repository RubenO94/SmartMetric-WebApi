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
    public class FormTemplateServiceTest
    {
        private readonly IFormTemplatesAdderService _formTemplatesAdderService;
        private readonly IFormTemplatesGetterService _formTemplatesGetterService;
        private readonly IFormTemplatesDeleterService _formTemplatesDeleterService;

        private readonly Mock<IFormTemplateRepository> _formTemplateRepositoryMock;
        private readonly IFormTemplateRepository _formTemplatesRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public FormTemplateServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;

            _formTemplateRepositoryMock = new Mock<IFormTemplateRepository>();
            _formTemplatesRepository = _formTemplateRepositoryMock.Object;

            var AdderloggerMock = new Mock<ILogger<FormTemplatesAdderService>>();
            var GetterloggerMock = new Mock<ILogger<FormTemplatesGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<FormTemplatesDeleterService>>();

            _formTemplatesAdderService = new FormTemplatesAdderService(_formTemplatesRepository, AdderloggerMock.Object);
            _formTemplatesGetterService = new FormTemplatesGetterService(_formTemplatesRepository, GetterloggerMock.Object);
            _formTemplatesDeleterService = new FormTemplatesDeleterService(_formTemplatesRepository, DeleterLoggerMock.Object);
        }

        #region AddFormTemplate

        //TESTE: fornecido um formTemplateDTOAddRequest nulo, deve lançar exceção
        [Fact]
        public async Task AddFormTemplate_ObjectIsNull_ShouldThrowException()
        {
            //Arrange
            FormTemplateDTOAddRequest? request = null;
            
            //Act
            Func<Task> action = async () => await _formTemplatesAdderService.AddFormTemplate(request);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //TESTE: fornecido um formTemplateDTOAddRequest com campo createdByUserId nulo, deve lançar exceção
        [Fact]
        public async Task AddFormTemplate_CreatedByUserIdIsNull_ShouldThrowException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            FormTemplateDTOAddRequest request = new FormTemplateDTOAddRequest
            {
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.EN,
                        Title = "Title",
                        Description = "Description",
                    }
                }
            };

            var formTemplate = request.ToFormTemplate();
            var formTemplateExpected = formTemplate.ToFormTemplateDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.AddFormTemplate(formTemplate)).ReturnsAsync(new FormTemplate { FormTemplateId = formTemplateId });

            //Act
            Func<Task> action = async () => await _formTemplatesAdderService.AddFormTemplate(request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um formTemplateDTOAddRequest com campo Translation nulo, deve lançar exceção
        [Fact]
        public async Task AddFormTemplate_TranslationIsNull_ShouldThrowException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            FormTemplateDTOAddRequest request = new FormTemplateDTOAddRequest
            {
                CreatedByUserId = 12345
            };

            var formTemplate = request.ToFormTemplate();
            var formTemplateExpected = formTemplate.ToFormTemplateDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.AddFormTemplate(formTemplate)).ReturnsAsync(new FormTemplate { FormTemplateId = formTemplateId });

            //Act
            Func<Task> action = async () => await _formTemplatesAdderService.AddFormTemplate(request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um formTemplateDTOAddRequest com campo Translation vazio, deve lançar exceção
        [Fact]
        public async Task AddFormTemplate_TranslationIsEmpty_ShouldThrowException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            FormTemplateDTOAddRequest request = new FormTemplateDTOAddRequest
            {
                CreatedByUserId = 12345,
                Translations = new List<TranslationDTOAddRequest>()
            };

            var formTemplate = request.ToFormTemplate();
            var formTemplateExpected = formTemplate.ToFormTemplateDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.AddFormTemplate(formTemplate)).ReturnsAsync(new FormTemplate { FormTemplateId = formTemplateId });

            //Act
            Func<Task> action = async () => await _formTemplatesAdderService.AddFormTemplate(request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um formTemplateDTOAddRequest com campo Translation sem o campo Language, deve lançar exceção
        [Fact]
        public async Task AddFormTemplate_LanguageIsNull_ShouldThrowException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            FormTemplateDTOAddRequest request = new FormTemplateDTOAddRequest
            {
                CreatedByUserId = 123,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Title = "Example of Title",
                        Description = "Description",
                    }
                }
            };

            var formTemplate = request.ToFormTemplate();
            var formTemplateExpected = formTemplate.ToFormTemplateDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.AddFormTemplate(formTemplate)).ReturnsAsync(new FormTemplate { FormTemplateId = formTemplateId });

            //Act
            Func<Task> action = async () => await _formTemplatesAdderService.AddFormTemplate(request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>().WithMessage("Language");
        }

        //TESTE: fornecido um formTemplateDTOAddRequest com campo Translation sem o campo Title, deve lançar exceção
        [Fact]
        public async Task AddFormTemplate_TitleIsNull_ShouldThrowException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            FormTemplateDTOAddRequest request = new FormTemplateDTOAddRequest
            {
                CreatedByUserId = 123,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.EN,
                        Description = "Description",
                    }
                }
            };

            var formTemplate = request.ToFormTemplate();
            var formTemplateExpected = formTemplate.ToFormTemplateDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.AddFormTemplate(formTemplate)).ReturnsAsync(new FormTemplate { FormTemplateId = formTemplateId });

            //Act
            Func<Task> action = async () => await _formTemplatesAdderService.AddFormTemplate(request);

            //Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        //TESTE: fornecido um formTemplateDTOAddRequest válido, adiciona com sucesso, retorna mensagem
        [Fact]
        public async Task AddFormTemplate_ShouldBeSuccessful()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            FormTemplateDTOAddRequest request = new FormTemplateDTOAddRequest
            {
                CreatedByUserId = 123,
                Translations = new List<TranslationDTOAddRequest>
                {
                    new TranslationDTOAddRequest
                    {
                        Language = Language.EN,
                        Title = "Title",
                        Description = "Description",
                    }
                }
            };

            var formTemplate = request.ToFormTemplate();
            var formTemplateExpected = formTemplate.ToFormTemplateDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.AddFormTemplate(formTemplate)).ReturnsAsync(new FormTemplate { FormTemplateId = formTemplateId });

            //Act
            var result = await _formTemplatesAdderService.AddFormTemplate(request);

            //Assert
            Assert.NotNull(result.Data);
            Assert.IsType<FormTemplateDTOResponse>(result.Data);
            result.Data.Equals(formTemplateExpected);
        }

        #endregion

        #region GetAllFormTemplate

        //TESTE: retornar uma lista vazia
        [Fact]
        public async Task GetAllFormTemplates_ShouldBeEmptyList()
        {
            //Arrange
            var request = new List<FormTemplate>();

            _formTemplateRepositoryMock.Setup(temp => temp.GetAllFormTemplates()).ReturnsAsync(request);

            //Act
            ApiResponse<List<FormTemplateDTOResponse?>> responseFromGet = await _formTemplatesGetterService.GetAllFormTemplates();

            //Assert
            responseFromGet.Data.Should().BeEmpty();
        }

        //TESTE: retornar uma lista
        [Fact]
        public async Task GetAllFormTemplates_ShouldBeSuccessful()
        {
            //Arrange
            List<FormTemplateDTOAddRequest> formTemplatesRequest = new()
            {
                new FormTemplateDTOAddRequest
                {
                    CreatedByUserId = 123,
                    Translations = new List<TranslationDTOAddRequest>
                    {
                        new TranslationDTOAddRequest
                        {
                            Language = Language.EN,
                            Title = "Title",
                            Description = "Description",
                        }
                    }
                },
                new FormTemplateDTOAddRequest
                {
                    CreatedByUserId = 12345,
                    Translations = new List<TranslationDTOAddRequest>
                    {
                        new TranslationDTOAddRequest
                        {
                            Language = Language.EN,
                            Title = "Title2",
                            Description = "Description2",
                        }
                    }
                },
            };

            List<FormTemplate> formTemplatesList = formTemplatesRequest.Select(temp => temp.ToFormTemplate()).ToList();
            List<FormTemplateDTOResponse> expectedResponse = formTemplatesList.Select(temp => temp.ToFormTemplateDTOResponse()).ToList();

            _formTemplateRepositoryMock.Setup(temp => temp.GetAllFormTemplates()).ReturnsAsync(formTemplatesList);

            //Act
            ApiResponse<List<FormTemplateDTOResponse?>> actualResponse = await _formTemplatesGetterService.GetAllFormTemplates();

            //Assert
            actualResponse.Data.Should().NotBeNull();
            actualResponse.Data.Should().NotBeEmpty();
            actualResponse.Data.Should().HaveSameCount(expectedResponse);
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetFormTemplateById

        //TESTE: fornecido um Guid nulo, deve lançar exceção
        [Fact]
        public async Task GetFormTemplateById_FormTemplateIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? formTemplateId = null;

            //Act
            Func<Task> action = async () => await _formTemplatesGetterService.GetFormTemplateById(formTemplateId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task GetFormTemplateById_FormTemplateIdIsValidButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? formTemplateId = Guid.NewGuid();

            //Act
            Func<Task> action = async () => await _formTemplatesGetterService.GetFormTemplateById(formTemplateId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, logo retorna ApiResponse
        [Fact]
        public async Task GetFormTemplateById_FormTemplateIdIsValidAndExist_ShouldBeSuccessful()
        {
            //Arrange
            var formTemplateId = Guid.NewGuid();

            FormTemplate formTemplate = new()
            {
                FormTemplateId = formTemplateId,
                CreatedDate = DateTime.UtcNow,
                CreatedByUserId = 12345,
                Translations = new List<FormTemplateTranslation>
                {
                    new FormTemplateTranslation
                    {
                        FormTemplateTranslationId = Guid.NewGuid(),
                        FormTemplateId = formTemplateId,
                        Language = Language.EN.ToString(),
                        Title = "Title",
                        Description = "Description",
                    }
                }
            };

            FormTemplateDTOResponse expectedResponse = formTemplate.ToFormTemplateDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.GetFormTemplateById(formTemplate.FormTemplateId)).ReturnsAsync(formTemplate);

            //Act
            ApiResponse<FormTemplateDTOResponse?> actualResponse = await _formTemplatesGetterService.GetFormTemplateById(formTemplateId);

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region DeleteFormTemplateById

        //TESTE: fornecido um Guid nulo, logo deve lançar exceção
        [Fact]
        public async Task DeleteFormTemplateById_FormTemplateIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? formTemplateId = null;

            //Act
            Func<Task> action = async () => await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task DeleteFromTemplateById_FormTemplateIdIsValidButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            _formTemplateRepositoryMock
                .Setup(temp => temp.GetFormTemplateById(formTemplateId))
                .ReturnsAsync(null as FormTemplate);

            //Act
            Func<Task> action = async () => await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: recebe um Guid válido e que existe, logo retorna ApiResponse
        [Fact]
        public async Task DeleteFromTemplateById_ShouldBeSuccessful()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            var formTemplate = new FormTemplate
            {
                FormTemplateId = formTemplateId,
                CreatedByUserId = 123,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Translations = new List<FormTemplateTranslation>
                {
                    new FormTemplateTranslation
                    {
                        FormTemplateTranslationId = Guid.NewGuid(),
                        FormTemplateId = formTemplateId,
                        Language = Language.EN.ToString(),
                        Description = "description",
                    }
                }
            };

            _formTemplateRepositoryMock
                .Setup(temp => temp.GetFormTemplateById(formTemplate.FormTemplateId))
                .ReturnsAsync(formTemplate);

            _formTemplateRepositoryMock
                .Setup(temp => temp.DeleteFormTemplateById(formTemplate.FormTemplateId))
                .ReturnsAsync(true);

            //Act
            var result = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplate.FormTemplateId);

            //Assert
            result.Data.Should().BeTrue();
            Assert.True(result.Data);
        }

        #endregion
    }
}