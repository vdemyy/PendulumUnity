using UnityEngine;

public class BobCollision : MonoBehaviour
{
    public GameObject winText;
    public GameObject loseText;
    public GameObject restartButton;
    private PendulumSimple pendulum;

    void Start()
    {
        pendulum = FindFirstObjectByType<PendulumSimple>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger with: " + other.tag);

        if (other.CompareTag("Goal"))
        {
            Debug.Log("Goal reached!");
            if (winText != null) winText.SetActive(true);
            if (restartButton != null) restartButton.SetActive(true);
            if (pendulum != null) pendulum.StopPendulum();

            LevelTimer timer = FindObjectOfType<LevelTimer>();
            if (timer != null) timer.StopTimer();

        }
        else if (other.CompareTag("Obstacle")) // Добавляем проверку на препятствие
        {
            Debug.Log("Hit obstacle!");
            if (loseText != null) loseText.SetActive(true);
            if (restartButton != null) restartButton.SetActive(true);
            if (pendulum != null) pendulum.StopPendulum();

            LevelTimer timer = FindObjectOfType<LevelTimer>();
            if (timer != null) timer.StopTimer();


        }
    }
}