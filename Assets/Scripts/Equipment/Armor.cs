using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer {
    [Serializable]
    public class Armor : Equipment {
        public ArmorType armorType;
        public ArmorSet armorSet;

        public override void Dequip () {
            throw new NotImplementedException ();
        }

        public override void Drop () {
            throw new NotImplementedException ();
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
    public enum ArmorType {
        HEADGEAR,
        CHESTPIECE,
        FOOTWEAR,
        CAPE
    }

    [Serializable]
    public enum ArmorSet {
        LIGHT,
        MEDIUM,
        HEAVY,
        UTILITY

    }
}