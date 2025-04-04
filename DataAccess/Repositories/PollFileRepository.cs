using Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DataAccess.Repositories
{
    public class PollFileRepository : IPollRepository
    {
        private readonly string filePath;

        public PollFileRepository(string _filePath)
        {
            filePath = _filePath;
        }

        public void CreatePoll(Poll poll)
        {
            var polls = GetPolls().ToList();
            poll.Id = polls.Count > 0 ? polls.Max(p => p.Id) + 1 : 1;
            polls.Add(poll);
            SavePolls(polls);
        }

        public IQueryable<Poll> GetPolls() { 

            if (!File.Exists(filePath))

            return new List<Poll>().AsQueryable();

                    var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Poll>>(jsonData)?. AsQueryable() ?? new List<Poll>().AsQueryable();
        }

        public Poll? GetPollById(int id)
        {
            return GetPolls().FirstOrDefault(p => p.Id == id);
        }

        public void Vote(int pollId, int option)
        {
            var polls = GetPolls().ToList();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);
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
                SavePolls(polls);
            }
        }

        private void SavePolls(List<Poll> polls)
        {
            var jsonData = JsonConvert.SerializeObject(polls, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
    }
}