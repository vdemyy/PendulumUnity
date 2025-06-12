using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = 40f;
    public TextMeshProUGUI timerText;
    public GameObject loseText;
    public GameObject restartButton;

    private float timeLeft;
    private bool timerActive = false;

    void Start()
    {
        if (GameMode.timedMode)
        {
            timeLeft = timeLimit;
            timerActive = true;
            if (timerText != null)
                timerText.gameObject.SetActive(true);
        }
        else
        {
            if (timerText != null)
                timerText.gameObject.SetActive(false);
        }
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    void Update()
    {
        if (timerActive)
        {
            timeLeft -= Time.deltaTime;
            if (timerText != null)
                timerText.text = $"Время: {Mathf.Ceil(timeLeft)}";

            if (timeLeft <= 0)
            {
                timerActive = false;
                if (loseText != null) loseText.SetActive(true);
                if (restartButton != null) restartButton.SetActive(true);
                var pendulum = FindObjectOfType<PendulumSimple>();
                if (pendulum != null) pendulum.StopPendulum();
            }
        }
    }
}
