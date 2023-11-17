using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Services.Adders;
using SmartMetric.Core.Services.Deleters;
using SmartMetric.Core.Services.Getters;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.ServicesContracts.Getters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SmartMetric.ServiceTests
{
    public class FormTemplateServiceTest
    {
        private readonly IFormTemplatesAdderService _formTemplatesAdderService;
        private readonly IFormTemplatesGetterService _formTemplatesGetterService;
        private readonly IFormTemplatesDeleterService _formTemplatesDeleterService;
        private readonly Mock<IFormTemplatesRepository> _formTemplatesRepositoryMock;
        private readonly IFormTemplatesRepository _formTemplatesRepository;

        //private readonly IFormTemplateTranslationsAdderService _formTemplatesTranslationsAdderService;
        //private readonly Mock<IFormTemplateTranslationsRepository> _formTemplatesTranslationsRepositoryMock;
        //private readonly IFormTemplateTranslationsRepository _formTemplatesTranslationsRepository;

        //private readonly IQuestionAdderService _questionAdderService;
        //private readonly Mock<IQuestionRepository> _questionRepositoryMock;
        //private readonly IQuestionRepository _questionRepository;

        //private readonly ISingleChoiceOptionsAdderService _singleChoiceOptionsAdderService;
        //private readonly Mock<ISingleChoiceOptionRepository> _singleChoiceOptionRepositoryMock;
        //private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;

        //private readonly IRatingOptionAdderService _ratingOptionAdderService;
        //private readonly Mock<IRatingOptionRepository> _ratingOptionRepositoryMock;
        //private readonly IRatingOptionRepository _ratingOptionRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public FormTemplateServiceTest(ITestOutputHelper testOutputHelper)
        {

            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;

            _formTemplatesRepositoryMock = new Mock<IFormTemplatesRepository>();
            _formTemplatesRepository = _formTemplatesRepositoryMock.Object;

            //_formTemplatesTranslationsRepositoryMock = new Mock<IFormTemplateTranslationsRepository>();
            //_formTemplatesTranslationsRepository = _formTemplatesTranslationsRepositoryMock.Object;

            //_questionRepositoryMock = new Mock<IQuestionRepository>();
            //_questionRepository = _questionRepositoryMock.Object;

            //_singleChoiceOptionRepositoryMock = new Mock<ISingleChoiceOptionRepository>();
            //_singleChoiceOptionRepository = _singleChoiceOptionRepositoryMock.Object;

            //_ratingOptionRepositoryMock = new Mock<IRatingOptionRepository>();
            //_ratingOptionRepository = _ratingOptionRepositoryMock.Object;

            var AdderloggerMock = new Mock<ILogger<FormTemplatesAdderService>>();
            //var GetterloggerMock = new Mock<ILogger<FormTemplatesGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<FormTemplatesDeleterService>>();
            //var TranslationsLoggerMock = new Mock<ILogger<FormTemplateTranslationsAdderService>>();

            //var QuestionLogger = new Mock<ILogger<QuestionAdderService>>();
            //var SingleChoiceOptionLogger = new Mock<ILogger<SingleChoiceOptionAdderSerive>>();
            //var RatingOptionLogger = new Mock<ILogger<RatingOptionAdderService>>();

            //_questionAdderService = new QuestionAdderService(_questionRepository, QuestionLogger.Object);
            //_singleChoiceOptionsAdderService = new SingleChoiceOptionAdderSerive(_singleChoiceOptionRepository, SingleChoiceOptionLogger.Object);
            //_formTemplatesTranslationsAdderService = new FormTemplateTranslationsAdderService(_formTemplatesTranslationsRepository, TranslationsLoggerMock.Object);
            _formTemplatesAdderService = new FormTemplatesAdderService(_formTemplatesRepository, AdderloggerMock.Object);
            _formTemplatesDeleterService = new FormTemplatesDeleterService(_formTemplatesRepository, _formTemplatesGetterService!, DeleterLoggerMock.Object);
        }

        #region AddFormTemplate

        // TESTE: Adiciona um modelo de formulário com sucesso.
        [Fact]
        public async Task AddFormTemplate_Success()
        {
            // Arrange
            var formTemplateDTOAddRequest = _fixture.Build<FormTemplateDTOAddRequest>()
                .Without(temp => temp.Translations) // Lista de traduções vazia
                .Create();

            _formTemplatesRepositoryMock
                .Setup(repo => repo.AddFormTemplate(It.IsAny<FormTemplate>()))
                .ReturnsAsync(new FormTemplate { FormTemplateId = Guid.NewGuid() });

            // Act
            var result = await _formTemplatesAdderService.AddFormTemplate(formTemplateDTOAddRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FormTemplateDTOResponse>(result);

            _formTemplatesRepositoryMock
                .Verify(repo => repo.AddFormTemplate(It.IsAny<FormTemplate>()), Times.Once);
        }

        #endregion

        #region DeleteFormTemplateById

        //TESTE: recebe um Guid nulo, logo deve retornar falso
        [Fact]
        public async Task DeleteFormTemplateById_FormTemplateIdIsNull()
        {
            //Arrange
            Guid? formTemplateId = null;

            //Act
            var result = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);

            //Assert
            Assert.False(result.Data);
        }

        //TESTE: recebe um id válido que não existe, logo deve retornar falso
        [Fact]
        public async Task DeleteFromTemplateById_FormTemplateIdIsValidButDoesntExist()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            _formTemplatesRepositoryMock
                .Setup(temp => temp.DeleteFormTemplateById(formTemplateId))
                .ReturnsAsync(false);

            //Act
            var result = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);

            //Assert
            Assert.False(result.Data);
        }

        //TESTE: recebe um id válido que existe, logo retorna true
        [Fact]
        public async Task DeleteFromTemplateById_FormTemplateIdIsValidAndExist()
        {
            //Arrange
            Guid formTemplateId = Guid.NewGuid();

            _formTemplatesRepositoryMock
                .Setup(temp => temp.DeleteFormTemplateById(formTemplateId))
                .ReturnsAsync(true);

            //Act
            var result = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);

            //Assert
            Assert.True(result.Data);
        }

        #endregion
    }
}
