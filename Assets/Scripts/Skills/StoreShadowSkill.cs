using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JMA.Explorer {
    public class StoreShadowSkill : Skill {
        public int shadowStoreCount = 5;
        public int shadowStoreIncrementPerLevel;
        private int shadowStoreBaseCount;
        public List<Shadow> shadowsStored;
        public float shadowStoreEXPGain = 0.00001f;
        public Shadow SelectedShadow;
        private void Start () {
            shadowStoreBaseCount = shadowStoreCount;
        }

        public override void InitializeSkill () {
            base.InitializeSkill ();

            EnergyCost = CONST.BaseStoreCost;
            cooldown = CONST.BaseStoreCD;
            maxLevel = CONST.StoreMaxLevel;

            shadowStoreBaseCount = CONST.BaseStoreAmount;
            shadowStoreIncrementPerLevel = CONST.BaseStoreIncrement;

            shadowStoreCount = shadowStoreBaseCount;
            shadowsStored.Clear ();

        }

        public override void LevelUpSkill () {
            base.LevelUpSkill ();
            shadowStoreCount = shadowStoreBaseCount + (shadowStoreIncrementPerLevel * lvl);
        }

        public override void ResetSkill () {
            shadowsStored.Clear ();
            InitializeSkill ();
        }

        public override void UseSkill (Player player) {
            // TODO: If the player does not have a viable shadow interactable sned warning or prompt
            // Checks if the user has a current interactable
            if (player.GetComponent<PlayerController> ().currentInteractable != null) {
                ShadowInteractable sI = player.GetComponent<PlayerController> ().currentInteractable.GetComponent<ShadowInteractable> ();
                if (sI == null) return;

                // Store if shadowCount is bigger than shadow stored
                if (shadowStoreCount > shadowsStored.Count) {
                    cooldownTimer = cooldown;
                    shadowsStored.Add (sI.shadowContainer);
                    AddExperience (shadowStoreEXPGain); // Adds experience based on the level
                    player.ModifyEnergy (EnergyCost); // Usually Costs Nothing
                }
            }
        }
    }

}