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
using System.Net.WebSockets;
using Xunit.Abstractions;

namespace SmartMetric.ServiceTests
{
    public class FormTemplateTranslationsServiceTest
    {
        private readonly IFormTemplateTranslationsAdderService _translationsAdderService;
        private readonly IFormTemplateTranslationsGetterService _translationsGetterService;
        private readonly IFormTemplateTranslationsDeleterService _translationsDeleterService;

        private readonly Mock<IFormTemplateTranslationRepository> _translationsRepositoryMock;
        private readonly IFormTemplateTranslationRepository _translationsRepository;

        private readonly Mock<IFormTemplateRepository> _formTemplateRepositoryMock;
        private readonly IFormTemplateRepository _formTemplatesRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public FormTemplateTranslationsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;

            _translationsRepositoryMock = new Mock<IFormTemplateTranslationRepository>();
            _translationsRepository = _translationsRepositoryMock.Object;

            _formTemplateRepositoryMock = new Mock<IFormTemplateRepository>();
            _formTemplatesRepository = _formTemplateRepositoryMock.Object;

            var AdderloggerMock = new Mock<ILogger<FormTemplateTranslationsAdderService>>();
            var GetterloggerMock = new Mock<ILogger<FormTemplateTranslationsGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<FormTemplateTranslationDeleterService>>();

            _translationsAdderService = new FormTemplateTranslationsAdderService(_translationsRepository, _formTemplatesRepository, AdderloggerMock.Object);
            _translationsGetterService = new FormTemplateTranslationsGetterService(_translationsRepository, GetterloggerMock.Object);
            _translationsDeleterService = new FormTemplateTranslationDeleterService(_translationsRepository, _formTemplatesRepository, DeleterLoggerMock.Object);
        }

        #region AddFormTemplateTranslation

        //TESTE: fornecido um formTemplateTranslationDTOAddRequest nulo, deve lançar exceção
        [Fact]
        public async Task AddFormTemplateTranslation_ObjectIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            FormTemplateTranslationDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddFormTemplateTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um formTemplateTranslationDTOAddRequest com campo FormTemplateId nulo, deve lançar exceção
        [Fact]
        public async Task AddFormTemplateTranslation_FormTemplateIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid formTemplateTranslationId = Guid.NewGuid();

            FormTemplateTranslationDTOAddRequest request = new()
            {
                Language = Language.EN,
                Title = "Title",
                Description = "Description",
            };

