using UnityEngine;
using Object = UnityEngine.Object;

namespace Manager.States
{
    public class MainGameState : GameStateBase
    {
        private GameObject _gameRuleTempObj;
        public MainGameRule MainGameRule;
        public MainGameState(GameManager manager) : base(manager)
        {
            StateType = GameStateType.MainGame;
        }

        public override void OnStateEnter()
        {
            Debug.Log("MainGameState OnStateEnter");
            StartMainGame();
            Manager.Player.PlayerStates.OnHungryChange += PlayerStatesOnHungryChange;
            Manager.Player.PlayerStates.OnRoundChange += OnRoundChange;
        }

        public override void OnStateExit()
        {
            Manager.Player.PlayerStates.OnHungryChange -= PlayerStatesOnHungryChange;
            Manager.Player.PlayerStates.OnRoundChange -= OnRoundChange;
            DestroyGameRule();
        }

        public override void OnStateUpdate()
        {
        }

        private void StartMainGame()
        {
            CreateOrFindGameRule();
            OnStateStart?.Invoke(this);
            MainGameRule.SwitchRoundState(MainGameRoundState.EndTurn);
        }
        
        private void CreateOrFindGameRule()
        {
            MainGameRule = Object.FindObjectOfType<MainGameRule>(true);
            MainGameRule.enabled = true;
            if (MainGameRule) return;
            _gameRuleTempObj = new GameObject("GameRule");
            MainGameRule = _gameRuleTempObj.AddComponent<MainGameRule>();
        }
        
        private void DestroyGameRule()
        {
            MainGameRule.enabled = false;
            if (_gameRuleTempObj)
            {
                Object.Destroy(_gameRuleTempObj);
            }
            
        }
        
        
        private void PlayerStatesOnHungryChange(float current, float max)
        {
            bool isDead = MainGameRule.IsStarvedOrFull(current, max);
            if (isDead)
            {
                Manager.SwitchState(GameStateType.Fail);
            }
            
        }
        
        private void OnRoundChange(int round)
        {
            bool isReachEnd = MainGameRule.IsReachEnd(round);
            if(!isReachEnd) return;
            GameStateType endState = MainGameRule.DecideWhichEnd(Manager.Player.PlayerStates.CurrentConquerRate);
            Manager.SwitchState(endState);
        }
    }
}