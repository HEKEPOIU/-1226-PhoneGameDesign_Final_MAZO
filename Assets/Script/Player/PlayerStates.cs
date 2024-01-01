using System;
using Manager.States;
using UnityEngine;

namespace Player
{
    public class PlayerStates : MonoBehaviour
    {
        private float _currentHungry;
        [SerializeField] private float _maxHungry = 100f;
        public event Action<float,float> OnHungryChange; 
        
        
        public void ResetState(MainGameState mainGameState)
        {
            SetHungry(mainGameState.GameRule.StartHungryRate * _maxHungry);
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
        
    }
}