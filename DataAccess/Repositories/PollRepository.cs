using DataAccess.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PollRepository
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

        public IQueryable<Poll> GetPolls()
        {
            return context.Polls;
        }
        public void Vote(int pollId, int option)
        {
            var poll = context.Polls.Find(pollId);
            if (poll != null)
            {
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
                context.SaveChanges();
            }
        }
    }
}
