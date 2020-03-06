using Moq;
using Moq.Protected;
using NUnit.Framework;
using StartupHouse.API.Interfaces.Exceptions;
using StartupHouse.API.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StartupHouse.API.Services.Tests
{
    public class Tests
    {
        private const string _mockedJson = "{'table':'A','currency':'dolar amerykañski','code':'USD','rates':[{'no':'001/A/NBP/2020','effectiveDate':'2020-01-02','mid':3.8000}]}";

        [Test]
        public async Task GetCurrency_WhenCalledAndReturns200_ShouldReturnExpectedValue()
        {
            //Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(_mockedJson)
                });

            var nbpService = new NbpService(new HttpClient(mockHttpMessageHandler.Object));

            var expectedResult = new NbpResponse
            {
                Code = "USD",
                Currency = "dolar amerykañski",
                Rates = new List<NbpCurrencyRate>()
                {
                    new NbpCurrencyRate
                    {
                        EffectiveDate = new DateTime(2020,1,2),
                        Mid = 3.8000M
                    }
                }
            };

            //Act
            var result = await nbpService.GetCurrency("USD", new DateTime(2020, 1, 2));

            //Assert
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public async Task GetCurrency_WhenCalledAndReturns404_ShouldReturnNull()
        {
            //Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var nbpService = new NbpService(new HttpClient(mockHttpMessageHandler.Object));

            //Act
            var result = await nbpService.GetCurrency("USD", new DateTime(2020, 1, 1));

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetCurrency_WhenCalledAndReturnsOtherThan200And404_ShouldThrowException()
        {
            //Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            var nbpService = new NbpService(new HttpClient(mockHttpMessageHandler.Object));

            //Act
            //Assert
            Assert.ThrowsAsync<NbpApiException>(() => nbpService.GetCurrency("USD", new DateTime(2020, 1, 1)));
        }
    }
}