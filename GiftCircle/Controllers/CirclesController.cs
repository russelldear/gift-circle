using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace GiftCircle.Controllers
{
    public class CirclesController : Controller
    {
        private readonly ILogger<CirclesController> _logger;

        public CirclesController(ILogger<CirclesController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
