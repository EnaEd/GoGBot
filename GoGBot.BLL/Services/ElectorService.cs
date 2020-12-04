using GoGBot.BLL.Models;
using GoGBot.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace GoGBot.BLL.Services
{
    public class ElectorService : IElectorService
    {
        public List<Elector> ElectorsTeam { get; set; }

        public void GetElectors()
        {
            throw new NotImplementedException();
        }
    }
}
