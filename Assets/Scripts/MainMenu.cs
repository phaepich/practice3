using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("NEW GAME(sasha)");
    }

    public void QuitGame()
    {
        Debug.Log("QUITTING");
        Application.Quit();
    }
}
