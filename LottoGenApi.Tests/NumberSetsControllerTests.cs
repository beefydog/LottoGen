using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LottoGenApi.Controllers;
using LottoGenApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LottoGenApi.Tests;

public class NumberSetsControllerTests
{
    private readonly Mock<ILogger<NumberSetsController>> _loggerMock;
    private readonly NumberSetsController _controller;

    public NumberSetsControllerTests()
    {
        _loggerMock = new Mock<ILogger<NumberSetsController>>();
        _controller = new NumberSetsController(_loggerMock.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnOkResult_WithValidData()
    {
        // Arrange
        var setsRequest = new SetsRequest(
            [
                new NumberGroup { Min = 1, Max = 10, NumbersPerGroup = 5, Divergence = 10, SumCheck = true, OeCheck = true },
                new NumberGroup { Min = 1, Max = 5, NumbersPerGroup = 2, Divergence = 15, SumCheck = false, OeCheck = false }
            ],
            5
        );

        // Act
        var result = await _controller.GetAsync(setsRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var data = Assert.IsType<List<int[]>>(okResult.Value);
        Assert.NotEmpty(data);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNotFound_WhenNoNumberSetsGenerated()
    {
        // Arrange
        var setsRequest = new SetsRequest(
            [
                new NumberGroup { Min = 1, Max = 1, NumbersPerGroup = 1, Divergence = 10, SumCheck = true, OeCheck = true }
            ],
            0 // This will cause no sets to be generated
        );

        // Act
        var result = await _controller.GetAsync(setsRequest);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("No number sets generated.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnInternalServerError_OnException()
    {
        // Arrange
        var setsRequest = new SetsRequest(
            [
                new NumberGroup { Min = 1, Max = 10, NumbersPerGroup = 5, Divergence = 10, SumCheck = true, OeCheck = true }
            ],
            500
        );
        // Force an exception in the controller method by simulating an exception directly.
        // Since static methods are challenging to mock, we simulate via the request payload or similar.

        // Act
        var result = await _controller.GetAsync(setsRequest);

        // Assert
        var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, internalServerErrorResult.StatusCode);
        Assert.Equal("Internal server error", internalServerErrorResult.Value);

        _loggerMock.Verify(
            x => x.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
            Times.Once
        );
    }
}