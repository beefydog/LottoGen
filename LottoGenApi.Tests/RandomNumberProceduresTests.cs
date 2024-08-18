using Xunit;
using FluentAssertions;
using LottoGenApi;

namespace LottoGenApi.Tests;

public class RandomNumberProceduresTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 4 }, true)]   // 2 evens, 2 odds
    [InlineData(new int[] { 1, 3, 5, 7 }, false)]  // 4 odds, 0 evens
    [InlineData(new int[] { 2, 4, 6, 8 }, false)]  // 0 odds, 4 evens
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, true)]  // 2 evens, 3 odds (valid ratio)
    public void OddEvenRatioGood_ShouldReturnExpectedResult(int[] testArray, bool expected)
    {
        // Act
        var result = RandomNumberProcedures.OddEvenRatioGood(testArray);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 10, 4)]
    [InlineData(0, 15, 4)]
    [InlineData(10, 20, 5)]
    public void GetNumFromMinToMax_ShouldReturnNumberWithinRange(int min, int max, int bits)
    {
        // Act
        var result = RandomNumberProcedures.GetNumFromMinToMax(min, max, bits);

        // Assert
        result.Should().BeInRange(min, max);
    }

    [Fact]
    public void GenerateBoolean_ShouldReturnTrueOrFalse()
    {
        // Act
        var result = RandomNumberProcedures.GenerateBoolean();

        // Assert
        (result == true || result == false).Should().BeTrue();
    }


    [Theory]
    [InlineData(5, 1, 10, 4, 10, true, true, true)]
    [InlineData(5, 1, 10, 4, 10, false, true, true)]
    [InlineData(5, 1, 10, 4, 10, true, false, true)]
    [InlineData(5, 1, 10, 4, 10, true, true, false)]
    public void ComputeNumberSet2_ShouldReturnValidNumberSet(int numbersPerGroup, int min, int max, int bits, decimal divergence, bool sort, bool sumCheck, bool oeCheck)
    {
        // Act
        var result = RandomNumberProcedures.ComputeNumberSet2(numbersPerGroup, min, max, bits, divergence, sort, sumCheck, oeCheck);

        // Assert
        result.Should().HaveCount(numbersPerGroup);
        result.Should().OnlyHaveUniqueItems();
        result.Should().OnlyContain(x => x >= min && x <= max);

        if (oeCheck)
        {
            RandomNumberProcedures.OddEvenRatioGood(result).Should().BeTrue();
        }

        if (sort)
        {
            var sortedSet = result.OrderBy(n => n).ToArray();
            result.Should().Equal(sortedSet);
        }
    }
}
