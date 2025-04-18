﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IPollRepository
    {
        void CreatePoll(Poll poll);
        IQueryable<Poll> GetPolls();
        Poll GetPollById(int id);
        void Vote(int pollId, int option,string userId);
    }
}
