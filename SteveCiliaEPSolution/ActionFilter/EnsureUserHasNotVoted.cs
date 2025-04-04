using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

public class EnsureUserHasNotVotedAttribute : ActionFilterAttribute
{
    private readonly IPollRepository _pollRepository;

    public EnsureUserHasNotVotedAttribute(IPollRepository pollRepository)
    {
        _pollRepository = pollRepository;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var pollId = (int)context.ActionArguments["pollId"];
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var poll = _pollRepository.GetPollById(pollId);
        if (poll != null && poll.VotedUsers.Contains(userId))
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                controller.ModelState.AddModelError(string.Empty, "You have already voted in this poll.");
                context.Result = controller.View("PollDetails", poll);
            }
        }

        base.OnActionExecuting(context);
    }
}
