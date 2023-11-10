using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Services;
using SmartMetric.Core.ServicesContracts;
using Xunit.Abstractions;

namespace SmartMetric.ServiceTests
{
    public class SingleChoiceOptionTranslationsTest
    {

        private readonly ISingleChoiceOptionTranslationsAdderService _translationsAdderService;
        private readonly ISingleChoiceOptionTranslationsGetterService _translationsGetterService;

        private readonly Mock<ISingleChoiceOptionTranslationsRepository> _translationsRepositoryMock;
        private readonly ISingleChoiceOptionTranslationsRepository _translationsRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public SingleChoiceOptionTranslationsTest(ITestOutputHelper testOutputHelper)
        {

            _fixture = new Fixture();
            _translationsRepositoryMock = new Mock<ISingleChoiceOptionTranslationsRepository>();
            _translationsRepository = _translationsRepositoryMock.Object;
            _testOutputHelper = testOutputHelper;

            var AdderloggerMock = new Mock<ILogger<SingleChoiceOptionTranslationsAdderService>>();
            var GetterloggerMock = new Mock<ILogger<SingleChoiceOptionTranslationsGetterService>>();

            _translationsAdderService = new SingleChoiceOptionTranslationsAdderService(_translationsRepository, AdderloggerMock.Object);
            _translationsGetterService = new SingleChoiceOptionTranslationsGetterService(_translationsRepository, GetterloggerMock.Object);
        }

        #region AddSingleChoiceOptionTranslation

        //TESTE: Fornecido um objeto SingleChoiceOptionTranslationDTOAddRequest como null, deve lançar um ArgumentNullException
        [Fact]
        public async Task AddSingleChoiceOptionTranslation_WithNullObject_ToBeArgumentNullException()
        {
            //Arrange
            SingleChoiceOptionTranslationDTOAddRequest? request = null;

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddSingleChoiceOptionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //TESTE: Fornecido um SingleChoiceOptionId válido, mas com Language nulo, deve lançar um ArgumentException
        [Fact]
        public async Task AddSingleChoiceOptionTranslation_WithValidSingleChoiceOptionIdAndNullLanguage_ToBeArgumentException()
        {
            //Arrange
            SingleChoiceOptionTranslationDTOAddRequest request = new SingleChoiceOptionTranslationDTOAddRequest
            {
                SingleChoiceOptionId = Guid.NewGuid(),
                Language = null,
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddSingleChoiceOptionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: Fornecido um SingleChoiceOptionId nulo, mas com Language válido, deve lançar um ArgumentException
        [Fact]
        public async Task AddSingleChoiceOptionTranslation_WithNullSingleChoiceOptionIdAndValidLanguage_ToBeArgumentException()
        {
            //Arrange
            SingleChoiceOptionTranslationDTOAddRequest request = new SingleChoiceOptionTranslationDTOAddRequest
            {
                SingleChoiceOptionId = null,
                Language = Language.PT,
                Description = "Sample Description"
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddSingleChoiceOptionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: Fornecido um SingleChoiceOptionId nulo, mas com Language válido, deve lançar um ArgumentException
        [Fact]
        public async Task AddSingleChoiceOptionTranslation_WithEmptyDescription_ToBeArgumentException()
        {
            //Arrange
            SingleChoiceOptionTranslationDTOAddRequest request = new SingleChoiceOptionTranslationDTOAddRequest
            {
                SingleChoiceOptionId = Guid.NewGuid(),
                Language = Language.PT,
                Description = ""
            };

            //Act
            Func<Task> action = async () =>
            {
                await _translationsAdderService.AddSingleChoiceOptionTranslation(request);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //TESTE: Fornecido um objeto SingleChoiceOptionTranslationDTOAddRequest com detalhes corretos, deve inserir o objeto na lista de traduções e retornar um objeto SingleChoiceOptionTranslationDTOResponse que inclua o recente SingleChoiceOptionTranslation Id gerado.
        [Fact]
        public async Task AddSingleChoiceOptionTranslation_WithFullDetails_ToBeSuccessful()
        {
            //Arranje
            SingleChoiceOptionTranslationDTOAddRequest request = _fixture.Build<SingleChoiceOptionTranslationDTOAddRequest>().With(temp => temp.Language, Language.PT).Create();

            SingleChoiceOptionTranslation translation = request.ToSingleChoiceOptionTranslation();
            SingleChoiceOptionTranslationDTOResponse response = translation.ToSingleChoiceOptionTranslationDTOResponse();

            _translationsRepositoryMock.Setup(temp => temp.AddSingleChoiceOptionTranslation(It.IsAny<SingleChoiceOptionTranslation>())).ReturnsAsync(translation);

            //Act
            SingleChoiceOptionTranslationDTOResponse response_from_add = await _translationsAdderService.AddSingleChoiceOptionTranslation(request);

            response.SingleChoiceOptionTranslationId = response_from_add.SingleChoiceOptionTranslationId;

            //Assert
            response_from_add.Should().NotBe(Guid.Empty);
            response_from_add.Should().Be(response);
        }

        #endregion

    }
}
