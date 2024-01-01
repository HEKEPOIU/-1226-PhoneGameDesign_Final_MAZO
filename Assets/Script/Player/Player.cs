using Character;
using Character.Spawner;
using Manager;
using Manager.States;
using SkillSystem;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private BaseGridCharacter _playerCharacter;
        [HideInInspector] public PlayerController PlayerController;
        [HideInInspector] public PlayerStates PlayerStates;
        private PlayerHUD _playerHUD;
        private void Awake()
        {
            //InitPlayerSelf;
            _playerCharacter = GetComponent<PlayerCharacter>();
            PlayerController = GetComponent<PlayerController>();
            //TODO: 這裡不該這樣做，如果我要通用性的話我不該在這裡轉型。
            PlayerController.PlayerCharacter = (PlayerCharacter)_playerCharacter;
            
            
            PlayerStates = FindOrCreate<PlayerStates>();
            PlayerStates.GridSkillSystem = FindOrCreate<GridSkillSystem>();
            PlayerStates.GridSkillSystem.Owner = this;
            PlayerStates.GridSkillSystem.GridSpawnerManager = FindObjectOfType<GridSpawnerManager>(true);
            PlayerController.PlayerStates = PlayerStates;
            _playerHUD = FindOrCreate<PlayerHUD>();
            _playerHUD.Initialize(this);
            GameStateBase.OnStateStart += SwitchGameState;
        }
        
        private void SwitchGameState(GameStateBase gameState)
        {
            _playerHUD.RestUI(gameState.StateType);
            PlayerStates.SetStates(gameState.StateType);
            switch (gameState.StateType)
            {
                case GameStateType.MainGame:
                    PlayerStates.ResetState(gameState as MainGameState);
                    _playerCharacter.InitGridObject();
                    break;
                case GameStateType.Fail:
                case GameStateType.TrueSuccess:
                case GameStateType.Success:
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