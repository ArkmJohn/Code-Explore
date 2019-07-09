using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer {
    public abstract class Equipment : Interactable
    {
        public abstract void Equip();
        public abstract void Dequip();
        public abstract void Drop();
        public abstract void PickUp();
    }
}