using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
