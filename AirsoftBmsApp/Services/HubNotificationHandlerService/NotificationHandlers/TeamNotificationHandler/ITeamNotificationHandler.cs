using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.TeamNotificationHandler;

public interface ITeamNotificationHandler
{
    void OnTeamCreated(TeamDto teamDto, ObservableRoom contextRoom);
    void OnTeamUpdated(TeamDto teamDto, ObservableRoom contextRoom);
    void OnTeamDeleted(int teamId, ObservableRoom contextRoom);
}
