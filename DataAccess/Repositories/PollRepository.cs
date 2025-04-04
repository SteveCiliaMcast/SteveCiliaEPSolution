using DataAccess.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class PollRepository : IPollRepository
    {
        private PollDbContext context;

        public PollRepository(PollDbContext _context)
        {
            context = _context;
        }

        public void CreatePoll(Poll poll)
        {
            context.Polls.Add(poll);
            context.SaveChanges();
        }

        public Poll GetPollById(int id)
        {
            return context.Polls.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Poll> GetPolls()
        {
            return context.Polls;
        }

        public void Vote(int pollId, int option, string userId)
        {
            var poll = context.Polls.Find(pollId);
            if (poll != null)
            {
                if (poll.VotedUsers.Contains(userId))
                {
                    // User has already voted
                    return;
                }

                switch (option)
                {
                    case 1:
                        poll.Option1VotesCount++;
                        break;
                    case 2:
                        poll.Option2VotesCount++;
                        break;
                    case 3:
                        poll.Option3VotesCount++;
                        break;
                }

                poll.VotedUsers.Add(userId);
                context.SaveChanges();
            }
        }
    }
}
