//using AutoFixture;
//using FluentAssertions;
//using Microsoft.Extensions.Logging;
//using Moq;
//using SmartMetric.Core.Domain.Entities;
//using SmartMetric.Core.Domain.RepositoryContracts;
//using SmartMetric.Core.DTO.AddRequest;
//using SmartMetric.Core.DTO.Response;
//using SmartMetric.Core.Enums;
//using SmartMetric.Core.Services.Adders;
//using SmartMetric.Core.Services.Deleters;
//using SmartMetric.Core.Services.Getters;
//using SmartMetric.Core.ServicesContracts.Adders;
//using SmartMetric.Core.ServicesContracts.Deleters;
//using SmartMetric.Core.ServicesContracts.Getters;
//using Xunit.Abstractions;

//namespace SmartMetric.ServiceTests
//{
//    public class SingleChoiceOptionTranslationsServiceTest
//    {

//        private readonly ISingleChoiceOptionTranslationsAdderService _translationsAdderService;
//        private readonly ISingleChoiceOptionTranslationsGetterService _translationsGetterService;
//        private readonly ISingleChoiceOptionTranslationDeleterService _translationsDeleterService;

//        private readonly Mock<ISingleChoiceOptionTranslationsRepository> _translationsRepositoryMock;
//        private readonly ISingleChoiceOptionTranslationsRepository _translationsRepository;

//        private readonly ITestOutputHelper _testOutputHelper;
//        private readonly IFixture _fixture;

//        public SingleChoiceOptionTranslationsServiceTest(ITestOutputHelper testOutputHelper)
//        {

//            _fixture = new Fixture();
//            _translationsRepositoryMock = new Mock<ISingleChoiceOptionTranslationsRepository>();
//            _translationsRepository = _translationsRepositoryMock.Object;
//            _testOutputHelper = testOutputHelper;

//            var AdderloggerMock = new Mock<ILogger<SingleChoiceOptionTranslationsAdderService>>();
//            var GetterloggerMock = new Mock<ILogger<SingleChoiceOptionTranslationsGetterService>>();
//            var DeleterloggerMock = new Mock<ILogger<SingleChoiceOptionTranslationDeleterService>>();

//            _translationsAdderService = new SingleChoiceOptionTranslationsAdderService(_translationsRepository, AdderloggerMock.Object);
//            _translationsGetterService = new SingleChoiceOptionTranslationsGetterService(_translationsRepository, GetterloggerMock.Object);
//            _translationsDeleterService = new SingleChoiceOptionTranslationDeleterService(_translationsRepository, DeleterloggerMock.Object);
//        }

//        #region AddSingleChoiceOptionTranslation

//        //TESTE: Fornecido um objeto SingleChoiceOptionTranslationDTOAddRequest como null, deve lançar um ArgumentNullException
//        [Fact]
//        public async Task AddSingleChoiceOptionTranslation_WithNullObject_ToBeArgumentNullException()
//        {
//            //Arrange
//            SingleChoiceOptionTranslationDTOAddRequest? request = null;

//            //Act
//            Func<Task> action = async () =>
//            {
//                await _translationsAdderService.AddSingleChoiceOptionTranslation(request);
//            };

//            //Assert
//            await action.Should().ThrowAsync<ArgumentNullException>();
//        }

//        //TESTE: Fornecido um SingleChoiceOptionId válido, mas com Language nulo, deve lançar um ArgumentException
//        [Fact]
//        public async Task AddSingleChoiceOptionTranslation_WithValidSingleChoiceOptionIdAndNullLanguage_ToBeArgumentException()
//        {
//            //Arrange
//            SingleChoiceOptionTranslationDTOAddRequest request = new SingleChoiceOptionTranslationDTOAddRequest
//            {
//                SingleChoiceOptionId = Guid.NewGuid(),
//                Language = null,
//                Description = "Sample Description"
//            };

//            //Act
//            Func<Task> action = async () =>
//            {
//                await _translationsAdderService.AddSingleChoiceOptionTranslation(request);
//            };

//            //Assert
//            await action.Should().ThrowAsync<ArgumentException>();
//        }

