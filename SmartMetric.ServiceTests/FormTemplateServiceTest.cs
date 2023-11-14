using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
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
    public class FormTemplateServiceTest
    {
        private readonly IFormTemplatesAdderService _formTemplatesAdderService;
        private readonly IFormTemplatesGetterService _formTemplatesGetterService;
        private readonly Mock<IFormTemplatesRepository> _formTemplatesRepositoryMock;
        private readonly IFormTemplatesRepository _formTemplatesRepository;

        private readonly IFormTemplateTranslationsAdderService _formTemplatesTranslationsAdderService;
        private readonly Mock<IFormTemplateTranslationsRepository> _formTemplatesTranslationsRepositoryMock;
        private readonly IFormTemplateTranslationsRepository _formTemplatesTranslationsRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public FormTemplateServiceTest(ITestOutputHelper testOutputHelper)
        {

            _fixture = new Fixture();
            _testOutputHelper = testOutputHelper;

            _formTemplatesRepositoryMock = new Mock<IFormTemplatesRepository>();
            _formTemplatesRepository = _formTemplatesRepositoryMock.Object;

            _formTemplatesTranslationsRepositoryMock = new Mock<IFormTemplateTranslationsRepository>();
            _formTemplatesTranslationsRepository = _formTemplatesTranslationsRepositoryMock.Object;

            var AdderloggerMock = new Mock<ILogger<FormTemplatesAdderService>>();
            var GetterloggerMock = new Mock<ILogger<FormTemplatesGetterService>>();

            var TranslationsLoggerMock = new Mock<ILogger<FormTemplateTranslationsAdderService>>();

            _formTemplatesTranslationsAdderService = new FormTemplateTranslationsAdderService(_formTemplatesTranslationsRepository, TranslationsLoggerMock.Object);

            _formTemplatesAdderService = new FormTemplatesAdderService(_formTemplatesRepository, AdderloggerMock.Object, _formTemplatesTranslationsAdderService);
            _formTemplatesGetterService = new FormTemplatesGetterService(_formTemplatesRepository, GetterloggerMock.Object);
        }

        #region AddFormTemplate

        // TESTE: Adiciona um modelo de formulário com sucesso.
        [Fact]
        public async Task AddFormTemplate_Success()
        {
            // Arrange
            var formTemplateDTOAddRequest = _fixture.Build<FormTemplateDTOAddRequest>()
                .With(temp => temp.Questions, new List<QuestionDTOAddRequest>() { _fixture.Build<QuestionDTOAddRequest>().Create() })
                .Create();

            _formTemplatesRepositoryMock
                .Setup(repo => repo.AddFormTemplate(It.IsAny<FormTemplate>()))
                .ReturnsAsync(new FormTemplate { FormTemplateId = Guid.NewGuid() });

            // Act
            var result = await _formTemplatesAdderService.AddFormTemplate(formTemplateDTOAddRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FormTemplateDTOResponse>(result);

            //_formTemplatesRepositoryMock
            //    .Verify(repo => repo.AddFormTemplate(It.IsAny<FormTemplate>()), Times.Once);
        }

        #endregion
    }
}
