using Easy.Tools.GoogleCalendarEvents.Models;
using Easy.Tools.GoogleCalendarEvents.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using Xunit;

namespace Easy.Tools.GoogleCalendarEvents.Tests
{
    public class GoogleEventServiceTests
    {
        [Fact]
        public async Task GetEventsAsync_ReturnsParsedEvents()
        {
            // Arrange
            var json = """
            {
                "items": [
                    {
                        "status": "confirmed",
                        "summary": "Sample Event",
                        "description": "Desc",
                        "start": { "date": "2025-01-01" },
                        "end": { "date": "2025-01-02" }
                    }
                ]
            }
            """;

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new GoogleEventService("fakeKey", "fakeCalendarId", httpClient);

            // Act
            var result = await service.GetEventsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Sample Event", result[0].Summary);
        }
    }
}
