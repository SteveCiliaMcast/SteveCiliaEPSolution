using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace SteveCiliaEPSolution.Controllers
{
    public class PollController : Controller
    {      

        [HttpGet]
        public IActionResult Index([FromServices] PollRepository pollRepository)
        {
            var polls = pollRepository.GetPolls().OrderByDescending(p => p.CreatedAt);
            return View(polls);
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

        [HttpGet]
        public IActionResult PollDetails(int id, [FromServices] PollRepository pollRepository)
        {
            var poll = pollRepository.GetPolls().FirstOrDefault(p => p.Id == id);
            if (poll == null)
            {
                return NotFound();
            }
            return View(poll);
        }

        [HttpPost]
        public IActionResult Vote(int pollId, int option, [FromServices] PollRepository pollRepository)
        {
            pollRepository.Vote(pollId, option);
            return RedirectToAction("PollDetails", new { id = pollId });
        }


    }
}
