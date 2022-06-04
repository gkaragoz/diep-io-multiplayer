using System.Collections.Generic;
using Constants;
using Entity.Logger;
using Entity.Player;
using Entity.UI.Lobby;
using Enums;
using Events;
using Mirror;
using Screens;
using Steamworks;
using UnityEngine;

namespace Entity.Network.Operations
{
    public class PlayerConnectedToLobbyOperation
    {
        public void OnPlayerConnectedToLobby(PlayerObjectController playerObjectController)
        {
            this.LogWarning("OnPlayerConnectedToLobby");
            
            var lobbyScreen = (LobbyScreen) UIEvents.ShowScreen?.Invoke(ScreenType.Lobby);
            var lobbyPlayerList = new List<LobbyPlayerData>();

            var lobbySteamId = ulong.Parse(PlayerPrefs.GetString(NetworkConstants.LOBBY_STEAM_ID));
            var lobbyCSteamId = new CSteamID(lobbySteamId); 
            var membersCount = SteamMatchmaking.GetNumLobbyMembers(lobbyCSteamId);
            for (int ii = 0; ii < membersCount; ii++)
            {
                var memberSteamId = SteamMatchmaking.GetLobbyMemberByIndex(lobbyCSteamId, ii);

                var name = playerObjectController.isLocalPlayer ? "(you) " : string.Empty;
                if (memberSteamId == SteamUser.GetSteamID())
                    name += SteamFriends.GetPersonaName();
                else
                    name += SteamFriends.GetFriendPersonaName(memberSteamId);
                
                lobbyPlayerList.Add(new LobbyPlayerData()
                {
                    PlayerObjectController = playerObjectController,
                    Name = name
                });
            }
            
            lobbyScreen.Initialize(lobbySteamId, NetworkClient.isHostClient, lobbyPlayerList);
        }
    }
}