using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer 
{
    public class ParallaxLayerController : MonoBehaviour
    {
        
        public PlayerController currentPlayer{get{return GameManager.instance.playerController;}}
        public List<ParallaxLayer> layers;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            foreach(ParallaxLayer pL in layers)
            {
                if(!pL.LayerObject.activeSelf) return;
                
                // Get target Position
                Vector2 targetPos = pL.LayerObject.transform.position;
                targetPos = (targetPos + pL.moveDirection) * pL.speed * Time.deltaTime;
                pL.LayerObject.transform.position = targetPos;
            }
        }
    }

    [Serializable]
    public struct ParallaxLayer
    {
        public string name;
        public GameObject LayerObject;
        public float speed;
        public Vector2 moveDirection;
    }
}