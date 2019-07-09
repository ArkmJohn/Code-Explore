using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer {
    public abstract class Skill : MonoBehaviour
    {
        public bool skillEnabled = false;
        public int maxLevel;
        public int lvl;
        public float currentExp;
        public float cooldownTimer;
        public float cooldown;
        public float EnergyCost = 0.001f;

        protected virtual void Update() 
        {
            if(cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Initializes the skill and sets the base stat for the skill
        /// </summary>
        public virtual void InitializeSkill()
        {
            skillEnabled = true;
            lvl = 1;
            currentExp = 0f;
            cooldownTimer = 0;
        }

        /// <summary>
        /// Use the skill, we should call add experience here
        /// </summary>
        /// <param name="player">The player that will use the skill</param>
        public abstract void UseSkill(Player player = null);
        public abstract void ResetSkill();
        public virtual void LevelUpSkill()
        {
            // TODO: Add effect on level up;
            if(lvl + 1 >= maxLevel)
            {
                // Already Max Level
            }
            else
               lvl++;
        }

        /// <summary>
        /// Adds experience to the skill. 1 point is equal to 1 level so the range should be from 0-1
        /// </summary>
        /// <param name="percentage"> The percentage that will be added to the exp bar. </param>
        public virtual void AddExperience(float percentage)
        {
            if(currentExp + percentage >= 1) // Level up here
            {
                LevelUpSkill();
            }
            else
            {
                currentExp += percentage;
            }
        }
    }
}