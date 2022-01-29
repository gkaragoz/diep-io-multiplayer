using System.Collections.Generic;
using Entity.UI.Room;
using Enums;
using Events;
using UnityEngine;
using Screen = Entity.UI.Screen.Screen;

namespace Screens
{
    public class RoomListScreen : Screen
    {
        public override ScreenType ScreenType => ScreenType.RoomList;
        
        #region Serialized References

        [SerializeField] private RoomListItemUI _roomListItemPrefab;
        [SerializeField] private RectTransform _contentParent;
        
        #endregion

        private List<RoomListItemUI> _roomList;

        public void Initialize(List<RoomListItem> roomListItems)
        {
            foreach (var roomListItem in roomListItems)
                CreateRoomListItem(roomListItem);
        }

        private void CreateRoomListItem(RoomListItem roomListItem)
        {
            var roomListItemUI = Instantiate(_roomListItemPrefab, _contentParent);
            roomListItemUI.Initialize(roomListItem);
            
            _roomList.Add(roomListItemUI);
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
        }

        #region Editor Callbacks

        public void OnClick_BackToMainMenu()
        {
            UIEvents.HideScreen?.Invoke(ScreenType);
            UIEvents.ShowScreen?.Invoke(ScreenType.MainMenu);
        }
        
        #endregion
        
    }
}