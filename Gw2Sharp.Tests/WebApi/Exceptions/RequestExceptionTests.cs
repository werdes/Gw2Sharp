using System;
using Gw2Sharp.WebApi.Exceptions;
using Gw2Sharp.WebApi.Http;
using NSubstitute;
using Xunit;

namespace Gw2Sharp.Tests.WebApi.Exceptions
{
    public class RequestExceptionTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var request = Substitute.For<IWebApiRequest>();
            var response = Substitute.For<IWebApiResponse>();
            var innerException = new NotImplementedException();
            string message = "test exception";


            var exception1 = new RequestException(request, message);
            var exception2 = new RequestException(request, response, message);
            var exception3 = new RequestException(request, message, innerException);
            var exception4 = new RequestException(request, response, message, innerException);

            Assert.Equal(message, exception1.Message);
            Assert.Null(exception1.InnerException);
            Assert.Same(request, exception1.Request);
            Assert.Null(exception1.Response);

            Assert.Equal(message, exception2.Message);
            Assert.Null(exception2.InnerException);
            Assert.Same(request, exception2.Request);
            Assert.Same(response, exception2.Response);

            Assert.Equal(message, exception3.Message);
            Assert.Same(innerException, exception3.InnerException);
            Assert.Same(request, exception3.Request);
            Assert.Null(exception3.Response);

            Assert.Equal(message, exception4.Message);
            Assert.Same(innerException, exception4.InnerException);
            Assert.Same(request, exception4.Request);
            Assert.Same(response, exception4.Response);
        }
    }
}
