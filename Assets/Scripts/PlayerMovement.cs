using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] [Tooltip("How fast the player moves horizontally.")]
    private float movementSpeed = 1.0f;
    [SerializeField] [Tooltip("How high the player jumps.")]
    private float jumpSpeed = 5.0f;
    
    [SerializeField] [Tooltip("The player's Rigidbody2D component.")]
    private Rigidbody2D playerRb;
    
    [SerializeField] [Tooltip("Transform at the bottom of the player.")]
    private Transform groundCheck;
    [SerializeField] [Tooltip("Radius of the ground check.")]
    private float groundCheckRadius = 0.05f;
    [SerializeField] [Tooltip("The layer that defines the 'ground'.")]
    private LayerMask groundLayer;
    
    /// <summary> Stores the user's current horizontal input value. </summary>
    private float horizontalInput;
    /// <summary> Stores if player is currently detected on any ground objects.</summary>
    private bool isGrounded;
    private bool isJumping;
    
    void Start() {
        // automatically find references in case they are not assigned in the inspector.
        if (playerRb == null) { playerRb = GetComponent<Rigidbody2D>(); }
        if (groundCheck == null) { groundCheck = transform.Find("GroundCheck"); }
        if (groundLayer == 0) { groundLayer = LayerMask.GetMask("Ground"); }
        
        isGrounded = false;
        isJumping = false;
    }

    void Update() {
        horizontalInput = Keyboard.current.dKey.ReadValue() -  Keyboard.current.aKey.ReadValue();
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            isJumping = true;
        }
    }

    private void FixedUpdate() {
        CheckGround();
        Move(horizontalInput);
        if (isJumping) { Jump(); }
    }

    private void OnDrawGizmosSelected() {
        if (groundCheck == null) {
            Debug.LogError("ERR: No reference assigned for `groundCheck`.");
            return;
        }

        if (isGrounded) {
            Gizmos.color = Color.green;
        }
        else {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    /// <summary>
    /// Handles the player's movement via the Unity physics system.
    /// </summary>
    /// <param name="movementInput"></param>
    private void Move(float movementInput) {
        // horizontal movement
        float horizontalVelocity = movementInput * movementSpeed;
        Vector2 movementVelocity = new Vector2(horizontalVelocity, playerRb.linearVelocityY);
        playerRb.linearVelocity = movementVelocity;
    }

    /// <summary>
    /// Handles a single player jump via the Unity physics system.
    /// </summary>
    private void Jump() {
        if (!isGrounded) { return; }
        
        Vector2 jumpVelocity = new Vector2(playerRb.linearVelocityX, jumpSpeed);
        playerRb.linearVelocity = jumpVelocity;
        isJumping = false;
    }

    /// <summary>
    /// Handles physics computations to determine if the player is currently on a ground object.
    /// </summary>
    private void CheckGround() {
        Collider2D detectedGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = detectedGround != null;
    }
}
