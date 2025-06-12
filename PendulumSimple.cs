using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PendulumSimple : MonoBehaviour
{
    [Header("Параметры маятника")]
    public float length = 1.0f;
    public float startAngleDeg = 45.0f;
    public float dt = 0.02f;

    [Header("UI")]
    public Slider lengthSlider;
    public Slider angleSlider;
    public Slider timeStepSlider;
    public TextMeshProUGUI lengthText;
    public TextMeshProUGUI angleText;
    public TextMeshProUGUI timeStepText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI exactTimeText;

    [Header("Маятник")]
    public Transform pivot;
    public Transform bob;
    public LineRenderer pendulumLine;

    float g = 9.81f;
    float theta, omega;
    float savedTheta; // Сохранённый угол в радианах
    bool running = false;
    bool goalReached = false;
    float elapsedTime = 0f;
    bool isSliderUpdating = false;

    void Start()
    {
        InitSliders();
        savedTheta = -startAngleDeg * Mathf.Deg2Rad;
        ResetPendulum();
    }

    void InitSliders()
    {
        if (lengthSlider != null)
        {
            lengthSlider.minValue = 0.5f;
            lengthSlider.maxValue = 10f;
            lengthSlider.value = length;
            lengthSlider.onValueChanged.AddListener((v) => {
                if (!isSliderUpdating)
                {
                    length = v;
                    UpdateUIText();
                    UpdatePendulumVisual(); // Без сброса угла
                }
            });
        }

        if (angleSlider != null)
        {
            angleSlider.minValue = 0f;
            angleSlider.maxValue = 170f;
            angleSlider.value = startAngleDeg;
            angleSlider.onValueChanged.AddListener((v) => {
                if (!isSliderUpdating)
                {
                    startAngleDeg = v;
                    savedTheta = -startAngleDeg * Mathf.Deg2Rad;
                    UpdateUIText();
                    ResetPendulum();
                }
            });
        }

        if (timeStepSlider != null)
        {
            timeStepSlider.minValue = 0.01f;
            timeStepSlider.maxValue = 0.1f;
            timeStepSlider.value = dt;
            timeStepSlider.onValueChanged.AddListener((v) => {
                if (!isSliderUpdating)
                {
                    dt = v;
                    UpdateUIText();
                    ResetPendulum();
                }
            });
        }

        UpdateUIText();
    }

    void UpdateUIText()
    {
        if (lengthText != null) lengthText.text = $"Длина нити: {length:F2}";
        if (angleText != null) angleText.text = running ? $"Текущий угол: {Mathf.Abs(theta * Mathf.Rad2Deg):F1}°" : $"Начальный угол: {startAngleDeg:F1}°";
        if (timeStepText != null) timeStepText.text = $"Шаг времени: {dt:F3}";

        double theta0 = startAngleDeg * Mathf.Deg2Rad;
        double exactPeriod = 4.0 * Mathf.Sqrt(length / g) * cei1(Mathf.Sin((float)(theta0 / 2.0)));
        double exactTime = exactPeriod / 2.0;

        if (exactTimeText != null)
            exactTimeText.text = $"Время (точное): {exactTime:F2} сек";
    }

    double cei1(double k)
    {
        double t = 1 - k * k;
        double a = (((0.01451196212 * t + 0.03742563713) * t + 0.03590092383) * t + 0.09666344259) * t + 1.38629436112;
        double b = (((0.00441787012 * t + 0.03328355346) * t + 0.06880248576) * t + 0.12498593597) * t + 0.5;
        return a - b * Mathf.Log((float)t);
    }

    public void StartPendulum()
    {
        running = true;
        goalReached = false;
        elapsedTime = 0f;
        theta = savedTheta;
        omega = 0f;
        UpdatePendulumVisual();
        if (timerText != null)
            timerText.text = $"Время: 0.00 сек";
    }

    public void StopPendulum()
    {
        running = false;
        savedTheta = theta; // сохраняем угол в момент остановки
        UpdateUIText();
    }

    public void ResetPendulum()
    {
        running = false;
        goalReached = false;
        elapsedTime = 0f;
        theta = savedTheta;
        omega = 0f;
        UpdatePendulumVisual();
        if (timerText != null)
            timerText.text = $"Время: 0.00 сек";
    }

    void Update()
    {
        if (!running || goalReached)
        {
            HandleKeyboardInput();
            return;
        }

        float a_i = -g / length * Mathf.Sin(theta);
        theta = theta + omega * dt + 0.5f * a_i * dt * dt;
        float a_i1 = -g / length * Mathf.Sin(theta);
        omega = omega + 0.5f * (a_i + a_i1) * dt;

        elapsedTime += dt;
        UpdatePendulumVisual();

        if (timerText != null)
            timerText.text = $"Время: {elapsedTime:F2} сек";

        HandleKeyboardInput(); // чтобы реагировать даже во время движения
    }

    void UpdatePendulumVisual()
    {
        float x = length * Mathf.Sin(theta);
        float y = -length * Mathf.Cos(theta);
        Vector3 bobPos = pivot.position + new Vector3(x, y, 0);
        if (pendulumLine != null)
        {
            pendulumLine.positionCount = 2;
            pendulumLine.SetPosition(0, pivot.position);
            pendulumLine.SetPosition(1, bobPos);
        }
        if (bob != null)
            bob.position = bobPos;

        if (angleSlider != null && running)
        {
            isSliderUpdating = true;
            float currentAngleDeg = Mathf.Abs(theta * Mathf.Rad2Deg);
            angleSlider.value = currentAngleDeg;
            if (angleText != null)
                angleText.text = $"Текущий угол: {currentAngleDeg:F1}°";
            isSliderUpdating = false;
        }
    }

    void HandleKeyboardInput()
    {
        float angleStep = 1f;
        float lengthStep = 0.1f;

        if (!running)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                startAngleDeg = Mathf.Clamp(startAngleDeg - angleStep, angleSlider.minValue, angleSlider.maxValue);
                savedTheta = -startAngleDeg * Mathf.Deg2Rad;
                if (angleSlider != null) angleSlider.value = startAngleDeg;
                ResetPendulum();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                startAngleDeg = Mathf.Clamp(startAngleDeg + angleStep, angleSlider.minValue, angleSlider.maxValue);
                savedTheta = -startAngleDeg * Mathf.Deg2Rad;
                if (angleSlider != null) angleSlider.value = startAngleDeg;
                ResetPendulum();
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                length = Mathf.Clamp(length + lengthStep, lengthSlider.minValue, lengthSlider.maxValue);
                if (lengthSlider != null) lengthSlider.value = length;
                UpdateUIText();
                UpdatePendulumVisual();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                length = Mathf.Clamp(length - lengthStep, lengthSlider.minValue, lengthSlider.maxValue);
                if (lengthSlider != null) lengthSlider.value = length;
                UpdateUIText();
                UpdatePendulumVisual();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartPendulum();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            StopPendulum();
        }
    }

    public void OnGoalReached()
    {
        running = false;
        goalReached = true;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
