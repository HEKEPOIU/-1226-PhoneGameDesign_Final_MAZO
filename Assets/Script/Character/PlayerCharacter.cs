using System;
using Character.Spawner;
using GirdSystem;
using SkillSystem;
using SkillSystem.Skill;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class PlayerCharacter : BaseGridCharacter
    {
        [SerializeField] private Vector2Int _startPosition;
        [SerializeField] private int _maxDashDistance;
        [SerializeField] private GridPlayerSkillBase[] _playerSkills;
        [HideInInspector] public bool IsUnstoppable = false;
        public event Action OnPlayerBeHit;
        public event Action<int> OnDash;

        private void Start()
        {
            InitGridObject();
        }

        public override void InitGridObject()
        {
            base.InitGridObject();
            
            Move(_startPosition);

        }

        public override void Interact(IGridObject interacter)
        {
            base.Interact(interacter);
            EnemyGridCharacter enemyGridCharacter = interacter as EnemyGridCharacter;
            
            
            if (enemyGridCharacter && !IsUnstoppable)
            {
                OnPlayerBeHit?.Invoke();
            }
        }
        
        public void DashSkill()
        {
            IsUnstoppable = true;
            OnDash?.Invoke(_maxDashDistance);
        }
        
        public void EndDashSkill()
        {
            IsUnstoppable = false;
        }
    }
}