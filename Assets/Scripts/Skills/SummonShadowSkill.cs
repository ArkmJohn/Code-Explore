using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JMA.Explorer {
    public class SummonShadowSkill : Skill {
        public Shadow shadowToSummon;
        public int currentShadowCount = 0;
        public int MaxShadowSummonCount = 2;
        public int ShadowSummonIncrement = 1;
        public float ShadowSummonExpGain;
        public float ShadowSummonEnergyCost;
        public float ShadowUptakeCostPerTik;

        public override void InitializeSkill()
        {
            base.InitializeSkill();
            
            EnergyCost = CONST.BaseSummonCost;
            cooldown = CONST.BaseSummonCD;
            maxLevel = CONST.SummonMaxLevel;
            
            ShadowUptakeCostPerTik = CONST.BaseSummonTick;
            MaxShadowSummonCount = CONST.BaseSummonCount;
            ShadowSummonIncrement = CONST.BaseSummonIncrement;

        }

        // TODO: Do this on player class
        public float perTikCost {
            get {
                return ShadowUptakeCostPerTik * currentShadowCount;
            }
        }

        public List<GameObject> ShadowsSummoned;

        public override void ResetSkill () {
            if (ShadowsSummoned.Count > 0) {
                List<GameObject> destroyer = new List<GameObject> ();
                foreach (GameObject c in ShadowsSummoned) {
                    GameObject toDestroy = c;

                    destroyer.Add (toDestroy);
                }
                ShadowsSummoned.Clear ();
                currentShadowCount = 0;
                foreach (GameObject a in destroyer) {
                    Destroy (a);
                }

                destroyer.Clear ();

            }
        }

        public override void UseSkill (Player player = null) {
            Shadow selectedShadow = player.StoreSkill.SelectedShadow;

            if (selectedShadow == null) return;

            if (currentShadowCount >= MaxShadowSummonCount) return;

            player.ModifyEnergy (EnergyCost, () => {
                Debug.Log ("No Energy To Summon");
            }, () => {
                cooldownTimer = cooldown;
                currentShadowCount++;
                GameObject newShadow = Instantiate (player.StoreSkill.SelectedShadow.ShadowPrefab, transform.position, Quaternion.identity);
                ShadowsSummoned.Add (newShadow);
                AddExperience (ShadowSummonExpGain);
            });

        }
    }
}