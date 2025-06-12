using UnityEngine;
using UnityEngine.UI;

public class MenuPendulum : MonoBehaviour
{
    public RectTransform pivot;      // ����� ������� (����)
    public RectTransform bob;        // ���� (����)
    public RectTransform stringRect; // ���� (Image-�������������)
    public float length = 220f;      // ����� ���� � ��������
    public float maxAngle = 45f;     // ������������ ���� (�������)
    public float speed = 1.2f;       // �������� �������
    public bool reverseDirection = false; // ��� ������� ��������

    private float currentAngle;
    private float direction = 1f;

    void Start()
    {
        currentAngle = -maxAngle;
        UpdatePendulum();
    }

    void Update()
    {
        currentAngle += direction * speed * Time.deltaTime * maxAngle;
        if (currentAngle > maxAngle)
        {
            currentAngle = maxAngle;
            direction = -1f;
        }
        else if (currentAngle < -maxAngle)
        {
            currentAngle = -maxAngle;
            direction = 1f;
        }
        UpdatePendulum();
    }

    void UpdatePendulum()
    {
        if (pivot == null || bob == null || stringRect == null) return;
        float angleRad = (reverseDirection ? -currentAngle : currentAngle) * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Sin(angleRad), -Mathf.Cos(angleRad)) * length;
        bob.anchoredPosition = pivot.anchoredPosition + offset;

        // �����: pivot � stringRect ������ ���� (0.5, 1) � ������� ����!
        stringRect.sizeDelta = new Vector2(stringRect.sizeDelta.x, length);
        stringRect.anchoredPosition = pivot.anchoredPosition;
        float angle = Mathf.Atan2(offset.x, -offset.y) * Mathf.Rad2Deg;
        stringRect.localRotation = Quaternion.Euler(0, 0, angle);
    }
}