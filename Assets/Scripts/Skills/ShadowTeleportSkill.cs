using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JMA.Explorer {
    public class ShadowTeleportSkill : Skill {
        public override void InitializeSkill () {
            base.InitializeSkill ();

            EnergyCost = CONST.BaseTeleportCost;
            cooldown = CONST.BaseTeleportCD;
            maxLevel = CONST.TeleportMaxLevel;
        }
        public override void ResetSkill () {
            InitializeSkill ();
        }

        public override void UseSkill (Player player = null) {
            player.ModifyEnergy (EnergyCost, () => {
                // NO Energy
            }, () => {
                cooldownTimer = cooldown;
                // SWAP ME
                Vector3 shadowSummonPosition = player.currentlySelectedSummon.transform.position;
                Vector3 playerPosition = player.gameObject.transform.position;
                player.gameObject.transform.position = shadowSummonPosition;
                player.currentlySelectedSummon.transform.position = playerPosition;
            });
        }
    }
}