using System;
using UIManagement;
using UIManagement.Element;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manager.States
{
    public class MainGameState : GameStateBase
    {
        private GameObject _gameRuleTempObj;
        public GameRule GameRule;
        public MainGameState(GameManager manager) : base(manager)
        {
            StateType = GameStateType.MainGame;
        }

        public static event Action<MainGameState> OnMainGameStart;
        public override void OnStateEnter()
        {
            Debug.Log("MainGameState OnStateEnter");
            StartMainGame();
        }

        public override void OnStateExit()
        {
            DestroyGameRule();
        }

        public override void OnStateUpdate()
        {
        }

        private void StartMainGame()
        {
            CreateOrFindGameRule();
            OnStateStart?.Invoke(this);
            OnMainGameStart?.Invoke(this);
        }
        
        private void CreateOrFindGameRule()
        {
            GameRule = Object.FindObjectOfType<GameRule>(true);
            GameRule.enabled = true;
            if (GameRule) return;
            _gameRuleTempObj = new GameObject("GameRule");
            GameRule = _gameRuleTempObj.AddComponent<GameRule>();
        }
        
        private void DestroyGameRule()
        {
            GameRule.enabled = false;
            if (_gameRuleTempObj)
            {
                Object.Destroy(_gameRuleTempObj);
            }
            
        }
    }
}