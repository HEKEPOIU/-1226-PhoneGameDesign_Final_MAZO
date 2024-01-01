using System;
using Character.Spawner;
using UnityEngine;

namespace SkillSystem.Skill
{
    [CreateAssetMenu(fileName = "SpawnEnemySkill", menuName = "PlayerSkill/SpawnEnemySkill")]
    public class SpawnEnemySkill : GridPlayerSkillBase
    {
        [SerializeField] private int _spawnCount = 8;
        
        public override void UseSkill(GridSkillSystem gridSkillSystem, Action onSkillUsed)
        {
            gridSkillSystem.GridSpawnerManager.RandomSpawnEnemyOnGrid(_spawnCount);
            onSkillUsed?.Invoke();
        }
    }
}