using System;
using Character.Spawner;
using UnityEngine;

namespace SkillSystem.Skill
{
    [CreateAssetMenu(fileName = "MoveFrontSkill", menuName = "PlayerSkill/MoveFrontSkill")]
    public class MoveFrontSkill : GridPlayerSkillBase
    {

        public override void UseSkill(GridSkillSystem gridSkillSystem, Action onSkillUsed)
        {
            gridSkillSystem.Owner.PlayerController.PlayerCharacter.DashSkill();
        }

    }
}