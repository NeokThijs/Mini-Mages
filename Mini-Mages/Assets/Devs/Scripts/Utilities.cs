using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager GameManager;
    public GameObject creditImage;
    private void Start()
    {
        if (gameManager != null)
        {
            GameManager = gameManager.GetComponent<GameManager>();
        }
    }
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

    public void StartAnotherRound()
    {
        GameManager.StartNewRound();
    }


    // andere / extra scenes
    public void MichaelScene()
    {
        SceneManager.LoadScene("MichaelTestScene");
    }
    public void Credits()
    {
        creditImage.SetActive(!creditImage.activeInHierarchy);
    }
}
