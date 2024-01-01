using System;
using Character;
using Manager;
using Manager.States;
using SkillSystem;
using UnityEngine;

namespace Player
{
    public class PlayerStates : MonoBehaviour
    {
        [SerializeField] private float _maxHungry = 100f;
        public float MaxHungry => _maxHungry;
        private GameStateType _gameStateType;
        private float _currentHungry;
        public float CurrentHungry => _currentHungry;
        private MainGameState _mainGameState;
        private int _currentRound = 0;
        
        private float _currentConquerRate = 0f;
        private float _maxConquerRate = 1f;
        public float CurrentConquerRate => _currentConquerRate;
        [HideInInspector] public GridSkillSystem GridSkillSystem;
        public event Action<float,float> OnHungryChange; 
        public event Action<GameStateType> OnStateChange;
        public event Action<int> OnRoundChange;
        public event Action<float,float> OnConquerRateChange;
        
        
        public void ResetState(MainGameState mainGameState)
        {
            _mainGameState = mainGameState;
            SetStates(mainGameState.StateType);
            SetHungry(_mainGameState.MainGameRule.StartHungryRate * _maxHungry);
            SetRound(0);
            SetConquerRate(0);
        }
        
        public void SetStates(GameStateType gameStateType)
        {
            _gameStateType = gameStateType;
            OnStateChange?.Invoke(_gameStateType);
        }
        
        public void ModifyHungry()
        {
            float delta = _mainGameState.MainGameRule.HungryDeltaRate * _maxHungry;
            UpdateHungry(-delta);
        }
        
        public void UpdateHungry(float delta)
        {
            _currentHungry += delta;
            _currentHungry = Mathf.Clamp(_currentHungry, 0, _maxHungry);
            OnHungryChange?.Invoke(_currentHungry, _maxHungry);
        }
        
        public void SetHungry(float currentHungry)
        {
            _currentHungry = currentHungry;
            OnHungryChange?.Invoke(_currentHungry, _maxHungry);
        }
        
        
        public void ModifyRound()
        {
            SetRound(_currentRound + 1);
        }
        
        public void SetRound(int round)
        {
            _currentRound = round;
            OnRoundChange?.Invoke(_currentRound);
        }
        
        public void ModifyConquerRate(float delta)
        {
            _currentConquerRate += delta;
            _currentConquerRate = Mathf.Clamp(_currentConquerRate, 0f, _maxConquerRate);
            OnConquerRateChange?.Invoke(_currentConquerRate, _maxConquerRate);
        }
        
        public void SetConquerRate(float rate)
        {
            _currentConquerRate = rate;
            OnConquerRateChange?.Invoke(_currentConquerRate, _maxConquerRate);
        }
        
    }
}