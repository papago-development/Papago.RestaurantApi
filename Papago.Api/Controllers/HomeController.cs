using Microsoft.AspNetCore.Mvc;
using Papago.Core.Logging;

namespace Papago.Api.Controllers
{
    [Route( "api/[controller]" )]
    public class HomeController : Controller
    {
        private readonly ILoggingService _loggingService;

        public HomeController( ILoggingService loggingService )
        {
            _loggingService = loggingService;
        }

        // GET api/home
        [HttpGet]
        public string Test()
        {
            const string text = @"API is up and running...";
            _loggingService.Warn( text );
            return text;
        }
    }
}