using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Dto.Zone;
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
        public Task<HttpResult> Update(PutTeamDto putTeamDto, int teamId);
        public Task<HttpResult> Leave();

        public Task<HttpResult> Delete(int teamId);

        public Task<HttpResult> CreateSpawn(PostZoneDto postZoneDto, int teamId);
    }
}
