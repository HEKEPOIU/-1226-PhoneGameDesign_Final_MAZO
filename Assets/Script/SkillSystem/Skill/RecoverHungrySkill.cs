using System;
using UnityEngine;

namespace SkillSystem.Skill
{
    [CreateAssetMenu(fileName = "RecoverHungrySkill", menuName = "PlayerSkill/RecoverHungrySkill")]
    public class RecoverHungrySkill : GridPlayerSkillBase
    {
        
        [Range(0f, 1f)] [SerializeField] float _hungryRecoverRate = .4f;
        public override void UseSkill(GridSkillSystem gridSkillSystem, Action onSkillUsed)
        {
            gridSkillSystem.Owner.PlayerStates.
                UpdateHungry(_hungryRecoverRate * gridSkillSystem.Owner.PlayerStates.MaxHungry);
            onSkillUsed?.Invoke();
        }
    }
}