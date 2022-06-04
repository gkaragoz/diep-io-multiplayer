using System.Collections.Generic;
using Entity.Logger;
using Entity.Player;
using Entity.UI.Lobby;
using Enums;
using Events;
using Mirror;
using Screens;
using Steamworks;

namespace Entity.Network.Operations
{
    public class PlayerConnectedToLobbyOperation
    {
        public PlayerConnectedToLobbyOperation()
        {
            NetworkEvents.OnServerAddPlayer += OnServerAddPlayer;
        }

        ~PlayerConnectedToLobbyOperation()
        {
            NetworkEvents.OnServerAddPlayer -= OnServerAddPlayer;
        }

        private void OnServerAddPlayer(NetworkConnection connection)
        {
            this.LogWarning("OnServerAddPlayer");
            
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