using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Networking.Handlers.Player
{
    public class TeamPostHandler(ITeamRestService teamRestService, IRoomDataService roomDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            dynamic dynamicRequest = request;

            PostTeamDto postTeamDto = new PostTeamDto
            {
                Name = dynamicRequest.Name,
            };

            var result = await teamRestService.TryRequest(new PostTeam(postTeamDto));

            switch (result)
            {
                case Success<int> success:
                    roomDataService.Room.Teams.Add(new ObservableTeam
                    {
                        Id = success.data,
                        Name = postTeamDto.Name,
                        RoomId = roomDataService.Room.Id,
                        Players = new ObservableCollection<ObservablePlayer>()
                    });

                    var nextResult = await base.Handle(request);

                    return nextResult is null ? result : nextResult;
                case Failure failure:
                    return new Failure(failure.errorMessage);
                case Error error:
                    return new Error(error.errorMessage);
                default:
                    throw new InvalidOperationException("Unknown result type");
            }
        }
    }
}
