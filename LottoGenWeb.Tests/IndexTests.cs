using Xunit;
using Bunit;
using Moq;
using LottoGenWeb.Pages;
using LottoGenWeb.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Threading;
using LottoGenWeb.Services;

namespace LottoGenWeb.Tests
{
    public class IndexTests
    {
        private readonly Mock<IHttpClientWrapper> _httpClientWrapperMock;
        private readonly LottoGenWeb.Pages.Index _indexPage;

        public IndexTests()
        {
            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _indexPage = new LottoGenWeb.Pages.Index
            {
                ClientWrapper = _httpClientWrapperMock.Object
            };
        }

        [Fact]
        public void ClearResults_ShouldResetNumberSetsAndCount()
        {
            _indexPage.NumberSetCount = 5;
            _indexPage.Numbersets = new int[5][];

            _indexPage.ClearResults();

            Assert.Equal(0, _indexPage.NumberSetCount);
            Assert.Single(_indexPage.Numbersets); // Should be reset to [[0]]
            Assert.Equal(0, _indexPage.Numbersets[0][0]);
        }

        [Fact]
        public void ToggleSpinner_ShouldUpdateHideSpinner()
        {
            _indexPage.HideSpinner = true;

            _indexPage.ToggleSpinner(true);

            Assert.False(_indexPage.HideSpinner);

            _indexPage.ToggleSpinner(false);

            Assert.True(_indexPage.HideSpinner);
        }

        [Fact]
        public void HandleGroupChange_ShouldUpdateNumberGroup()
        {
            var updatedGroup = new NumberGroup(1, false, 5, 50, 2, 10, false, false);

            _indexPage.HandleGroupChange(updatedGroup);

            var group = _indexPage.NGs[0];
            Assert.False(group.Enabled);
            Assert.Equal(5, group.MinValue);
            Assert.Equal(50, group.MaxValue);
            Assert.Equal(2, group.NumbersPerGroup);
            Assert.Equal(10, group.Divergence);
        }
       
        [Fact]
        public async Task ProcessForm_ShouldCallGetNumbersetsAndToggleSpinner()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new int[][] { [1, 2, 3] })
            };
            _httpClientWrapperMock.Setup(wrapper => wrapper.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(mockResponse);

            // Act
            await _indexPage.ProcessForm();

            // Assert that Numbersets is populated correctly
            Assert.NotEmpty(_indexPage.Numbersets);
            Assert.Equal(1, _indexPage.Numbersets[0][0]);

            // Debugging statements
            Console.WriteLine("HideSpinner before final assertion: " + _indexPage.HideSpinner);

            // Assert that spinner is hidden
            Assert.True(_indexPage.HideSpinner, "The spinner should be hidden after ProcessForm completes.");
        }


    }
}