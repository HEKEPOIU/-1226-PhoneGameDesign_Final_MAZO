using System;
using Character.Spawner;
using UnityEngine;

namespace SkillSystem.Skill
{
    
    public abstract class GridPlayerSkillBase : ScriptableObject
    {
        public GameObject SkillCardPrefab;
        public GameObject SkillBallPrefab;
        
        public abstract void UseSkill(GridSkillSystem gridSkillSystem, Action onSkillUsed);
    }
}