//        //TESTE: Fornecido um SingleChoiceOptionId nulo, mas com Language válido, deve lançar um ArgumentException
//        [Fact]
//        public async Task AddSingleChoiceOptionTranslation_WithNullSingleChoiceOptionIdAndValidLanguage_ToBeArgumentException()
//        {
//            //Arrange
//            SingleChoiceOptionTranslationDTOAddRequest request = new SingleChoiceOptionTranslationDTOAddRequest
//            {
//                SingleChoiceOptionId = null,
//                Language = Language.PT,
//                Description = "Sample Description"
//            };

//            //Act
//            Func<Task> action = async () =>
//            {
//                await _translationsAdderService.AddSingleChoiceOptionTranslation(request);
//            };

//            //Assert
//            await action.Should().ThrowAsync<ArgumentException>();
//        }

//        //TESTE: Fornecido um SingleChoiceOptionId nulo, mas com Language válido, deve lançar um ArgumentException
//        [Fact]
//        public async Task AddSingleChoiceOptionTranslation_WithEmptyDescription_ToBeArgumentException()
//        {
//            //Arrange
//            SingleChoiceOptionTranslationDTOAddRequest request = new SingleChoiceOptionTranslationDTOAddRequest
//            {
//                SingleChoiceOptionId = Guid.NewGuid(),
//                Language = Language.PT,
//                Description = ""
//            };

//            //Act
//            Func<Task> action = async () =>
//            {
//                await _translationsAdderService.AddSingleChoiceOptionTranslation(request);
//            };

//            //Assert
//            await action.Should().ThrowAsync<ArgumentException>();
//        }

//        //TESTE: Fornecido um objeto SingleChoiceOptionTranslationDTOAddRequest com detalhes corretos, deve inserir o objeto na lista de traduções e retornar um objeto SingleChoiceOptionTranslationDTOResponse que inclua o recente SingleChoiceOptionTranslation Id gerado.
//        [Fact]
//        public async Task AddSingleChoiceOptionTranslation_WithFullDetails_ToBeSuccessful()
//        {
//            //Arranje
//            SingleChoiceOptionTranslationDTOAddRequest request = _fixture.Build<SingleChoiceOptionTranslationDTOAddRequest>().With(temp => temp.Language, Language.PT).Create();

//            SingleChoiceOptionTranslation translation = request.ToSingleChoiceOptionTranslation();
//            SingleChoiceOptionTranslationDTOResponse response = translation.ToSingleChoiceOptionTranslationDTOResponse();

//            _translationsRepositoryMock.Setup(temp => temp.AddSingleChoiceOptionTranslation(It.IsAny<SingleChoiceOptionTranslation>())).ReturnsAsync(translation);

//            //Act
//            SingleChoiceOptionTranslationDTOResponse response_from_add = await _translationsAdderService.AddSingleChoiceOptionTranslation(request);

//            response.SingleChoiceOptionTranslationId = response_from_add.SingleChoiceOptionTranslationId;

//            //Assert
//            response_from_add.Should().NotBe(Guid.Empty);
//            response_from_add.Should().Be(response);
//        }

//        #endregion

//        #region GetAllSingleChoiceOptionTranslations

//        //TESTE: GetAllSingleChoiceOptionTranslations por defeito deve retornar uma lista vazia;
//        [Fact]
//        public async Task GetAllSingleChoiceOptionTranslations_ToBeEmptyList()
//        {
//            //Arrange
//            var translations = new List<SingleChoiceOptionTranslation>();

//            _translationsRepositoryMock.Setup(temp => temp.GetAllSingleChoiceOptionTranslations()).ReturnsAsync(translations);

//            //Act
//            List<SingleChoiceOptionTranslationDTOResponse> response = await _translationsGetterService.GetAllSingleChoiceOptionTranslations();

//            //Assert
//            response.Should().BeEmpty();
//        }

//        //TESTE: Simular uma adição de algumas traduções e chamar GetAllSingleChoiceOptionTranslations, deve retornar as mesmas traduções que foram adicionadas.
//        [Fact]
//        public async Task GetAllSingleChoiceOptionTranslations_WithSomeTranslations_ToBeSuccessful()
//        {
//            //Arrange
//            List<SingleChoiceOptionTranslation> translations = new List<SingleChoiceOptionTranslation>
//        {
//            new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = Guid.NewGuid(), Language = "PT", Description = "Sample Description" },
//            new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = Guid.NewGuid(), Language = "EN", Description = "Sample Description" },
//            new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = Guid.NewGuid(), Language = "ES", Description = "Sample Description" }
//        };

