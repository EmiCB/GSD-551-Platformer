using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementAmount = 1.0f;
    
    void Start() {
        
    }

    void Update()
    {
        bool isMoveRightPressed = Keyboard.current.dKey.wasPressedThisFrame; 
        if (isMoveRightPressed) { MoveRight(); }
        
        bool moveLeftPressed = Keyboard.current.aKey.wasPressedThisFrame;
        if (moveLeftPressed) { MoveLeft(); }
    }
    
    /** Moves the player to the right by its movementAmount in world space. */
    private void MoveRight() {
        Debug.Log("MoveRight was called.");
        gameObject.transform.position += Vector3.right * movementAmount;
    }
    
    /** Moves the player to the left by its movementAmount in world space. */
    private void MoveLeft() {
        Debug.Log("MoveLeft was called.");
        gameObject.transform.position += Vector3.left * movementAmount;
    }
}
