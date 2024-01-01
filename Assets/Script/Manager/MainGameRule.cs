using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public enum MainGameRoundState
    {
        EndTurn,
        EnemyTurn,
        End,
        DrawCard
    }
    
    public class MainGameRule : MonoBehaviour
    {
        [Range(0f, 1f)] 
        public float HigherHungryRate = 0.8f;
        [Range(0f, 1f)] 
        public float LowerHungryRate = .3f;
        [Range(0f,1f)]
        public float StartHungryRate = 0.7f;
        
        [Range(0f, 1f)] public float HungryDeltaRate = 0.05f;

        [SerializeField] private float _delayBetweenRound;

        public int EndRound = 50;
        
        private bool _isWaitingForRoundChange = false;
        

        
        public MainGameRoundState CurrentRoundState = MainGameRoundState.EndTurn;
        private MainGameRoundState _lastRoundState = MainGameRoundState.EndTurn;
        private Timer _changeStateTimer;
        public static event Action<MainGameRoundState, MainGameRoundState> OnRoundStateChange;

        private void Awake()
        {
            _changeStateTimer = gameObject.AddComponent<Timer>();
            _changeStateTimer.TotalTime = _delayBetweenRound;
            _changeStateTimer.OnTimerEnd += InvokeChangeRoundEvent;
        }

        public void SwitchRoundState(MainGameRoundState roundState)
        {
            if (_isWaitingForRoundChange) return;
            _isWaitingForRoundChange = true;
            _lastRoundState = CurrentRoundState;
            CurrentRoundState = roundState;
            _changeStateTimer.StartTimer();
            
        }
        
        public void SwitchToNextRound(MainGameRoundState roundState)
        {
            switch (roundState)
            {
                case MainGameRoundState.EndTurn:
                    SwitchRoundState(MainGameRoundState.EnemyTurn);
                    break;
                case MainGameRoundState.DrawCard:
                    break;
                case MainGameRoundState.EnemyTurn:
                    SwitchRoundState(MainGameRoundState.End);
                    break;
                case MainGameRoundState.End:
                    SwitchRoundState(MainGameRoundState.EndTurn);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void InvokeChangeRoundEvent()
        {
            _isWaitingForRoundChange = false;
            OnRoundStateChange?.Invoke(CurrentRoundState, _lastRoundState);
        }

        private void OnDestroy()
        {
            _changeStateTimer.OnTimerEnd -= InvokeChangeRoundEvent;
        }
        
        public bool IsStarvedOrFull(float currentHungry, float maxHungry)
        {
            if (currentHungry >= maxHungry * HigherHungryRate)
            {
                return true;
            }
            else if (currentHungry <= maxHungry * LowerHungryRate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool IsReachEnd(int currentRound)
        {
            return currentRound == EndRound;
        }
        
        public GameStateType DecideWhichEnd(float currentConquerRate)
        {
            return Mathf.Approximately(currentConquerRate, 1f) ?
                GameStateType.TrueSuccess : GameStateType.Success;
        }
    }
}