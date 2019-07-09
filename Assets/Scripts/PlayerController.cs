using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMA.Explorer {
    public enum ClimbingState {
        IDLE,
        CLIMBINGROPE,
        CLIMBINGLADDER
    }

    [RequireComponent (typeof (Player))]
    public class PlayerController : MonoBehaviour {

        public Animator animator;
        public Rigidbody2D rb2D;
        public float CurrentDirection = -1;
        public float JumpForce { get { return GetComponent<Player> ().JumpForce; } }
        public float MoveSpeed { get { return GetComponent<Player> ().MoveSpeed; } }
        public float MoveMultiplier = 1;
        public Vector3 scale;

        public bool CanAct = true;

        [HideInInspector]
        public Item nearestItem;
        [HideInInspector]
        public Interactable currentInteractable;

        [HideInInspector]
        public ClimbingState climbingState;

        [HideInInspector] public bool CanJump = false;

        #region UNITY_CALLBACKS

        // Start is called before the first frame update
        void Start () {
            scale = transform.localScale;
            rb2D = GetComponent<Rigidbody2D> ();
        }

        // Update is called once per frame
        void FixedUpdate () {

            if (CanAct == false) return;

            UpdateMove ();

            // Jump
            animator.SetFloat ("Up-Velocity", rb2D.velocity.y); //new Vector3 (0, rb2D.velocity.y, 0).magnitude);
            if (Input.GetKeyDown (KeyCode.Space) && CanJump) // && new Vector3 (0, rb2D.velocity.y, 0).sqrMagnitude <= 0.001f) 
            {
                animator.SetTrigger ("Jump");
                rb2D.AddForce (Vector3.up * JumpForce);
            }

            // Pick Up
            if (Input.GetKeyDown (KeyCode.Q)) {
                TryInteract ();
            }

            UpdateFace ();
            if(rb2D.velocity.y != 0)
                rb2D.velocity = Vector2.ClampMagnitude (rb2D.velocity, 7);

        }

        private void OnTriggerEnter2D (Collider2D other) {
            // Should check item here
            if (other.gameObject.GetComponent<Item> () != null &&
                nearestItem != other.gameObject.GetComponent<Item> ()) {
                nearestItem = other.gameObject.GetComponent<Item> ();
            }

            if (other.gameObject.GetComponent<Interactable> () != null &&
                currentInteractable != other.gameObject.GetComponent<Interactable> ()) {
                currentInteractable = other.gameObject.GetComponent<Interactable> ();
            }
        }

        private void OnTriggerExit2D (Collider2D other) {
            // Remove nearest item if exiting collider
            if (other.gameObject.GetComponent<Item> () != null &&
                nearestItem == other.gameObject.GetComponent<Item> ()) {
                nearestItem = null;

            }

            if (other.gameObject.GetComponent<Interactable> () != null &&
                currentInteractable == other.gameObject.GetComponent<Interactable> ()) {
                currentInteractable = null;
            }
        }

        private void OnCollisionEnter2D (Collision2D other) {

            if (other.gameObject.GetComponent<Wall> () != null) {
                // lower gravity on touch
                HoldWall ();
            }

            if (other.gameObject.CompareTag ("Ground")) {
                CanJump = true;
            }

        }

        private void OnCollisionExit2D (Collision2D other) 
        {

            if (other.gameObject.GetComponent<Wall> () != null) {
                // return back gravity on touch
                ReleaseWall ();
            }

            if (other.gameObject.CompareTag ("Ground")) {
                CanJump = false;
            }
        }

        #endregion

        #region UPDATE_FUNCTIONS

        private void TryInteract () {

            if (nearestItem != null) {
                nearestItem.UseItem ();
                animator.SetTrigger ("Pick-Up");
            } else if (currentInteractable != null) {
                currentInteractable.Interact ();
                // TODO: Create Interact
                animator.SetTrigger ("Interact");
            }

        }

        /// <summary>
        /// Enables the player to move from left to right
        /// </summary>
        private void UpdateMove () {
            float horizontal = Input.GetAxis ("Horizontal");
            float vertical = Input.GetAxis ("Vertical");
            animator.SetFloat ("Speed", Mathf.Abs (horizontal));
            if (horizontal != 0 &&
                new Vector3 (0, rb2D.velocity.y, 0).sqrMagnitude < 0.001f &&
                climbingState == ClimbingState.IDLE) {

                rb2D.velocity = Vector2.right * horizontal * MoveSpeed * MoveMultiplier;

            }

            if (vertical != 0 && climbingState != ClimbingState.IDLE) {
                // TODO: Create Climbing
                if (climbingState != ClimbingState.CLIMBINGROPE) {
                    animator.SetFloat ("ClimbRopeSpeed", vertical);
                } else {
                    animator.SetFloat ("ClimbingLadderSpeed", vertical);
                }

                rb2D.velocity = Vector2.up * vertical * MoveSpeed * MoveMultiplier;

            }

            UpdateCurrentDirection (horizontal);
        }

        /// <summary>
        /// Updates the facing direction of the sprite sheet
        /// </summary>
        private void UpdateFace () {
            if (CurrentDirection == -1 && !IsLookingAt (1)) {
                transform.localScale = new Vector3 (scale.x, scale.y, scale.z);
            }
            if (CurrentDirection == 1 && !IsLookingAt (-1)) {
                transform.localScale = new Vector3 (-scale.x, scale.y, scale.z);
            }
        }

        #endregion

        #region UTILITY_FUNCTIONS

        /// <summary>
        /// Updates the current direction the player should be placing
        /// </summary>
        /// <param name="horizontal">the direction will be used.</param>
        public void UpdateCurrentDirection (float horizontal) {
            // Going Right
            if (horizontal > 0) {
                if (CurrentDirection != 1) CurrentDirection = 1;
            } else if (horizontal < 0) // Going Left
            {
                if (CurrentDirection != -1) CurrentDirection = -1;
            }
        }

        /// <summary>
        /// Called when the player touches a scalable wall
        /// </summary>
        public void HoldWall () {
            // TODO: Create Hold Wall
            // Animator
            animator.SetTrigger ("Hold-Wall");

            // RB2D Change
            rb2D.gravityScale = 0;
        }

        /// <summary>
        /// Called when the player jumps away from the wall
        /// </summary>
        public void ReleaseWall () {
            // TODO: Create Fall
            // Animator
            animator.SetTrigger ("Fall");

            // RB2D change
            rb2D.gravityScale = 1;
        }

        /// <summary>
        /// When returning true is facing in that direction.
        /// </summary>
        /// <param name="faceDirection">The direction that were checking at, -1 means to the right and 1 to the left.</param>
        /// <returns></returns>
        public bool IsLookingAt (int faceDirection) {
            if (transform.localScale == new Vector3 (scale.x * faceDirection, scale.y, scale.z)) {
                return true;
            } else {
                return false;
            }
        }

        #endregion
    }
}