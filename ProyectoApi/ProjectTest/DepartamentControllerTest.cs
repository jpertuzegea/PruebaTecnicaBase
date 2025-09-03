//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using Commons.Dtos.Configurations;
using Commons.Dtos.Domains;
using Interfaces.Interfaces;
using Moq;
using Proyecto.Controllers;

namespace ProjectTest
{

    public class DepartamentControllerTest
    {
        private readonly Mock<IDepartamentService> _mockDepartamentService;
        private readonly DepartamentController _controller;


        public DepartamentControllerTest()
        {
            _mockDepartamentService = new Mock<IDepartamentService>();
            _controller = new DepartamentController(_mockDepartamentService.Object);
        }

        #region DepartamentList Tests

        [Fact]
        public async Task Departament_WhenCalled_ShouldReturnDepartamentList()
        {
            // Arrange
            var expectedResult = new ResultModel<DepartamentDto[]>
            {
                HasError = false,
                Data = new DepartamentDto[]
                {
                    new DepartamentDto { DepartamentId = 1, Name = "IT", State = 1, NameState = "Activo" },
                    new DepartamentDto { DepartamentId = 2, Name = "HR", State = 1, NameState = "Activo" }
                },
                Messages = "Departaments listed successfully"
            };

            _mockDepartamentService.Setup(x => x.DepartamentList())
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Departament();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.Value);
            _mockDepartamentService.Verify(x => x.DepartamentList(), Times.Once);
        }

