using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GatedPlatform : MonoBehaviour {
    [SerializeField]
    private int requiredCollectibles;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TextMeshPro unlockStatusText;
    
    [SerializeField]
    private TilemapCollider2D collider;
    [SerializeField]
    private TilemapRenderer renderer;
    private bool _isSolid = false;
    
    [SerializeField]
    private float ghostingAlpha = 0.3f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (collider == null) { GetComponent<TilemapCollider2D>(); }
        if (renderer == null) { GetComponent<TilemapRenderer>(); }
        
        SetPlatformState(false);
        UpdateUnlockStatusText(0);
        gameManager.RegisterGatedPlatform(this);
    }
    
    public void UpdateGatedPlatformState(int currentCollectibles) {
        SetPlatformState(currentCollectibles >= requiredCollectibles);
        UpdateUnlockStatusText(currentCollectibles);
    }

    private void SetPlatformState(bool isSolid) {
        _isSolid = isSolid;
        collider.enabled = isSolid;
        //Color color = isSolid ? Color.green : Color.red;
        Color color = renderer.material.color;
        color.a = isSolid ? 1.0f : ghostingAlpha;
        renderer.material.color = color;
    }

    private void UpdateUnlockStatusText(int currentCollectibles) {
        if (unlockStatusText == null) { return; }

        if (_isSolid) {
            unlockStatusText.gameObject.SetActive(false);
            return;
        }
        
        unlockStatusText.text = currentCollectibles.ToString() + "/"  + requiredCollectibles.ToString();
        unlockStatusText.gameObject.SetActive(true);
    }
}
