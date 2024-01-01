using System;
using Character.Spawner;
using UnityEngine;

namespace SkillSystem.Skill
{
    [CreateAssetMenu(fileName = "SeaAttackSkill", menuName = "PlayerSkill/SeaAttackSkill")]
    public class SeaAttackSkill : GridPlayerSkillBase
    {
        public override void UseSkill(GridSkillSystem gridSkillSystem, Action onSkillUsed)
        {
            
            gridSkillSystem.Owner.PlayerController.RangeAttack();
        }
    }
}