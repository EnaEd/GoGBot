using GoGBot.BLL.Models;
using GoGBot.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace GoGBot.BLL.Services
{
    public class ElectorService : IElectorService
    {
        public ElectorService()
        {
            ElectorsTeam = new();
            //IsCanVote = false;
        }
        public List<Elector> ElectorsTeam { get; set; }
        public bool IsCanVote { get; set; }

        public void GetElectors()
        {
            throw new NotImplementedException();
        }
    }
}