//            List<SingleChoiceOptionTranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToSingleChoiceOptionTranslationDTOResponse()).ToList();

//            _translationsRepositoryMock.Setup(temp => temp.GetAllSingleChoiceOptionTranslations()).ReturnsAsync(translations);

//            //Act
//            List<SingleChoiceOptionTranslationDTOResponse> actualResponse = await _translationsGetterService.GetAllSingleChoiceOptionTranslations();

//            //Assert
//            actualResponse.Should().BeEquivalentTo(expectedResponse);
//        }

//        #endregion

//        #region GetSingleChoiceOptionTranslationById

//        //TESTE: Fornecido um SingleChoiceOptionTranslationId nulo, deve lançar um ArgumentNullException
//        [Fact]
//        public async Task GetSingleChoiceOptionTranslationById_WithNullId_ToBeArgumentNullException()
//        {
//            //Arrange
//            Guid? id = null;

//            //Act
//            Func<Task> action = async () =>
//            {
//                await _translationsGetterService.GetSingleChoiceOptionTranslationById(id);
//            };

//            //Assert
//            await action.Should().ThrowAsync<ArgumentNullException>();
//        }

//        //TESTE: Fornecido um SingleChoiceOptionTranslationId válido, mas sem tradução associada, deve retornar null como SingleChoiceOptionTranslationDTOResponse
//        [Fact]
//        public async Task GetSingleChoiceOptionTranslationById_WithValidId_NoTranslation_ToBeNull()
//        {
//            //Arrange
//            Guid id = Guid.NewGuid();

//            _translationsRepositoryMock.Setup(temp => temp.GetSingleChoiceOptionTranslationById(It.IsAny<Guid>())).ReturnsAsync((SingleChoiceOptionTranslation)null);

//            //Act
//            SingleChoiceOptionTranslationDTOResponse? response = await _translationsGetterService.GetSingleChoiceOptionTranslationById(id);

//            //Assert
//            response.Should().BeNull();
//        }

//        //TESTE: Fornecido um SingleChoiceOptionTranslationId válido, com tradução associada, deve retornar a tradução correspondente
//        [Fact]
//        public async Task GetSingleChoiceOptionTranslationById_WithValidId_WithTranslation_ToBeSuccessful()
//        {
//            //Arrange
//            Guid id = Guid.NewGuid();
//            SingleChoiceOptionTranslation translation = new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = id, SingleChoiceOptionId = Guid.NewGuid(), Language = "PT", Description = "Sample Description" };

//            _translationsRepositoryMock.Setup(temp => temp.GetSingleChoiceOptionTranslationById(It.IsAny<Guid>())).ReturnsAsync(translation);

//            //Act
//            SingleChoiceOptionTranslationDTOResponse? response = await _translationsGetterService.GetSingleChoiceOptionTranslationById(id);

//            //Assert
//            response.Should().NotBeNull();
//            response.Should().BeEquivalentTo(translation.ToSingleChoiceOptionTranslationDTOResponse());
//        }

//        #endregion

//        #region GetTranslationsBySingleChoiceOptionId

//        //TESTE: Fornecido um SingleChoiceOptionId nulo, deve lançar um ArgumentNullException
//        [Fact]
//        public async Task GetTranslationsBySingleChoiceOptionId_WithNullId_ToBeArgumentNullException()
//        {
//            //Arrange
//            Guid? id = null;

//            //Act
//            Func<Task> action = async () =>
//            {
//                await _translationsGetterService.GetTranslationsBySingleChoiceOptionId(id);
//            };

//            //Assert
//            await action.Should().ThrowAsync<ArgumentNullException>();
//        }

//        //TESTE: Fornecido um SingleChoiceOptionId válido, mas sem traduções associadas, deve retornar uma lista vazia de SingleChoiceOptionTranslationDTOResponse
//        [Fact]
//        public async Task GetTranslationsBySingleChoiceOptionId_WithValidId_NoTranslations_ToBeEmptyList()
//        {
//            //Arrange
//            Guid id = Guid.NewGuid();

