using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public GameObject obstacleToDisable; // ���������� ���� ����������� � ����������
    public Sprite leverOnSprite;         // ������ ��� ��������������� ������ (�� �������)
    public Sprite leverOffSprite;        // ������ ��� ����������� ������ (�� �������)

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
        if (!activated && other.CompareTag("Bob")) // Bob � ��� ��������
        {
            activated = true;
            if (obstacleToDisable != null)
                obstacleToDisable.SetActive(false); // ����������� ��������

            if (spriteRenderer != null && leverOnSprite != null)
                spriteRenderer.sprite = leverOnSprite; // ������ ������ ������
        }
    }
}