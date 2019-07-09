using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JMA.Explorer {
    public class ShadowInteractable : Interactable {

        public Shadow shadowContainer;

        public override void Interact () {
            // TODO: Show prompt to use the skill instead.
        }
    }

    [Serializable]
    public class Shadow {
        public string ShadowType;
        public GameObject ShadowPrefab;
    }
}