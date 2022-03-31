using System;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Controllers;
using Assets.Scripts.Entity.Logger;
using Assets.Scripts.Entity.UI.Lobby;
using Assets.Scripts.Enums;
using Assets.Scripts.Events;
using Assets.Scripts.Screens;
using Steamworks;

namespace Assets.Scripts.Controllers
{
    public class ListLobbiesCommand : Command
    {
        protected CallResult<LobbyMatchList_t> RequestLobbyList;

        public ListLobbiesCommand()
        {
            NetworkEvents.ListLobbiesCommand += ListLobbiesListener;

            if (!SteamManager.Initialized)
                return;

            RequestLobbyList = CallResult<LobbyMatchList_t>.Create(OnLobbyListRefreshed);
        }

        private void OnLobbyListRefreshed(LobbyMatchList_t callback, bool bIOFailure)
        {
            this.LogWarning("OnLobbyListRefreshed");

            if (bIOFailure)
                return;

            var lobbyListItems = new List<LobbyListItem>();

            var lobbiesCount = callback.m_nLobbiesMatching;

            for (int ii = 0; ii < lobbiesCount; ii++)
            {
                var lobbySteamId = SteamMatchmaking.GetLobbyByIndex(ii);

                var lobbyListItem = new LobbyListItem()
                {
                    LobbySteamId = lobbySteamId.m_SteamID,
                    LobbyName = SteamMatchmaking.GetLobbyData(lobbySteamId, NetworkConstants.LOBBY_NAME_KEY),
                    OwnerName = SteamMatchmaking.GetLobbyData(lobbySteamId, NetworkConstants.LOBBY_OWNER_NAME_KEY),
                    CurrentPlayersCount = SteamMatchmaking.GetNumLobbyMembers(lobbySteamId),
                    MaxPlayersCount = SteamMatchmaking.GetLobbyMemberLimit(lobbySteamId),
                    IsPrivate = bool.Parse(SteamMatchmaking.GetLobbyData(lobbySteamId, NetworkConstants.LOBBY_TYPE_KEY)),
                    Status = (LobbyStatus)Enum.Parse(typeof(LobbyStatus), SteamMatchmaking.GetLobbyData(lobbySteamId, NetworkConstants.LOBBY_STATUS_KEY)),
                };

                lobbyListItems.Add(lobbyListItem);
            }

            var lobbiesScreen = UIEvents.ShowScreen?.Invoke(ScreenType.Lobbies);
            if (lobbiesScreen is LobbiesScreen screen)
                screen.Initialize(lobbyListItems);
        }

        private void ListLobbiesListener()
        {
            this.Log("RequestLobbyList");
            SteamMatchmaking.AddRequestLobbyListStringFilter(NetworkConstants.LOBBY_OWNER_NAME_KEY, "Whoaa", ELobbyComparison.k_ELobbyComparisonEqualToOrGreaterThan);
            RequestLobbyList.Set(SteamMatchmaking.RequestLobbyList());
        }
    }
}