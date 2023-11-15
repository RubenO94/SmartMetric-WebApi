using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.Services.Deleters;
using SmartMetric.Core.ServicesContracts.Deleters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SmartMetric.ServiceTests
{
    public class SingleChoiceOptionServiceTest
    {
        #region Variables

        //private readonly ISingleChoiceOptionAdderService _singleChoiceOptionAdderService;
        //private readonly ISingleChoiceOptionGetterService _singleChoiceOptionGetterService;
        private readonly ISingleChoiceOptionDeleterService _singleChoiceOptionDeleterService;

        private readonly Mock<ISingleChoiceOptionRepository> _singleChoiceOptionRepositoryMock;
        private readonly ISingleChoiceOptionRepository _singleChoiceOptionRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        #endregion

        #region Constructor

        public SingleChoiceOptionServiceTest (ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _singleChoiceOptionRepositoryMock = new Mock<ISingleChoiceOptionRepository>();
            _singleChoiceOptionRepository = _singleChoiceOptionRepositoryMock.Object;
            _testOutputHelper = testOutputHelper;

            //var AdderLoggerMock = new Mock<ILogger<SingleChoiceOptionAdderService>>();
            //var GetterLoggerMock = new Mock<ILogger<SingleChoiceOptionGetterService>>();
            var DeleterLoggerMock = new Mock<ILogger<SingleChoiceOptionDeleterService>>();

            //_singleChoiceOptionAdderService = new SingleChoiceOptionAdderService(_singleChoiceOptionRepository, AdderLoggerMock.Object);
            //_singleChoiceOptionGetterService = new SingleChoiceOptionGetterService(_singleChoiceOptionRepository, GetterLoggerMock.Object);
            _singleChoiceOptionDeleterService = new SingleChoiceOptionDeleterService(_singleChoiceOptionRepository, DeleterLoggerMock.Object);
        }

        #endregion

        #region DeleteSingleChoiceOptionById Tests

        //TESTE: recebe um Guid nulo, logo deve retornar falso
        [Fact]
        public async Task DeleteSingleChoiceOptionById_SingleChoiceOptionIdIsNull_ShouldReturnFalse()
        {
            //Arrange
            Guid? singleChoiceOptionId = null;

            //Act
            var result = await _singleChoiceOptionDeleterService.DeleteSingleChoiceOptionById(singleChoiceOptionId);

            //Assert
            Assert.False(result);
        }

        //TESTE: recebe um id válido que não existe, logo deve retornar falso
        [Fact]
        public async Task DeleteSingleChoiceOptionById_SingleChoiceOptionIdIsValidAndDoesntExist_ShouldReturnFalse()
        {
            //Arrange
            var singleChoiceOptionId = Guid.NewGuid();

            _singleChoiceOptionRepositoryMock
                .Setup(temp => temp.DeleteSingleChoiceOptionById(singleChoiceOptionId))
                .ReturnsAsync(false);

            //Act
            var result = await _singleChoiceOptionDeleterService.DeleteSingleChoiceOptionById(singleChoiceOptionId);

            //Assert
            Assert.False(result);
        }

        //TESTE: recebe um id válido que existe, logo retorna true
        [Fact]
        public async Task DeleteSingleChoiceOptionById_SingleChoiceOptionIdIsValidAndExists_ShouldReturnTrue()
        {
            //Arrange
            var singleChoiceOptionId = Guid.NewGuid();

            _singleChoiceOptionRepositoryMock
                .Setup(temp => temp.DeleteSingleChoiceOptionById(singleChoiceOptionId)).ReturnsAsync(true);

            //Act
            var result = await _singleChoiceOptionDeleterService.DeleteSingleChoiceOptionById(singleChoiceOptionId);

            //Assert
            Assert.True(result);
        }

        #endregion
    }
}
