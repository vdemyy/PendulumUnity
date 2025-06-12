using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public GameObject obstacleToDisable; // Перетащите сюда препятствие в инспекторе
    public Sprite leverOnSprite;         // Спрайт для активированного рычага (по желанию)
    public Sprite leverOffSprite;        // Спрайт для неактивного рычага (по желанию)

    private SpriteRenderer spriteRenderer;
    private bool activated = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && leverOffSprite != null)
            spriteRenderer.sprite = leverOffSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Bob")) // Bob — тег маятника
        {
            activated = true;
            if (obstacleToDisable != null)
                obstacleToDisable.SetActive(false); // Препятствие исчезает

            if (spriteRenderer != null && leverOnSprite != null)
                spriteRenderer.sprite = leverOnSprite; // Меняем спрайт рычага
        }
    }
}