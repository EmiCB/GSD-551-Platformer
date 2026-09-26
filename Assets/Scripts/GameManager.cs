using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField][Tooltip("How many Collectibles are required for the player to complete the level.")]
    private int requiredCollectibles = 3;
    
    private int _currentCollectibles = 0;
    private bool _isGameOver = false;
    private Vector3 _playerStartPosition = new Vector3();
    private List<Collectible> _collectibles = new List<Collectible>();

    [SerializeField][Tooltip("The TMP component where the current collectible status is displayed to the player.")]
    private TextMeshProUGUI collectiblesText;

    [SerializeField][Tooltip("GameObject to show on player win.")]
    private CanvasGroup youWinUI;
    [SerializeField][Tooltip("GameObject to show on player lose.")]
    private CanvasGroup youLoseUI;

    [SerializeField][Tooltip("Root for all in-game objects.")]
    private GameObject gameplayRoot;

    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    private Rigidbody2D playerRb;
    
    void Start() {
        // send custom errors if required references are missing
        if (collectiblesText == null) { Debug.LogError("Collectibles Text reference is not set!"); }
        if (youWinUI == null) { Debug.LogError("Win UI reference is not set!"); }
        if (youLoseUI == null) { Debug.LogError("Lose UI reference is not set!"); }

        // find player references automatically if they are missing
        if (player == null) { player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); }
        if (playerRb == null) { playerRb = player.GetComponent<Rigidbody2D>(); }
        
        // save any initial values for resetting
        _playerStartPosition = player.transform.position;
        
        // set initial state of the game
        SetUIState(youWinUI, false);
        SetUIState(youLoseUI, false);
        gameplayRoot.SetActive(true);
        SetPlayerGameplay(true);
        
        // update player UI
        UpdateCollectibleText();
    }
    
    public void CollectItem() {
        _currentCollectibles++;
        UpdateCollectibleText();

        // Check for player win condition
        if (_currentCollectibles >= requiredCollectibles) {
            HandleGameOver(youWinUI);
        }
    }

    private void UpdateCollectibleText() {
        collectiblesText.text = "Collectibles: " + _currentCollectibles + "/" + requiredCollectibles;
    }
    
    private void HandleGameOver(CanvasGroup resultsPanel) {
        if (_isGameOver) { return; }
        
        _isGameOver = true;
        SetPlayerGameplay(false);
        SetUIState(resultsPanel, true);
    }

    public void LoseGame() {
        HandleGameOver(youLoseUI);
    }

    private void SetUIState(CanvasGroup panel, bool isEnabled) {
        panel.alpha = isEnabled ? 1 : 0;
        panel.gameObject.SetActive(isEnabled);
    }

    private void SetPlayerGameplay(bool isEnabled) {
        player.enabled = isEnabled;
        playerRb.simulated = isEnabled;
        player.GetComponent<Animator>().speed = isEnabled ? 1.0f : 0.0f;
    }

    public void ResetGameplay() {
        // reset player state
        player.transform.position = _playerStartPosition;
        SetPlayerGameplay(true);
        
        // reset game state
        foreach (Collectible collectible in _collectibles) {
            collectible.gameObject.SetActive(true);
        }
        _currentCollectibles = 0;
        UpdateCollectibleText();
        _isGameOver = false;
        
        // reset UI
        SetUIState(youWinUI, false);
        SetUIState(youLoseUI, false);
    }

    public void RegisterCollectible(Collectible collectible) {
        _collectibles.Add(collectible);
    }
}
