using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1.0f;
    
    void Start() {
        
    }

    void Update() {
        // Handle continuous movement on the horizontal axis
        float horizontalInput = Keyboard.current.dKey.ReadValue() -  Keyboard.current.aKey.ReadValue();

        if (horizontalInput != 0) {
            Vector3 movementDirection = Vector3.right * horizontalInput;
            Vector3 movementVelocity = movementSpeed * movementDirection;
            transform.position += movementVelocity * Time.deltaTime;
        }
    }
}
