using Microsoft.AspNetCore.Http;
using Moq;

namespace CinemaProject.Tests.Helpers
{
   internal static class ReturnValuesHelper
   {
      internal static HttpContext GetMockHttpContext()
      {
         Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
         Mock<HttpRequest> request = new Mock<HttpRequest>();
         Mock<HttpResponse> response = new Mock<HttpResponse>();

         mockHttpContext.Setup(s => s.Request).Returns(request.Object);
         mockHttpContext.Setup(s => s.Response).Returns(response.Object);
         mockHttpContext.Setup(x => x.Response.Cookies).Returns(new HttpCookieCollection());
         return mockHttpContext.Object;
      }
   }
}
