using UnityEngine;

public class Hazzard : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    
    void Start() {
        if  (gameManager == null) { gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            gameManager.LoseGame();
        }
    }
}
