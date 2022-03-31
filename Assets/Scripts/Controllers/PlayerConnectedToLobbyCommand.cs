using System.Collections.Generic;
using Assets.Scripts.Entity.Controllers;
using Assets.Scripts.Entity.Logger;
using Assets.Scripts.Entity.Player;
using Assets.Scripts.Entity.UI.Lobby;
using Assets.Scripts.Enums;
using Assets.Scripts.Events;
using Assets.Scripts.Screens;
using Mirror;
using Steamworks;

namespace Assets.Scripts.Controllers
{
    public class PlayerConnectedToLobbyCommand : Command
    {
        public PlayerConnectedToLobbyCommand()
        {
            NetworkEvents.OnServerAddPlayer += OnServerAddPlayer;
        }

        private void OnServerAddPlayer(NetworkConnection connection)
        {
            this.LogWarning("OnServerAddPlayer");
            
            return;
            
            var lobbyScreen = (LobbyScreen) UIEvents.ShowScreen?.Invoke(ScreenType.Lobby);
            var lobbyPlayerList = new List<LobbyPlayer>();

            var lobbySteamId = new CSteamID(PlayerDataController.Instance.GetLobbySteamId()); 
            var membersCount = SteamMatchmaking.GetNumLobbyMembers(lobbySteamId);
            for (int ii = 0; ii < membersCount; ii++)
            {
                var memberSteamId = SteamMatchmaking.GetLobbyMemberByIndex(lobbySteamId, ii);

                var name = string.Empty;
                if (memberSteamId == SteamUser.GetSteamID())
                    name = SteamFriends.GetPersonaName();
                else
                    name = SteamFriends.GetFriendPersonaName(memberSteamId);
                
                lobbyPlayerList.Add(new LobbyPlayer()
                {
                    SteamId = memberSteamId.m_SteamID,
                    Name = name,
                });
            }
            
            lobbyScreen.Initialize(NetworkClient.isHostClient, lobbyPlayerList);
        }
    }
}