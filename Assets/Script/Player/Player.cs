using System;
using Character;
using Manager;
using Manager.States;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private BaseGridCharacter _playerCharacter;
        private PlayerController _playerController;
        [HideInInspector] public PlayerStates PlayerStates;
        private PlayerHUD _playerHUD;
        private void Awake()
        {
            //InitPlayerSelf;
            _playerCharacter = GetComponent<PlayerCharacter>();
            _playerController = GetComponent<PlayerController>();
            _playerController.PlayerCharacter = _playerCharacter;
            
            PlayerStates = FindOrCreate<PlayerStates>();
            _playerHUD = FindOrCreate<PlayerHUD>();
            _playerHUD.Initialize(this);
            GameStateBase.OnStateStart += SwitchGameState;
        }
        
        private void SwitchGameState(GameStateBase gameState)
        {
            print("SwitchGameState");
            _playerHUD.RestUI(gameState.StateType);
            switch (gameState.StateType)
            {
                case GameStateType.MainGame:
                    PlayerStates.ResetState(gameState as MainGameState);
                    break;
            }
        }

        private void OnDestroy()
        {
            GameStateBase.OnStateStart -= SwitchGameState;

        }

        public T FindOrCreate<T>()  where T : MonoBehaviour
        {
            T t = FindObjectOfType<T>(true);
            if (t == null)
            {
                GameObject tempObj = new GameObject(typeof(T).Name);
                t = gameObject.AddComponent<T>();
            }
            return t;
        }
    
    }
}