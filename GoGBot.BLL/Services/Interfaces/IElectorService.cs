using GoGBot.BLL.Models;
using System.Collections.Generic;

namespace GoGBot.BLL.Services.Interfaces
{
    public interface IElectorService
    {
        public List<Elector> ElectorsTeam { get; set; }
        public void GetElectors();
    }
}
