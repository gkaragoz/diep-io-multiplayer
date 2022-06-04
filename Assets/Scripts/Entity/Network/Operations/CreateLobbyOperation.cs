using Constants;
using Entity.Logger;
using Enums;
using Events;
using Mirror;
using Steamworks;

namespace Entity.Network.Operations
{
    public class CreateLobbyOperation
    {
        private Callback<LobbyCreated_t> _lobbyCreated;

        private bool IsPrivate
        {
            get
            {
                switch (_lobbyType)
                {
                    case ELobbyType.k_ELobbyTypePrivate:
                        return true;
                    case ELobbyType.k_ELobbyTypeFriendsOnly:
                        return true;
                    case ELobbyType.k_ELobbyTypePublic:
                        return false;
                    case ELobbyType.k_ELobbyTypeInvisible:
                        return false;
                    case ELobbyType.k_ELobbyTypePrivateUnique:
                        return true;
                    default:
                        return false;
                }
            }
        }

        private ELobbyType _lobbyType;

        public CreateLobbyOperation()
        {
            if (!SteamManager.Initialized)
                return;

            _lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        }

        public void CreateLobbyListener(ELobbyType lobbyType)
        {
            this.Log("Trying to create lobby.");

            _lobbyType = lobbyType;

            SteamMatchmaking.CreateLobby(lobbyType, NetworkManager.singleton.maxConnections);
        }

        private void OnLobbyCreated(LobbyCreated_t callback)
        {
            this.LogWarning($"OnLobbyCreated {callback.m_eResult}");

            if (callback.m_eResult != EResult.k_EResultOK)
                return;

            NetworkEvents.StartHost?.Invoke();
            
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.HOST_ADDRESS_KEY, SteamUser.GetSteamID().ToString());
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.LOBBY_NAME_KEY, $"{SteamFriends.GetPersonaName()} 's LOBBY");
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.LOBBY_OWNER_NAME_KEY, $"{SteamFriends.GetPersonaName()}");
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.LOBBY_TYPE_KEY, $"{IsPrivate}");
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), NetworkConstants.LOBBY_STATUS_KEY, $"{LobbyStatus.AVAILABLE.ToString()}");
        }
    }
}