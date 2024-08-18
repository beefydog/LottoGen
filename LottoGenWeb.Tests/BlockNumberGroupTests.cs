using Xunit;
using Bunit;
using LottoGenWeb.Controls;
using LottoGenWeb.Models;

namespace LottoGenWeb.Tests
{
    public class BlockNumberGroupTests
    {
        [Fact]
        public void BlockNumberGroup_ShouldRenderCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();

            // Instantiate NumberGroup with the required constructor arguments
            var numberGroup = new NumberGroup(
                groupId: 1,
                enabled: true,
                minValue: 10,
                maxValue: 100,
                numbersPerGroup: 5,
                divergence: 10,
                checkSumEnabled: true,
                checkOEEnabled: false
            );

            // Render the component with the required parameters
            var component = ctx.RenderComponent<BlockNumberGroup>(parameters => parameters
                .Add(p => p.Ng, numberGroup));

            // Act
            var renderedMarkup = component.Markup;

            // Assert
            Assert.Contains("Group1", renderedMarkup);  
        }
    }
}
