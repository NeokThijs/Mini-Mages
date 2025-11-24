using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    public void StartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void MainGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void EndScreen()
    {
        SceneManager.LoadScene("EndScreen");
    }

    public void CloseGame()
    {
        Application.Quit();
    }


    // andere / extra scenes
    public void MichaelScene()
    {
        SceneManager.LoadScene("MichaelTestScene");
    }
}
