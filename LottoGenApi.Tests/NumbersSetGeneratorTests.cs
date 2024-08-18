using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using LottoGenApi;

namespace LottoGenApi.Tests;

public class NumbersSetGeneratorTests
{
    [Fact]
    public async Task GenerateSetsAsListOfIntArray_ShouldReturnCorrectNumberOfSets()
    {
        // Arrange
        int[] min = [1, 1];
        int[] max = [10, 20];
        int[] numbersPerGroup = [5, 3];
        decimal[] divergence = [0.5m, 0.3m];
        int sets = 3;
        bool[] sumcheck = [true, false];
        bool[] oecheck = [false, true];

        // Act
        var result = await NumbersSetGenerator.GenerateSetsAsListOfIntArray(min, max, numbersPerGroup, divergence, sets, sumcheck, oecheck);

        // Assert
        result.Should().HaveCount(sets);
    }

    [Fact]
    public async Task GenerateSetsAsListOfIntArray_ShouldReturnNonEmptySets()
    {
        // Arrange
        int[] min = [1, 1];
        int[] max = [10, 20];
        int[] numbersPerGroup = [5, 3];
        decimal[] divergence = [0.5m, 0.3m];
        int sets = 3;
        bool[] sumcheck = [true, false];
        bool[] oecheck = [false, true];

        // Act
        var result = await NumbersSetGenerator.GenerateSetsAsListOfIntArray(min, max, numbersPerGroup, divergence, sets, sumcheck, oecheck);

        // Assert
        foreach (var numberSet in result)
        {
            numberSet.Should().NotBeEmpty();
            numberSet.Should().HaveCount(numbersPerGroup[0] + numbersPerGroup[1]);
        }
    }

    [Fact]
    public async Task GenerateSetsAsListOfIntArray_ShouldGenerateNumbersWithinRange()
    {
        // Arrange
        int[] min = [1, 1];
        int[] max = [10, 20];
        int[] numbersPerGroup = [5, 3];
        decimal[] divergence = [0.5m, 0.3m];
        int sets = 3;
        bool[] sumcheck = [true, false];
        bool[] oecheck = [false, true];

        // Act
        var result = await NumbersSetGenerator.GenerateSetsAsListOfIntArray(min, max, numbersPerGroup, divergence, sets, sumcheck, oecheck);

        // Assert
        foreach (var numberSet in result)
        {
            for (int i = 0; i < numbersPerGroup[0]; i++)
            {
                numberSet[i].Should().BeInRange(min[0], max[0]);
            }
            for (int i = numbersPerGroup[0]; i < numberSet.Length; i++)
            {
                numberSet[i].Should().BeInRange(min[1], max[1]);
            }
        }
    }

}
