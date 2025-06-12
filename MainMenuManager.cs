using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject aboutPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelectMenu"); // Название вашей сцены с маятником
    }
    public void ShowAbout()
    {
        SceneManager.LoadScene("AboutUs");
    }


    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}