            var formTemplateTranslation = request.ToFormTemplateTranslation();
            var response = formTemplateTranslation.ToFormTemplateTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.AddFormTemplateTranslation(formTemplateTranslation)).ReturnsAsync(new FormTemplateTranslation { FormTemplateTranslationId = formTemplateTranslationId});

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddFormTemplateTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um formTemplateTranslationDTOAddRequest com campo FormTemplateId válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task AddFormTemplateTranslation_FormTemplateIdIsValidButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();
            Guid formTemplateNonExistingId = Guid.NewGuid();

            List<FormTemplateTranslation> formTemplateTranslationExists = new()
            {
                new FormTemplateTranslation
                {
                    FormTemplateTranslationId = Guid.NewGuid(),
                    FormTemplateId = formTemplateId,
                    Language = Language.PT.ToString(),
                    Title = "Title",
                    Description = "Description",
                }
            };

            FormTemplate formTemplate = new()
            {
                FormTemplateId = formTemplateId,
                CreatedDate = DateTime.UtcNow,
                CreatedByUserId = 123434,
                Translations = formTemplateTranslationExists
            };

            FormTemplateTranslationDTOAddRequest request = new()
            {
                FormTemplateId = formTemplateId,
                Language = Language.EN,
                Title = "Title",
                Description = "Description",
            };

            var formTemplateTranslation = request.ToFormTemplateTranslation();
            var response = formTemplateTranslation.ToFormTemplateTranslationDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.GetFormTemplateById(formTemplateNonExistingId)).ReturnsAsync(null as FormTemplate);

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddFormTemplateTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um formTemplateTranslationDTOAddRequest com campo Language nulo, deve lançar exceção
        [Fact]
        public async Task AddFormTemplateTranslation_LanguageIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid formTemplateTranslationId = Guid.NewGuid();
            Guid formTemplateId = Guid.NewGuid();

            FormTemplateTranslationDTOAddRequest request = new()
            {
                FormTemplateId = formTemplateId,
                Title = "Title",
                Description = "Description",
            };

            var formTemplateTranslation = request.ToFormTemplateTranslation();
            var response = formTemplateTranslation.ToFormTemplateTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.AddFormTemplateTranslation(formTemplateTranslation)).ReturnsAsync(new FormTemplateTranslation { FormTemplateTranslationId = formTemplateTranslationId });

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddFormTemplateTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um formTemplateTranslationDTOAddRequest com campo Title nulo, deve lançar exceção
        [Fact]
        public async Task AddFormTemplateTranslation_TitleIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid formTemplateTranslationId = Guid.NewGuid();
            Guid formTemplateId = Guid.NewGuid();

            FormTemplateTranslationDTOAddRequest request = new()
            {
                FormTemplateId = formTemplateId,
                Language = Language.EN,
                Description = "Description",
            };

            var formTemplateTranslation = request.ToFormTemplateTranslation();
            var response = formTemplateTranslation.ToFormTemplateTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.AddFormTemplateTranslation(formTemplateTranslation)).ReturnsAsync(new FormTemplateTranslation { FormTemplateTranslationId = formTemplateTranslationId });

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddFormTemplateTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um formTemplateTranslationDTOAddRequest válido mas idioma já existe, deve lançar exceção
        [Fact]
        public async Task AddFormTemplateTranslation_LanguageAlreadyExists_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            List<FormTemplateTranslation> formTemplateTranslationExists = new()
            {
                new FormTemplateTranslation
                {
                    FormTemplateTranslationId = Guid.NewGuid(),
                    FormTemplateId = formTemplateId,
                    Language = Language.EN.ToString(),
                    Title = "Title",
                    Description = "Description",
                }
            };

            FormTemplate formTemplate = new()
            {
                FormTemplateId = formTemplateId,
                CreatedDate = DateTime.UtcNow,
                CreatedByUserId = 123434,
                Translations = formTemplateTranslationExists
            };

            FormTemplateTranslationDTOAddRequest request = new()
            {
                FormTemplateId = formTemplateId,
                Language = Language.EN,
                Title = "Title",
                Description = "Description",
            };

            var formTemplateTranslation = request.ToFormTemplateTranslation();
            var response = formTemplateTranslation.ToFormTemplateTranslationDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.GetFormTemplateById(formTemplateId)).ReturnsAsync(formTemplate);

            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsByFormTemplateId(formTemplateId)).ReturnsAsync(formTemplateTranslationExists);

            _translationsRepositoryMock.Setup(temp => temp.AddFormTemplateTranslation(formTemplateTranslation)).ReturnsAsync(formTemplateTranslation);

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddFormTemplateTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um formTemplateTranslationDTOAddRequest válido, adiciona com sucesso, retorna mensagem
        [Fact]
        public async Task AddFormTemplateTranslation_ShouldBeSuccessful()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            List<FormTemplateTranslation> formTemplateTranslationExists = new()
            {
                new FormTemplateTranslation
                {
                    FormTemplateTranslationId = Guid.NewGuid(),
                    FormTemplateId = formTemplateId,
                    Language = Language.PT.ToString(),
                    Title = "Title",
                    Description = "Description",
                }
            };

            FormTemplate formTemplate = new()
            {
                FormTemplateId = formTemplateId,
                CreatedDate = DateTime.UtcNow,
                CreatedByUserId = 123434,
                Translations = formTemplateTranslationExists
            };

            FormTemplateTranslationDTOAddRequest request = new()
            {
                FormTemplateId = formTemplateId,
                Language = Language.EN,
                Title = "Title",
                Description = "Description",
            };

            var formTemplateTranslation = request.ToFormTemplateTranslation();
            var response = formTemplateTranslation.ToFormTemplateTranslationDTOResponse();

            _formTemplateRepositoryMock.Setup(temp => temp.GetFormTemplateById(formTemplateId)).ReturnsAsync(formTemplate);

            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsByFormTemplateId(formTemplateId)).ReturnsAsync(formTemplateTranslationExists);

            _translationsRepositoryMock.Setup(temp => temp.AddFormTemplateTranslation(formTemplateTranslation)).ReturnsAsync(formTemplateTranslation);

            //Act
            var result = await _translationsAdderService.AddFormTemplateTranslation(request);
            response.FormTemplateTranslationId = result.Data!.FormTemplateTranslationId;

            //Assert
            result.Data.Should().Be(response);
        }

        #endregion

        #region GetAllFormTemplateTranslations

        //TESTE: retornar uma lista vazia
        [Fact]
        public async Task GetAllFormTemplateTranslation_ShouldBeEmptyList()
        {
            //Arrange
            List<FormTemplateTranslation> translations = new List<FormTemplateTranslation>();

            _translationsRepositoryMock.Setup(temp => temp.GetAllFormTemplateTranslations()).ReturnsAsync(translations);

            //Act
            ApiResponse<List<FormTemplateTranslationDTOResponse>> responseFromGet = await _translationsGetterService.GetAllFormTemplateTranslations();

            //Assert
            responseFromGet.Data.Should().BeEmpty();
        }

        //TESTE: retornar uma lista
        [Fact]
        public async Task GetAllFormTemplateTranslation_ShouldBeSuccessful()
        {
            //Arrange
            List<FormTemplateTranslation> translations = new()
            {
                _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
                _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
                _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
            };

            List<FormTemplateTranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();

            _translationsRepositoryMock.Setup(temp => temp.GetAllFormTemplateTranslations()).ReturnsAsync(translations);

            //Act
            ApiResponse<List<FormTemplateTranslationDTOResponse>> actualResponse = await _translationsGetterService.GetAllFormTemplateTranslations();

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetFormTemplateTranslationById

        //TESTE: fornecido um Guid nulo, deve lançar exceção
        [Fact]
        public async Task GetFormTemplateTranslationById_FormTemplateTranslationIdIsNull_ShouldThrowException()
        {
            //Arrange
            Guid? formTemplateTranslationId = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetFormTemplateTranslationById(formTemplateTranslationId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task GetFormTemplateTranslationById_FormTemplateTranslationIdIsValidButDoesntExist_ShouldThrowException()
        {
            //Arrange
            Guid? formTemplateTranslationId = Guid.NewGuid();

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetFormTemplateTranslationById(formTemplateTranslationId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, deve lançar exceção
        [Fact]
        public async Task GetFormTemplateTranslationById_FormTemplateTranslationIdIsValidAndExist_ShouldThrowException()
        {
            //Arrange
            Guid formTemplateTranslationId = Guid.NewGuid();

            FormTemplateTranslation translation = new FormTemplateTranslation
            {
                FormTemplateTranslationId = formTemplateTranslationId,
                FormTemplateId = It.IsAny<Guid>(),
                Language = Language.PT.ToString(),
                Title = "Title",
            };

            FormTemplateTranslationDTOResponse expectedResponse = translation.ToFormTemplateTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.GetFormTemplateTranslationById(formTemplateTranslationId)).ReturnsAsync(translation);

            //Act
            ApiResponse<FormTemplateTranslationDTOResponse?> actualResponse = await _translationsGetterService.GetFormTemplateTranslationById(translation.FormTemplateTranslationId);

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region GetTranslationsByFormTemplateId

        //TESTE: fornecido um Guid nulo, deve lançar exceção
        [Fact]
        public async Task GetTranslationByFormTemplateId_FormTemplateIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? formTemplateId = null;

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task GetTranslationByFormTemplateId_FormTemplateIdIsValidButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            //Act
            Func<Task> action = async () => await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, retorna lista
        [Fact]
        public async Task GetTranslationByFormTemplateId_ShouldBeSuccessful()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            List<FormTemplateTranslation> translations = new()
            {
                new FormTemplateTranslation
                {
                    FormTemplateTranslationId = Guid.NewGuid(),
                    FormTemplateId = formTemplateId,
                    Language = Language.PT.ToString(),
                    Title = "Title",
                },
                new FormTemplateTranslation
                {
                    FormTemplateTranslationId = Guid.NewGuid(),
                    FormTemplateId = formTemplateId,
                    Language = Language.EN.ToString(),
                    Title = "Title",
                    Description = "Description",
                },
            };

            List<FormTemplateTranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();

            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsByFormTemplateId(formTemplateId)).ReturnsAsync(translations);

            //Act
            ApiResponse<List<FormTemplateTranslationDTOResponse>?> actualResponse = await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);

            //Assert
            actualResponse.Data!.Equals(expectedResponse);
        }

        #endregion

        #region DeleteFormTemplateTranslationById

        //TESTE: fornecido um Guid nulo, deve lançar exceção
        [Fact]
        public async Task DeleteFormTemplateTranslationById_FormTemplateIdIsNull_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? formTemplateId = null;
            Language language = Language.PT;

            //Act
            Func<Task> action = async () => await _translationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido mas que não existe, deve lançar exceção
        [Fact]
        public async Task DeleteFormTemplateTranslationById_FormTemplateIdIsValidButDoesntExist_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid? formTemplateId = null;
            Language language = Language.PT;

            _formTemplateRepositoryMock.Setup(temp => temp.GetFormTemplateById(formTemplateId)).ReturnsAsync(null as FormTemplate);

            //Act
            Func<Task> action = async () => await _translationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, não pode remover pois só existe um idioma, logo deve lançar exceção
        [Fact]
        public async Task DeleteFormTemplateTranslationById_FormTemplateIdIsValidButOnlyOneTranslation_ShouldThrowHttpStatusException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();
            Language language = Language.PT;

            FormTemplate formTemplate = new()
            {
                FormTemplateId = formTemplateId,
                CreatedByUserId = 123,
                CreatedDate = DateTime.UtcNow,
                Translations = new List<FormTemplateTranslation>
                {
                    new FormTemplateTranslation
                    {
                        FormTemplateTranslationId = Guid.NewGuid(),
                        FormTemplateId = formTemplateId,
                        Language = language.ToString(),
                        Title = "Title",
                        Description = "Description",
                    }
                }
            };

            _formTemplateRepositoryMock.Setup(temp => temp.GetFormTemplateById(formTemplateId)).ReturnsAsync(formTemplate);

            //Act
            Func<Task> action = async () => await _translationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, language não existe, logo deve lançar exceção
        [Fact]
        public async Task DeleteFormTemplateTranslationById_LanguageDoesntExist_ShouldThrowException()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();
            Language language = Language.PT;

            FormTemplate formTemplate = new()
            {
                FormTemplateId = formTemplateId,
                CreatedByUserId = 123,
                CreatedDate = DateTime.UtcNow,
                Translations = new List<FormTemplateTranslation>
                {
                    new FormTemplateTranslation
                    {
                        FormTemplateTranslationId = Guid.NewGuid(),
                        FormTemplateId = formTemplateId,
                        Language = Language.ES.ToString(),
                        Title = "Title",
                        Description = "Description",
                    },
                    new FormTemplateTranslation
                    {
                        FormTemplateTranslationId = Guid.NewGuid(),
                        FormTemplateId = formTemplateId,
                        Language = Language.FR.ToString(),
                        Title = "Title",
                        Description = "Description",
                    }
                }
            };

            _formTemplateRepositoryMock.Setup(temp => temp.GetFormTemplateById(formTemplateId)).ReturnsAsync(formTemplate);

            //Act
            Func<Task> action = async () => await _translationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um Guid válido e que existe, logo sucesso
        [Fact]
        public async Task DeleteFormTemplateTranslationById_ShouldBeSuccessful()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();
            Guid formTemplateTranslationId = Guid.NewGuid();
            Language language = Language.PT;

            FormTemplate formTemplate = new()
            {
                FormTemplateId = formTemplateId,
                CreatedByUserId = 123,
                CreatedDate = DateTime.UtcNow,
                Translations = new List<FormTemplateTranslation>
                {
                    new FormTemplateTranslation
                    {
                        FormTemplateTranslationId = formTemplateTranslationId,
                        FormTemplateId = formTemplateId,
                        Language = language.ToString(),
                        Title = "Titulo",
                        Description = "Descricao",
                    },
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

            _formTemplateRepositoryMock.Setup(temp => temp.GetFormTemplateById(formTemplateId)).ReturnsAsync(formTemplate);

            _translationsRepositoryMock.Setup(temp => temp.DeleteFormTemplateTranslationById(formTemplateTranslationId)).ReturnsAsync(true);

            //Act
            var result = await _translationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);

            //Assert
            Assert.True(result.Data);
        }

        #endregion
    }
}