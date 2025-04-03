using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace SteveCiliaEPSolution.Controllers
{
    public class PollController : Controller
    {      

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreatePoll([FromServices] PollRepository pollRepository)
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePoll(Poll poll, [FromServices] PollRepository pollRepository)
        {
            if (ModelState.IsValid)
            {
                poll.CreatedAt = DateTime.Now;
                pollRepository.CreatePoll(poll);
                return RedirectToAction("Index");
            }
            return View(poll);
        }


    }
}
