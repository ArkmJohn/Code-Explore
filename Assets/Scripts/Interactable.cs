using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer {
    public abstract class Interactable : MonoBehaviour {

        public abstract void Interact ();

        public virtual void DestroyInteractable()
        {
            Destroy(gameObject);
        }
    }
}