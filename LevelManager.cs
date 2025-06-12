using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1f;  // ���������� ���������� �������� ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // ������������� ������� �����
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");  // ��������� ����� ����
    }

    public void LoadEasy()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_Easy");
    }

    public void LoadNormal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_Normal");
    }

    public void LoadHard()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_Hard");
    }
}