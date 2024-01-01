using System;
using Manager;
using UIManagement;
using UIManagement.Element;
using UnityEngine;

namespace Player
{
    public class PlayerHUD : MonoBehaviour
    {
        private UIManager _uiManager;
        [HideInInspector] public Player Player;

        private void Awake()
        {
            Initialize(Player);
        }
        
        public void Initialize(Player player)
        {
            if (!_uiManager) _uiManager = FindObjectOfType<UIManager>(true);
            Player = player;
            _uiManager.OwnerPlayer = Player;
        }
        
        public void RestUI(GameStateType gameStateType)
        {
            switch (gameStateType)
            {
                case GameStateType.Start:
                    _uiManager.Show<StartUIElement>(false);
                    break;
                case GameStateType.MainGame:
                    _uiManager.Show<MainGameUIElement>(false);
                    break;
                case GameStateType.Success:
                case GameStateType.Fail:
                case GameStateType.TrueSuccess:
                    _uiManager.Show<EndUIElement>(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameStateType), gameStateType, null);
            }
            
        }
        
        
    }
}