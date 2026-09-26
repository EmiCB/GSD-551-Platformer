using UnityEngine;

public class Camera : MonoBehaviour {
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float speed = 0.125f;
    
    private Vector3 _velocity = Vector3.zero;

    void Start() {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position;
    }
    
    void LateUpdate() {
        Vector3 nextPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, nextPosition, ref _velocity, speed);
        transform.position = smoothedPosition;
    }
}
