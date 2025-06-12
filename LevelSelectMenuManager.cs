using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ����������� ��� Toggle!

public static class GameMode
{
    public static bool timedMode = false;
}

public class LevelSelectMenuManager : MonoBehaviour
{
    public Toggle timedModeToggle; // ��� ���� �������� � ����������

    public void LoadSampleScene()
    {
        GameMode.timedMode = timedModeToggle != null && timedModeToggle.isOn;
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadNormal()
    {
        GameMode.timedMode = timedModeToggle != null && timedModeToggle.isOn;
        SceneManager.LoadScene("Level_Normal");
    }
    public void LoadNormal_2()
    {
        GameMode.timedMode = timedModeToggle != null && timedModeToggle.isOn;
        SceneManager.LoadScene("Level_Normal_2");
    }
    public void LoadHard()
    {
        GameMode.timedMode = timedModeToggle != null && timedModeToggle.isOn;
        SceneManager.LoadScene("Level_Hard");
    }
    public void LoadHard_2()
    {
        GameMode.timedMode = timedModeToggle != null && timedModeToggle.isOn;
        SceneManager.LoadScene("Level_Hard_2");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
