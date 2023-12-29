using System;
using Manager.States;
using Singleton;
using UnityEngine;

namespace Manager
{
    enum GameStateType
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
            set
            {
                if (_currentState != null)
                {
                    _currentState.OnStateExit();
                }

                _currentState = value;
                _currentState.OnStateEnter();
            }
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
            
            CurrentState = _states[(int)_startState];
            Player = FindObjectOfType<Player.Player>();
            if (Player == null)
            {
                Debug.LogError("Player not found");
                throw new Exception("Player not found");
            }
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            CurrentState.OnStateUpdate();
        }
    }
}