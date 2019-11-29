using System;
using System.Threading.Tasks;
using GiftCircle.Models;
using GiftCircle.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace GiftCircle.Controllers
{
    [Authorize]
    public class CirclesController : Controller
    {
        private readonly CirclesRepository _circlesRepository;
        private readonly ILogger<CirclesController> _logger;

        public CirclesController(CirclesRepository circlesRepository, ILogger<CirclesController> logger)
        {
            _circlesRepository = circlesRepository;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();

            var circles = await _circlesRepository.GetCircles(userId);

            var viewModel = new CirclesViewModel {Circles = circles};

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var userId = User.GetUserId();

            var newCircle = new Circle
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name
            };

            await _circlesRepository.CreateCircle(newCircle);

            return RedirectToAction("Index");
        }
    }
}
