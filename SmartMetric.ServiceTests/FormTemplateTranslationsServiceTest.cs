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

        private readonly Mock<IFormTemplateTranslationsRepository> _translationsRepositoryMock;
        private readonly IFormTemplateTranslationsRepository _translationsRepository;

        private readonly Mock<IFormTemplatesRepository> _formTemplateRepositoryMock;
        private readonly IFormTemplatesRepository _formTemplatesRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public FormTemplateTranslationsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;

            _translationsRepositoryMock = new Mock<IFormTemplateTranslationsRepository>();
            _translationsRepository = _translationsRepositoryMock.Object;

            _formTemplateRepositoryMock = new Mock<IFormTemplatesRepository>();
            _formTemplatesRepository = _formTemplateRepositoryMock.Object;

            var AdderloggerMock = new Mock<ILogger<FormTemplateTranslationsAdderService>>();
            var GetterloggerMock = new Mock<ILogger<FormTemplateTranslationsGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<FormTemplateTranslationDeleterService>>();

            _translationsAdderService = new FormTemplateTranslationsAdderService(_translationsRepository, AdderloggerMock.Object);
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

            FormTemplateTranslationDTOAddRequest request = new()
            {
                FormTemplateId = formTemplateId,
                Language = Language.EN,
                Title = "Title",
                Description = "Description",
            };

            var formTemplateTranslation = request.ToFormTemplateTranslation();
            var response = formTemplateTranslation.ToFormTemplateTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsByFormTemplateId(formTemplateId)).ReturnsAsync(formTemplateTranslationExists);

            _translationsRepositoryMock.Setup(temp => temp.AddFormTemplateTranslation(formTemplateTranslation)).ReturnsAsync(formTemplateTranslation);

            //Act
            Func<Task> action = async () => await _translationsAdderService.AddFormTemplateTranslation(request);

            //Assert
            await action.Should().ThrowAsync<HttpStatusException>();
        }

        //TESTE: fornecido um formTemplateTranslationDTOAddRequest válido, adiciona com sucesso, retorna mensagem
        [Fact]
        public async Task AddFormTemplateTranslation_FormTemplateTranslationIsValid_ShouldBeSuccessful()
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

            FormTemplateTranslationDTOAddRequest request = new()
            {
                FormTemplateId = formTemplateId,
                Language = Language.EN,
                Title = "Title",
                Description = "Description",
            };

            var formTemplateTranslation = request.ToFormTemplateTranslation();
            var response = formTemplateTranslation.ToFormTemplateTranslationDTOResponse();

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

        //        //TEST: GetAllFormTemplateTranslations por defeito deve retornar uma lista vazia;
        //        [Fact]
        //        public async Task GetAllFormTemplateTranslations_ToBeEmptyList()
        //        {
        //            //Arrange
        //            var translations = new List<FormTemplateTranslation>();

        //            _translationsRepositoryMock.Setup(temp => temp.GetAllFormTemplateTranslations()).ReturnsAsync(translations);

        //            //Act
        //            List<FormTemplateTranslationDTOResponse> response_from_get = await _translationsGetterService.GetAllFormTemplateTranslations();

        //            //Assert
        //            response_from_get.Should().BeEmpty();
        //        }

        //        //TESTE: Simular uma adição de algumas traduções e chamar GetAllFormTemplatesTranslations, deve retornar as mesmas traduções que foram adicionadas.
        //        [Fact]
        //        public async Task GetAllFormTemplateTranslations_WithSomeTranslations_ToBeSuccessful()
        //        {
        //            //Arrange
        //            List<FormTemplateTranslation> translations = new List<FormTemplateTranslation>()
        //            {
        //                _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
        //                 _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
        //                  _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create(),
        //            };

        //            List<FormTemplateTranslationDTOResponse> expected_response = translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();

        //            //Log expected_response:
        //            _testOutputHelper.WriteLine("Expected Response:");
        //            foreach (var item in expected_response)
        //            {
        //                _testOutputHelper.WriteLine(item.ToString());
        //            }

        //            _translationsRepositoryMock.Setup(temp => temp.GetAllFormTemplateTranslations()).ReturnsAsync(translations);

        //            //Act
        //            List<FormTemplateTranslationDTOResponse> actual_response = await _translationsGetterService.GetAllFormTemplateTranslations();

        //            //Log actual_response:
        //            _testOutputHelper.WriteLine("Actual Response:");
        //            foreach (var item in actual_response)
        //            {
        //                _testOutputHelper.WriteLine(item.ToString());
        //            }

        //            //Assert
        //            actual_response.Should().BeEquivalentTo(expected_response);
        //        }
        #endregion

        #region GetFormTemplateTranslationById

        //        //TESTE: Fornecido um FormTemplateTranslationId null, deve lançar um ArgumenteNullException
        //        [Fact]
        //        public async Task GetFormTemplateTranslationById_NullFormTemplateTranslationId_ToBeNull()
        //        {
        //            //Arrange
        //            Guid? id = null;

        //            //Act
        //            Func<Task> action = async () =>
        //            {
        //                await _translationsGetterService.GetFormTemplateTranslationById(id);
        //            };

        //            //Assert
        //            await action.Should().ThrowAsync<ArgumentNullException>();
        //        }

        //        //TESTE: Fornecido um FormTemplateTranslationId válido deve retornar o objeto FormTemplateTranslationDTOResponse correspondente ao id
        //        [Fact]
        //        public async Task GetFormTemplateTranslationById_WithValidId_ToBeSuccessful()
        //        {
        //            //Arrange
        //            FormTemplateTranslation translation = _fixture.Build<FormTemplateTranslation>().With(temp => temp.FormTemplate, null as FormTemplate).Create();

        //            FormTemplateTranslationDTOResponse expected_response = translation.ToFormTemplateTranslationDTOResponse();

        //            _translationsRepositoryMock.Setup(temp => temp.GetFormTemplateTranslationById(It.IsAny<Guid>())).ReturnsAsync(translation);

        //            //Act
        //            FormTemplateTranslationDTOResponse? actual_response = await _translationsGetterService.GetFormTemplateTranslationById(translation.FormTemplateTranslationId);

        //            //Assert
        //            actual_response.Should().Be(expected_response);
        //        }
        #endregion

        #region GetTranslationsByFormTemplateId

        //        //TESTE: Fornecido um FormTemplateId nulo, deve lançar um ArgumenteNullException
        //        [Fact]
        //        public async Task GetTranslationsByFormTemplateId_NullFormTemplateId_ToThrowArgumentNullException()
        //        {
        //            //Arrange
        //            Guid? formTemplateId = null;

        //            //Act
        //            Func<Task> action = async () =>
        //            {
        //                await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);
        //            };

        //            //Assert
        //            await action.Should().ThrowAsync<ArgumentNullException>();
        //        }

        //        //TESTE: Fornecido um FormTemplateId válido, mas sem traduções associadas, deve retornar uma lista vazia de FormTemplateTranslationDTOResponse
        //        [Fact]
        //        public async Task GetTranslationsByFormTemplateId_WithValidId_NoTranslations_ToBeEmptyList()
        //        {
        //            //Arrange
        //            Guid formTemplateId = Guid.NewGuid();

        //            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsByFormTemplateId(It.IsAny<Guid>())).ReturnsAsync(new List<FormTemplateTranslation>());

        //            //Act
        //            List<FormTemplateTranslationDTOResponse>? response = await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);

        //            //Assert
        //            response.Should().BeEmpty();
        //        }

        //        //TESTE: Fornecido um FormTemplateId válido, com traduções associadas, deve retornar a lista correspondente de FormTemplateTranslationDTOResponse
        //        [Fact]
        //        public async Task GetTranslationsByFormTemplateId_WithValidId_WithTranslations_ToBeSuccessful()
        //        {
        //            //Arrange
        //            Guid formTemplateId = Guid.NewGuid();

        //            List<FormTemplateTranslation> translations = new List<FormTemplateTranslation>()
        //            {
        //                _fixture.Build<FormTemplateTranslation>()
        //                .With(temp => temp.FormTemplateId, formTemplateId)
        //                .With(temp => temp.FormTemplate, null as FormTemplate)
        //                .Create(),
        //                _fixture.Build<FormTemplateTranslation>()
        //                .With(temp => temp.FormTemplateId, formTemplateId)
        //                .With(temp => temp.FormTemplate, null as FormTemplate)
        //                .Create(),
        //                _fixture.Build<FormTemplateTranslation>()
        //                .With(temp => temp.FormTemplateId, formTemplateId)
        //                .With(temp => temp.FormTemplate, null as FormTemplate)
        //                .Create(),
        //            };

        //            List<FormTemplateTranslationDTOResponse> expected_response = translations.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList();

        //            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsByFormTemplateId(It.IsAny<Guid>())).ReturnsAsync(translations);

        //            //Act
        //            List<FormTemplateTranslationDTOResponse>? actual_response = await _translationsGetterService.GetTranslationsByFormTemplateId(formTemplateId);

        //            //Assert
        //            actual_response.Should().BeEquivalentTo(expected_response);
        //        }

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


        //TESTE: fornecido um Guid válido e que existe, remove com sucesso

        //        //TESTE: recebe um Guid nulo, logo deve retornar falso
        //        [Fact]
        //        public async Task DeleteFormTemplateTranslationById_FormTemplateTranslationIdIsNull_ShouldReturnFalse()
        //        {
        //            //Arrange
        //            Guid? formTemplateTranslationId = null;
        //            Language language = Language.PT;

        //            //Act
        //            var result = await _translationsDeleterService.DeleteFormTemplateTranslationById(formTemplateTranslationId, Language.PT);

        //            //Assert
        //            Assert.False(result);
        //        }

        //        //TESTE: recebe um Guid válido que não existe, logo deve retornar falso
        //        [Fact]
        //        public async Task DeleteFormTemplateTranslationById_FormTemplateTranslationIdIsValidAndDoesntExist_ShouldReturnFalse()
        //        {
        //            //Arrange
        //            var formTemplateTranslationId = Guid.NewGuid();
        //            Language language = Language.PT;

        //            _translationsRepositoryMock
        //                .Setup(temp => temp.DeleteFormTemplateTranslationById(formTemplateTranslationId))
        //                .ReturnsAsync(false);

        //            //Act
        //            var result = await _translationsDeleterService.DeleteFormTemplateTranslationById(formTemplateTranslationId, language);

        //            //Assert
        //            Assert.False(result);
        //        }


        //        //TESTE: recebe um id válido que existe, logo retorna true
        //        [Fact]
        //        public async Task DeleteFormTemplateTranslationById_FormTemplateTranslationIdIsValidAndExists_ShouldReturnTrue()
        //        {
        //            //Arrange
        //            var formTemplateTranslationId = Guid.NewGuid();
        //            Language language = Language.PT;

        //            _translationsRepositoryMock
        //                .Setup(temp => temp.DeleteFormTemplateTranslationById(formTemplateTranslationId))
        //                .ReturnsAsync(true);

        //            //Act
        //            var result = await _translationsDeleterService.DeleteFormTemplateTranslationById(formTemplateTranslationId, language);

        //            //Assert
        //            Assert.True(result);
        //        }

        #endregion
    }
}