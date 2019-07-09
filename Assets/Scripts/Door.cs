using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer 
{
    public class Door : Interactable
    {
        public enum DoorState
        {
            NONE,
            BONUS,
            EXIT,
            ENTRANCE
        }

        public DoorState currentDoorState;

        public override void Interact()
        {
            switch(currentDoorState)
            {
                case DoorState.ENTRANCE:
                    GameManager.instance.GoToPreviousRoom();
                    break;
                case DoorState.EXIT:
                    GameManager.instance.GoNextRoom();
                    break;
                case DoorState.BONUS:
                    // TODO: add bonus level
                    break;
                case DoorState.NONE:
                    // Just a placeholder
                    break;
                default:
                    Debug.Log("No Door type found");
                    break;
            }
        }
    }
}