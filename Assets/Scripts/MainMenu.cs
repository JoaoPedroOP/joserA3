using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        ChangeToScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void GoToMenu()
    {
        ChangeToScene("Menu");
    }
    private void ChangeToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
