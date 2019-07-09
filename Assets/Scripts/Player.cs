using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer {
    [RequireComponent (typeof (PlayerController))]
    public class Player : MonoBehaviour {

        public float JumpForce = 250;
        public float MoveSpeed = 3;

        public int maxHealth;
        [HideInInspector]
        public int currentHealth;

        public float maxEnergy;
        [HideInInspector]
        public float currentEnergy;

        public StoreShadowSkill StoreSkill;
        public SummonShadowSkill SummonSkill;
        public Skill TeleportSkill;

        public ShadowSummon currentlySelectedSummon = null;

        public Equipped currentEquipment;

        private void Start () {
            MoveSpeed = CONST.BaseMoveSpeed;
            JumpForce = CONST.BaseJumpForce;
            maxEnergy = CONST.BaseEnergy;
            maxHealth = CONST.BaseHealth;
        }

        private void Update () {
            if (Input.GetKeyDown (KeyCode.Alpha1)) {
                TryActivateSkill (StoreSkill);
            }

            if (Input.GetKeyDown (KeyCode.Alpha2)) {
                TryActivateSkill (SummonSkill);
            }

            if (Input.GetKeyDown (KeyCode.Alpha3)) {
                TryActivateSkill (TeleportSkill);
            }
        }

        public void ModifyEnergy (float energyModifier, Action OnEnergyEmpty = null, Action OnEnergyUsed = null) {
            float updatedEnergy = currentEnergy + energyModifier;
            if (updatedEnergy > 0) {
                if (updatedEnergy >= maxEnergy)
                    updatedEnergy = maxEnergy;

                currentEnergy = updatedEnergy;
                OnEnergyUsed ();
            } else {
                // No More Energy
                OnEnergyEmpty ();
            }
        }

        public void ModifyHealth (int healthModifer, Action OnHealthChange = null, Action OnHealthOver = null) {
            int updatedHealth = currentHealth + healthModifer;
            if (updatedHealth > 0) {
                if (updatedHealth >= maxHealth)
                    updatedHealth = maxHealth;

                currentHealth = updatedHealth;
                OnHealthChange ();
            } else {
                OnHealthOver ();
            }
        }

        private void TryActivateSkill (Skill skill) {
            if (skill.skillEnabled) {
                if (skill.cooldownTimer <= 0) {
                    skill.UseSkill (this);
                }
            }
        }

        #region EQUIPMENT_HANDLERS

        #region EQUIPMENT_GET/SET
        public Weapon LeftHand
        {
            get
            {
                return currentEquipment.LeftHand;
            }
            set
            {
                currentEquipment.LeftHand = value;
            }
        }

        public Weapon RightHand
        {
            get
            {
                return currentEquipment.RightHand;
            }
            set
            {
                currentEquipment.RightHand = value;
            }
        }

        public Armor Headgear
        {
            get
            {
                return currentEquipment.Headgear;
            }
            set
            {
                currentEquipment.Headgear = value;
            }
        }

        public Armor Chestpiece
        {
            get
            {
                return currentEquipment.Chestpiece;
            }
            set
            {
                currentEquipment.Chestpiece = value;
            }
        }

        public Armor Footwear
        {
            get
            {
                return currentEquipment.Footwear;
            }
            set
            {
                currentEquipment.Footwear = value;
            }
        }

        public Armor Cape
        {
            get
            {
                return currentEquipment.Cape;
            }
            set
            {
                currentEquipment.Cape = value;
            }
        }
        #endregion
        
        public void TryEquip (Weapon newWeapon) {
            switch (newWeapon.weaponType) {
                case WeaponType.SHORTSWORD:
                    if (LeftHand == null && RightHand.weaponHold == WeaponHold.ONEHANDED) {
                        // Equip in left hand
                        LeftHand = newWeapon;
                        LeftHand.Equip ();
                    } else {
                        if (RightHand != null && RightHand.weaponHold == WeaponHold.TWOHANDED) {
                            // Is using two handed
                            // TODO: Store current weapon befored dequiping
                            RightHand.Dequip ();
                            RightHand = newWeapon;
                            RightHand.Equip ();
                        } else if (RightHand == null && LeftHand != null) {
                            RightHand = newWeapon;
                            RightHand.Equip ();
                        } else if (currentEquipment != null && LeftHand != null) {
                            RightHand.Dequip ();
                            RightHand = newWeapon;
                            RightHand.Equip ();
                        }
                    }
                    break;
                case WeaponType.BROADSWORD:
                case WeaponType.BOW:
                    if (RightHand != null) {
                        RightHand.Dequip ();
                        RightHand = null;
                    }

                    if (LeftHand != null) {
                        LeftHand.Dequip ();
                        LeftHand = null;
                    }

                    RightHand = newWeapon;
                    RightHand.Equip ();

                    break;
                case WeaponType.CROSSBOW:
                    if (LeftHand == null && RightHand.weaponHold == WeaponHold.ONEHANDED) {
                        // Equip in left hand
                        LeftHand = newWeapon;
                        LeftHand.Equip ();
                    } else {
                        if (RightHand != null && RightHand.weaponHold == WeaponHold.TWOHANDED) {
                            // Is using two handed
                            // TODO: Store current weapon befored dequiping
                            RightHand.Dequip ();
                            RightHand = newWeapon;
                            RightHand.Equip ();
                        } else if (RightHand == null && LeftHand != null) {
                            RightHand = newWeapon;
                            RightHand.Equip ();
                        } else if (currentEquipment != null && LeftHand != null) {
                            RightHand.Dequip ();
                            RightHand = newWeapon;
                            RightHand.Equip ();
                        }
                    }
                    break;
                default:
                    Debug.LogError ("No Weapon of that type");
                    break;

            }
        }

        public void TryEquip (Armor newArmor) {
            switch (newArmor.armorType) {
                case ArmorType.HEADGEAR:
                    if (Headgear != null) {
                        Headgear.Dequip ();
                        Headgear = null;
                    }

                    Headgear = newArmor;
                    Headgear.Equip ();
                    break;
                case ArmorType.CHESTPIECE:
                    if (Chestpiece != null) {
                        Chestpiece.Dequip ();
                        Chestpiece = null;
                    }

                    Chestpiece = newArmor;
                    Chestpiece.Equip ();
                    break;
                case ArmorType.FOOTWEAR:
                    if (Footwear != null) {
                        Footwear.Dequip ();
                        Footwear = null;
                    }

                    Footwear = newArmor;
                    Footwear.Equip ();
                    break;
                case ArmorType.CAPE:
                    if (Cape != null) {
                        Cape.Dequip ();
                        Cape = null;
                    }

                    Cape = newArmor;
                    Cape.Equip ();
                    break;
            }
        }

        #endregion

    }

    [Serializable]
    public class Equipped {
        public Weapon LeftHand, RightHand;
        public Armor Headgear, Chestpiece, Footwear, Cape;
    }
}