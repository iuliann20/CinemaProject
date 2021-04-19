using Microsoft.AspNetCore.Http;

namespace CinemaProject.Tests.Helpers
{
   internal class HttpCookieCollection : IResponseCookies
   {
      public void Append(string key, string value)
      {
         return;
      }

      public void Append(string key, string value, CookieOptions options)
      {
         return;
      }

      public void Delete(string key)
      {
         return;
      }

      public void Delete(string key, CookieOptions options)
      {
         return;
      }
   }
}