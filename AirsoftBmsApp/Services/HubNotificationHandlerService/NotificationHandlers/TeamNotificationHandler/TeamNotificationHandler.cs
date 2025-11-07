using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Model.Observable;

namespace AirsoftBmsApp.Services.HubNotificationHandlerService.NotificationHandlers.TeamNotificationHandler;

public class TeamNotificationHandler : ITeamNotificationHandler
{
    public void OnTeamCreated(TeamDto teamDto, ObservableRoom contextRoom)
    {
        bool doesTeamExist = contextRoom.Teams.Any(t => t.Id == teamDto.TeamId);

        if (doesTeamExist) return;

        contextRoom.Teams.Add(new ObservableTeam(teamDto));
    }

    public void OnTeamDeleted(int teamId, ObservableRoom contextRoom)
    {
        ObservableTeam? team = contextRoom.Teams.FirstOrDefault(t => t.Id == teamId);

        if (team is null) return;

        contextRoom.Teams.Remove(team);
    }

    public void OnTeamUpdated(TeamDto teamDto, ObservableRoom contextRoom)
    {
        ObservableTeam? previousTeam = contextRoom.Teams.FirstOrDefault(t => t.Id == teamDto.TeamId);

        if (previousTeam is null) return;

        previousTeam.Name = teamDto.Name;
        
        if (previousTeam.OfficerId != teamDto.OfficerPlayerId)
        {
            ObservablePlayer? oldOfficerPlayer = contextRoom.Teams.SelectMany(t => t.Players).FirstOrDefault(p => p.Id == previousTeam.OfficerId);
            ObservablePlayer? newOfficerPlayer = contextRoom.Teams.SelectMany(t => t.Players).FirstOrDefault(p => p.Id == teamDto.OfficerPlayerId);

            previousTeam.OfficerId = teamDto.OfficerPlayerId;

            if (oldOfficerPlayer is not null) oldOfficerPlayer.IsOfficer = false;
            if (newOfficerPlayer is not null) newOfficerPlayer.IsOfficer = true;
        }
    }
}
