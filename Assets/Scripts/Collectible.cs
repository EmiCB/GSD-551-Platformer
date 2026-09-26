using UnityEngine;

public class Collectible : MonoBehaviour {
    [SerializeField]
    private GameManager gameManager;

    void Start() {
        if  (gameManager == null) { gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); }

        gameManager.RegisterCollectible(this);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            Debug.Log(collider.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            gameManager.CollectItem();
            gameObject.SetActive(false);
        }
    }
}
