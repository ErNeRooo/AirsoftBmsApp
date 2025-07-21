using AirsoftBmsApp.Model.Dto.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Room
{
    public interface IRoomHandler
    {
        public Task<HttpResult> Create(PostRoomDto postRoomDto);
        public Task<HttpResult> Join(JoinRoomDto joinRoomDto);
        public Task<HttpResult> Update(PutRoomDto putRoomDto);
        public Task<HttpResult> Leave();
    }
}
