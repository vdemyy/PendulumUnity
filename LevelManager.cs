using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1f;  // Возвращаем нормальную скорость игры
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Перезагружаем текущую сцену
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");  // Загружаем сцену меню
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