using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Dto.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Room
{
    public interface ITeamHandler
    {
        public Task<HttpResult> Create(PostTeamDto postTeamDto);
        public Task<HttpResult> Leave();
    }
}