        [Fact]
        public async Task Departament_WhenServiceReturnsError_ShouldReturnErrorResult()
        {
            // Arrange
            var expectedResult = new ResultModel<DepartamentDto[]>
            {
                HasError = true,
                Data = Array.Empty<DepartamentDto>(),
                Messages = "Technical error listing departaments",
                ExceptionMessage = "Database connection failed"
            };

            _mockDepartamentService.Setup(x => x.DepartamentList())
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Departament();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentList(), Times.Once);
        }

        #endregion

        #region DepartamentAdd Tests

        [Fact]
        public async Task DepartamentAdd_WithValidData_ShouldReturnSuccessResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                Name = "Finance",
                State = 1,
                NameState = "Activo"
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = false,
                Data = string.Empty,
                Messages = "Departament successfully created"
            };

            _mockDepartamentService.Setup(x => x.DepartamentAdd(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentAdd(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentAdd(departamentDto), Times.Once);
        }

        [Fact]
        public async Task DepartamentAdd_WithInvalidData_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                Name = "",
                State = 1
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = true,
                Data = string.Empty,
                Messages = "Departament name is required"
            };

            _mockDepartamentService.Setup(x => x.DepartamentAdd(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentAdd(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentAdd(departamentDto), Times.Once);
        }

        [Fact]
        public async Task DepartamentAdd_WhenServiceThrows_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                Name = "Marketing",
                State = 1
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = true,
                Data = string.Empty,
                Messages = "Technical error creating departament: Database error",
                ExceptionMessage = "System.Exception: Database error"
            };

            _mockDepartamentService.Setup(x => x.DepartamentAdd(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentAdd(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Contains("Technical error creating departament", result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentAdd(departamentDto), Times.Once);
        }

        #endregion

        #region GetDepartamentByDepartamentId Tests

        [Fact]
        public async Task GetDepartamentByDepartamentId_WithValidId_ShouldReturnDepartament()
        {
            // Arrange
            var departamentId = 1;
            var expectedDepartament = new DepartamentDto
            {
                DepartamentId = 1,
                Name = "IT",
                State = 1,
                NameState = "Activo"
            };

            var expectedResult = new ResultModel<DepartamentDto>
            {
                HasError = false,
                Data = expectedDepartament,
                Messages = "Departament found successfully"
            };

            _mockDepartamentService.Setup(x => x.GetDepartamentByDepartamentId(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetDepartamentByDepartamentId(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Equal(expectedDepartament.DepartamentId, result.Value.Data.DepartamentId);
            Assert.Equal(expectedDepartament.Name, result.Value.Data.Name);
            _mockDepartamentService.Verify(x => x.GetDepartamentByDepartamentId(departamentId), Times.Once);
        }

        [Fact]
        public async Task GetDepartamentByDepartamentId_WithInvalidId_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentId = 0;
            var expectedResult = new ResultModel<DepartamentDto>
            {
                HasError = true,
                Data = null,
                Messages = "Invalid departament ID"
            };

            _mockDepartamentService.Setup(x => x.GetDepartamentByDepartamentId(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetDepartamentByDepartamentId(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.GetDepartamentByDepartamentId(departamentId), Times.Once);
        }

        [Fact]
        public async Task GetDepartamentByDepartamentId_WhenNotFound_ShouldReturnNotFoundResult()
        {
            // Arrange
            var departamentId = 999;
            var expectedResult = new ResultModel<DepartamentDto>
            {
                HasError = false,
                Data = null,
                Messages = "Departament not found"
            };

            _mockDepartamentService.Setup(x => x.GetDepartamentByDepartamentId(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetDepartamentByDepartamentId(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Null(result.Value.Data);
            Assert.Equal("Departament not found", result.Value.Messages);
            _mockDepartamentService.Verify(x => x.GetDepartamentByDepartamentId(departamentId), Times.Once);
        }

        [Fact]
        public async Task GetDepartamentByDepartamentId_WhenServiceThrows_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentId = 1;
            var expectedResult = new ResultModel<DepartamentDto>
            {
                HasError = true,
                Data = null,
                Messages = "Technical error retrieving departament: Connection timeout",
                ExceptionMessage = "System.TimeoutException: Connection timeout"
            };

            _mockDepartamentService.Setup(x => x.GetDepartamentByDepartamentId(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetDepartamentByDepartamentId(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Contains("Technical error retrieving departament", result.Value.Messages);
            _mockDepartamentService.Verify(x => x.GetDepartamentByDepartamentId(departamentId), Times.Once);
        }

        #endregion

        #region DepartamentUpdt Tests

        [Fact]
        public async Task DepartamentUpdt_WithValidData_ShouldReturnSuccessResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                DepartamentId = 1,
                Name = "IT Updated",
                State = 1,
                NameState = "Activo"
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = false,
                Data = null,
                Messages = "Departament updated successfully"
            };

            _mockDepartamentService.Setup(x => x.DepartamentUpdate(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentUpdt(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentUpdate(departamentDto), Times.Once);
        }

        [Fact]
        public async Task DepartamentUpdt_WithInvalidId_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                DepartamentId = 0,
                Name = "Invalid",
                State = 1
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = true,
                Data = null,
                Messages = "Invalid departament ID"
            };

            _mockDepartamentService.Setup(x => x.DepartamentUpdate(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentUpdt(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentUpdate(departamentDto), Times.Once);
        }

        [Fact]
        public async Task DepartamentUpdt_WithEmptyName_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                DepartamentId = 1,
                Name = "",
                State = 1
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = true,
                Data = null,
                Messages = "Departament name is required"
            };

            _mockDepartamentService.Setup(x => x.DepartamentUpdate(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentUpdt(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentUpdate(departamentDto), Times.Once);
        }

        [Fact]
        public async Task DepartamentUpdt_WhenDepartamentNotFound_ShouldReturnNotFoundResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                DepartamentId = 999,
                Name = "Non Existent",
                State = 1
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = false,
                Data = null,
                Messages = "Departament not found"
            };

            _mockDepartamentService.Setup(x => x.DepartamentUpdate(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentUpdt(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Equal("Departament not found", result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentUpdate(departamentDto), Times.Once);
        }

        [Fact]
        public async Task DepartamentUpdt_WhenNoChangesDetected_ShouldReturnNoChangesResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                DepartamentId = 1,
                Name = "IT",
                State = 1
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = false,
                Data = null,
                Messages = "No changes detected, departament is up to date"
            };

            _mockDepartamentService.Setup(x => x.DepartamentUpdate(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentUpdt(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Equal("No changes detected, departament is up to date", result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentUpdate(departamentDto), Times.Once);
        }

        [Fact]
        public async Task DepartamentUpdt_WhenNameAlreadyExists_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentDto = new DepartamentDto
            {
                DepartamentId = 1,
                Name = "Existing Name",
                State = 1
            };

            var expectedResult = new ResultModel<string>
            {
                HasError = true,
                Data = null,
                Messages = "A departament with this name already exists"
            };

            _mockDepartamentService.Setup(x => x.DepartamentUpdate(departamentDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentUpdt(departamentDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentUpdate(departamentDto), Times.Once);
        }

        #endregion

        #region DepartamentDelete Tests

        [Fact]
        public async Task DepartamentDelete_WithValidId_ShouldReturnSuccessResult()
        {
            // Arrange
            var departamentId = 1;
            var expectedResult = new ResultModel<string>
            {
                HasError = false,
                Data = null,
                Messages = "Departament deleted successfully"
            };

            _mockDepartamentService.Setup(x => x.DepartamentDelete(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentDelete(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentDelete(departamentId), Times.Once);
        }

        [Fact]
        public async Task DepartamentDelete_WithInvalidId_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentId = 0;
            var expectedResult = new ResultModel<string>
            {
                HasError = true,
                Data = null,
                Messages = "Invalid departament ID"
            };

            _mockDepartamentService.Setup(x => x.DepartamentDelete(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentDelete(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Equal(expectedResult.Messages, result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentDelete(departamentId), Times.Once);
        }

        [Fact]
        public async Task DepartamentDelete_WhenDepartamentNotFound_ShouldReturnNotFoundResult()
        {
            // Arrange
            var departamentId = 999;
            var expectedResult = new ResultModel<string>
            {
                HasError = false,
                Data = null,
                Messages = "Departament not found"
            };

            _mockDepartamentService.Setup(x => x.DepartamentDelete(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentDelete(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Equal("Departament not found", result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentDelete(departamentId), Times.Once);
        }

        [Fact]
        public async Task DepartamentDelete_WhenAlreadyDeleted_ShouldReturnAlreadyDeletedResult()
        {
            // Arrange
            var departamentId = 1;
            var expectedResult = new ResultModel<string>
            {
                HasError = false,
                Data = null,
                Messages = "Departament was already deleted or does not exist"
            };

            _mockDepartamentService.Setup(x => x.DepartamentDelete(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentDelete(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Value.HasError);
            Assert.Equal("Departament was already deleted or does not exist", result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentDelete(departamentId), Times.Once);
        }

        [Fact]
        public async Task DepartamentDelete_WhenServiceThrows_ShouldReturnErrorResult()
        {
            // Arrange
            var departamentId = 1;
            var expectedResult = new ResultModel<string>
            {
                HasError = true,
                Data = null,
                Messages = "Technical error deleting departament: Foreign key constraint",
                ExceptionMessage = "System.Exception: Foreign key constraint"
            };

            _mockDepartamentService.Setup(x => x.DepartamentDelete(departamentId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DepartamentDelete(departamentId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.HasError);
            Assert.Contains("Technical error deleting departament", result.Value.Messages);
            _mockDepartamentService.Verify(x => x.DepartamentDelete(departamentId), Times.Once);
        }

        #endregion

        #region Constructor Tests

        [Fact]
        public async Task Constructor_WithNullService_ShouldCreateInstanceButFailOnMethodCall()
        {
            // Arrange & Act
            var controller = new DepartamentController(null);

            // Assert
            Assert.NotNull(controller);

            // Verify that calling methods with null service throws NullReferenceException
            await Assert.ThrowsAsync<NullReferenceException>(async () => await controller.Departament());
        }


        [Fact]
        public void Constructor_WithValidService_ShouldCreateInstance()
        {
            // Arrange
            var mockService = new Mock<IDepartamentService>();

            // Act
            var controller = new DepartamentController(mockService.Object);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task Constructor_WithValidService_ShouldAllowMethodCalls()
        {
            // Arrange
            var mockService = new Mock<IDepartamentService>();
            var expectedResult = new ResultModel<DepartamentDto[]>
            {
                HasError = false,
                Data = Array.Empty<DepartamentDto>(),
                Messages = "Test"
            };

            mockService.Setup(x => x.DepartamentList()).ReturnsAsync(expectedResult);
            var controller = new DepartamentController(mockService.Object);

            // Act
            var result = await controller.Departament();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.Value);
        }

        #endregion
    }
}