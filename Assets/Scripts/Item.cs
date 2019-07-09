using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace JMA.Explorer
{
    public abstract class Item : Interactable
    {
        public abstract void ConsumeItem();
        public abstract void DestroyItem();
        public abstract void UseItem();
    }
}