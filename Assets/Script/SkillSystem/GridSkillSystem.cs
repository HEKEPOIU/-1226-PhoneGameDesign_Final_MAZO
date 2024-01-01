using System;
using Character.Spawner;
using SkillSystem.Skill;
using UnityEngine;

namespace SkillSystem
{
    public class GridSkillSystem : MonoBehaviour
    {
        [HideInInspector] public Player.Player Owner;
        [HideInInspector] public GridSpawnerManager GridSpawnerManager;
        public GridPlayerSkillBase[] PlayerSkills;
        public Action OnSkillUsed;
        
        public void UseSkill(int skillIndex)
        {
            print("使用技能:" + skillIndex + "  " + PlayerSkills[skillIndex].name);
            PlayerSkills[skillIndex].UseSkill(this, OnSkillUsed);
        }
        
        
    }

}