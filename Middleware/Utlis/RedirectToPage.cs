using DeviceDetectorNET;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;

namespace Middleware.Utlis
{
    public class RedirectToPage
    {
        private readonly RequestDelegate _next;

        public RedirectToPage(RequestDelegate next)
        {
            _next = next;
        }

        public Task Redirection(HttpContext context)
        {
            string userAgent = context.Request.Headers["User-Agent"].ToString();
            var Parse_userAgent = new DeviceDetector(userAgent);
            Parse_userAgent.Parse();

            string browserName = Parse_userAgent.GetClient().Match.Name; //przypisanie nazwy użytej przeglądarki do zmiennej
            if (WhichBrowser(browserName))
            {
               
                    context.Response.Redirect("https://www.mozilla.org/pl/firefox/new/"); //przekierowanie na Firefoxa
                    return Task.CompletedTask;
               
            }

            return _next(context);
        }

        private bool WhichBrowser(string browserName) //funkcja sprawdzająca, czy weszliśmy na którąś z wymienionych przeglądarek
        {
            return browserName.Contains("Microsoft Edge") || browserName.Contains("EdgeChromium") || browserName.Contains("IE");
        }
    }
}