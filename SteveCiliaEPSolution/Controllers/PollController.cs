using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace SteveCiliaEPSolution.Controllers
{
    public class PollController : Controller
    {
        [HttpGet]
        public IActionResult Index([FromServices] IPollRepository pollRepository)
        {
            var polls = pollRepository.GetPolls().OrderByDescending(p => p.CreatedAt);
            return View(polls);
        }

        [HttpGet]
        public IActionResult CreatePoll()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePoll([FromServices] IPollRepository pollRepository, Poll poll)
        {
            if (ModelState.IsValid)
            {
                poll.CreatedAt = DateTime.Now;
                pollRepository.CreatePoll(poll);
                return RedirectToAction("Index");
            }
            return View(poll);
        }

        [HttpGet]
        public IActionResult PollDetails([FromServices] IPollRepository pollRepository, int id)
        {
            var poll = pollRepository.GetPollById(id);
            if (poll == null)
            {
                return NotFound();
            }
            return View(poll);
        }

        [HttpPost]
        public IActionResult Vote([FromServices] IPollRepository pollRepository, int pollId, int option)
        {
            pollRepository.Vote(pollId, option);
            return RedirectToAction("PollDetails", new { id = pollId });
        }
    }
}
