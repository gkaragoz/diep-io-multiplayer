using System.Collections.Generic;
using Entity.Controllers;
using Entity.Logger;
using Entity.Player;
using Entity.UI.Lobby;
using Enums;
using Events;
using Mirror;
using Screens;
using Steamworks;

namespace Controllers
{
    public class PlayerConnectedToLobbyCommand : Command
    {
        public PlayerConnectedToLobbyCommand()
        {
            NetworkEvents.OnServer_ClientConnectedToServer += OnServer_ClientConnectedToServerListener;
            NetworkEvents.OnServer_ClientDisconnectedFromServer += OnServer_ClientDisconnectedFromServerListener;

            NetworkEvents.OnClient_ClientConnectedToServer += OnClient_ClientConnectedToServerListener;
            NetworkEvents.OnClient_ClientDisconnectedFromServer += OnClient_ClientDisconnectedFromServerListener;
        }

        private void OnServer_ClientConnectedToServerListener(NetworkConnection connection)
        {
            this.LogWarning("OnServer_ClientConnectedToServerListener");
            
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

        private void OnServer_ClientDisconnectedFromServerListener(NetworkConnection connection)
        {
            this.LogWarning("OnServer_ClientDisconnectedFromServerListener");
            
        }

        private void OnClient_ClientConnectedToServerListener(NetworkConnection connection)
        {
            this.LogWarning("OnClient_ClientConnectedToServerListener");

        }

        private void OnClient_ClientDisconnectedFromServerListener(NetworkConnection connection)
        {
            this.LogWarning("OnClient_ClientDisconnectedFromServerListener");

        }
    }
}