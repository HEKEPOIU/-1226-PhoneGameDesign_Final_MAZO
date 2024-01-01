using System;
using Manager.States;
using Singleton;
using UnityEngine;

namespace Manager
{
    public enum GameStateType
    {
        Start,
        MainGame,
        Success,
        Fail,
        TrueSuccess
    }
    public class GameManager : PersistentSingleton<GameManager>
    {
        public Player.Player Player { get; private set; }
        
        [SerializeField] private GameStateType _startState = GameStateType.Start;
        private GameStateBase[] _states = new GameStateBase[Enum.GetValues(typeof(GameStateType)).Length];
        public GameStateBase CurrentState
        {
            get => _currentState;
            private set => _currentState = value;
        }
        private GameStateBase _currentState;
        

        protected override void Awake()
        {
            base.Awake();
            _states[(int)GameStateType.Start] = new StartGameState(this);
            _states[(int)GameStateType.MainGame] = new MainGameState(this);
            _states[(int)GameStateType.Success] = new SuccessGameState(this);
            _states[(int)GameStateType.Fail] = new FailGameState(this);
            _states[(int)GameStateType.TrueSuccess] = new TrueSuccessGameState(this);
            Player = FindObjectOfType<Player.Player>(true);
            if (Player == null)
            {
                Debug.LogError("Player not found");
                throw new Exception("Player not found");
            }
            
        }

        private void Start()
        {
            SwitchState(_startState);
        }

        private void Update()
        {
            CurrentState.OnStateUpdate();
        }
        
        public void SwitchState(GameStateType stateType)
        {
            if(_currentState != null)
            {
                if (_currentState.StateType == stateType)
                {
                    return;
                }
                _currentState.OnStateExit();
            }

            

            _currentState = _states[(int)stateType];
            _currentState.OnStateEnter();
        }
        
        
    }
}