//            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsBySingleChoiceOptionId(It.IsAny<Guid>())).ReturnsAsync(new List<SingleChoiceOptionTranslation>());

//            //Act
//            List<SingleChoiceOptionTranslationDTOResponse>? response = await _translationsGetterService.GetTranslationsBySingleChoiceOptionId(id);

//            //Assert
//            response.Should().BeEmpty();
//        }

//        //TESTE: Fornecido um SingleChoiceOptionId válido, com algumas traduções associadas, deve retornar a lista de traduções correspondentes
//        [Fact]
//        public async Task GetTranslationsBySingleChoiceOptionId_WithValidId_WithSomeTranslations_ToBeSuccessful()
//        {
//            //Arrange
//            Guid id = Guid.NewGuid();
//            List<SingleChoiceOptionTranslation> translations = new List<SingleChoiceOptionTranslation>
//        {
//            new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = id, Language = "PT", Description = "Sample Description" },
//            new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = id, Language = "EN", Description = "Sample Description" },
//            new SingleChoiceOptionTranslation { SingleChoiceOptionTranslationId = Guid.NewGuid(), SingleChoiceOptionId = id, Language = "ES", Description = "Sample Description" }
//        };

//            List<SingleChoiceOptionTranslationDTOResponse> expectedResponse = translations.Select(temp => temp.ToSingleChoiceOptionTranslationDTOResponse()).ToList();

//            _translationsRepositoryMock.Setup(temp => temp.GetTranslationsBySingleChoiceOptionId(It.IsAny<Guid>())).ReturnsAsync(translations);

//            //Act
//            List<SingleChoiceOptionTranslationDTOResponse>? actualResponse = await _translationsGetterService.GetTranslationsBySingleChoiceOptionId(id);

//            //Assert
//            actualResponse.Should().BeEquivalentTo(expectedResponse);
//        }

//        #endregion

//        #region DeleteSingleChoiceOptionTranslationById Tests

//        //TESTE: recebe um Guid nulo, logo deve retornar falso
//        [Fact]
//        public async Task DeleteSingleChoiceOptionTranslationById_SingleChoiceOptionTranslationIdIsNull_ShouldReturnFalse()
//        {
//            //Arrange
//            Guid? singleChoiceOptionTranslationId = null;

//            //Act
//            var result = await _translationsDeleterService.DeleteSingleChoiceOptionTranslationById(singleChoiceOptionTranslationId);

//            //Assert
//            Assert.False(result);
//        }

//        //TESTE: recebe um id válido que não existe, logo deve retornar falso
//        [Fact]
//        public async Task DeleteSingleChoiceOptionTranslationById_SingleChoiceOptionTranslationIdIsValidAndDoesntExist_ShouldReturnFalse()
//        {
//            //Arrange
//            var singleChoiceOptionTranslationId = Guid.NewGuid();

//            _translationsRepositoryMock
//                .Setup(temp => temp.DeleteSingleChoiceOptionTranslationById(singleChoiceOptionTranslationId))
//                .ReturnsAsync(false);

//            //Act
//            var result = await _translationsDeleterService.DeleteSingleChoiceOptionTranslationById(singleChoiceOptionTranslationId);

//            //Assert
//            Assert.False(result);
//        }

//        //TESTE: recebe um id válido que existe, logo retorna true
//        [Fact]
//        public async Task DeleteSingleChoiceOptionTranslationById_SingleChoiceOptionTranslationIdIsValidAndExists_ShouldReturnTrue()
//        {
//            //Arrange
//            var singleChoiceOptionTranslationId = Guid.NewGuid();

//            _translationsRepositoryMock
//                .Setup(temp => temp.DeleteSingleChoiceOptionTranslationById(singleChoiceOptionTranslationId)).ReturnsAsync(true);

//            //Act
//            var result = await _translationsDeleterService.DeleteSingleChoiceOptionTranslationById(singleChoiceOptionTranslationId);

//            //Assert
//            Assert.True(result);
//        }

//        #endregion
//    }
//}
