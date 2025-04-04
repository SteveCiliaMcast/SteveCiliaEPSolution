using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        [ServiceFilter(typeof(EnsureUserHasNotVotedAttribute))]
        public IActionResult Vote([FromServices] IPollRepository pollRepository, int pollId, int option)
        {
            if (!ModelState.IsValid)
            {
                var poll = pollRepository.GetPollById(pollId);
                return View("PollDetails", poll);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            pollRepository.Vote(pollId, option, userId);

            return RedirectToAction("PollDetails", new { id = pollId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Vote(int pollId)
        {
            return RedirectToAction("PollDetails", new { id = pollId });
        }


    }
}
