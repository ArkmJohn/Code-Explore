using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer {
    [Serializable]
    public class Weapon : Equipment {
        public WeaponType weaponType;
        public WeaponHold weaponHold;
        public string WeaponName;
        public string WeaponDescription;
        public int WeaponLevel;
        public int Damage;
        public GameObject projectile;

        public override void Dequip () {
            throw new NotImplementedException ();
        }

        public override void Drop () {
            Destroy (gameObject);
        }

        public override void Equip () {
            throw new NotImplementedException ();
        }

        public override void Interact () {
            PickUp ();
        }

        public override void PickUp () {
            throw new NotImplementedException ();
        }
    }

    [Serializable]
    public enum WeaponType {
        SHORTSWORD,
        BROADSWORD,
        BOW,
        CROSSBOW
    }

    [Serializable]
    public enum WeaponHold {
        ONEHANDED,
        TWOHANDED
    }
}