using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool isPause = false;
    public GameObject PauseMenu;

    public static void GoToLevel(int id)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + id;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public static void GoToMain()
    {
        SceneManager.LoadScene(0);
    }

    public static void RestartCurrentLevel()
    {

    }

    public void DisplayPauseMenu(bool i_Display)
    {
        PauseMenu.SetActive(i_Display);
    }

}
