using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer {
    public enum GameState {
        READY,
        PLAYING,
        GAMEOVER,
        ONMENU
    }

    [Serializable]
    public class Room {
        /// <summary>
        /// The objects that will be disabled or enabled when the room loads in and and out
        /// </summary>
        [Tooltip ("The objects that will be disabled or enabled when the room loads in and and out")]
        public List<GameObject> roomObjects;

        /// <summary>
        /// Enabled when the player is currently in the room that this script is referencing
        /// </summary>
        public bool IsPlayerInRoom;

        public Transform roomEntrance, roomExit;

        /// <summary>
        /// Call when a player enters a room
        /// </summary>
        public void ExitRoom () {
            IsPlayerInRoom = false;
            // Deactivate Objects
            foreach (GameObject ob in roomObjects) {
                ob.SetActive (false);
            }
            // TODO: Reset Room

        }

        /// <summary>
        /// Call when a player exits a room
        /// </summary>
        public void EnterRoom () {
            IsPlayerInRoom = true;
            // Activate Objects
            foreach (GameObject ob in roomObjects) {
                ob.SetActive (true);
            }

            // TODO: Initialize Room assets

        }

        public Vector3 EntrancePosition { get { return roomEntrance.position; } }
        public Vector3 ExitPosition { get { return roomExit.position; } }

    }

    public class GameManager : MonoBehaviour 
    {

        public static GameManager instance;
        public PlayerController playerController;
        public Player player;
        public List<GameObject> EnemyPrefabs, ItemPrefabs, RoomList;
        public GameState currentGameState = GameState.READY;
        public List<Room> rooms;
        public int currentRoomIndex = 0;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake () {
            if (instance == null)
                instance = this;
            else {
                Destroy (this);
            }

            DontDestroyOnLoad (this.gameObject);
        }

        /// <summary>
        /// Start is called on the 
        /// frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start () {
            playerController = FindObjectOfType<PlayerController> ();
            player = FindObjectOfType<Player>();
        }

        /// <summary>
        /// Toggle the state if the player can move
        /// </summary>
        /// <param name="state">The state that will be change</param>
        public void TogglePlayerMove (bool state) {
            playerController.CanAct = state;
        }

        /// <summary>
        /// Goes to the next room from the current player
        /// Moves the player
        /// </summary>
        public void GoNextRoom () {
            int nextRoomId = currentRoomIndex;
            rooms[currentRoomIndex].ExitRoom ();
            if (currentRoomIndex + 1 < rooms.Count)
                currentRoomIndex++;
            else {
                currentRoomIndex = rooms.Count - 1;
            }
            // Go Do Action Here
            rooms[currentRoomIndex].EnterRoom ();
            playerController.transform.position = rooms[currentRoomIndex].EntrancePosition;
        }

        /// <summary>
        /// Goes to the previous room from the current player
        /// Moves the player
        /// </summary>
        public void GoToPreviousRoom () {
            int nextRoomId = currentRoomIndex;
            rooms[currentRoomIndex].ExitRoom ();
            if (currentRoomIndex - 1 >= 0)
                currentRoomIndex--;
            else {
                currentRoomIndex = 0;
            }
            // Go Do Action Here
            rooms[currentRoomIndex].EnterRoom ();
            playerController.transform.position = rooms[currentRoomIndex].EntrancePosition;

        }
    }
}