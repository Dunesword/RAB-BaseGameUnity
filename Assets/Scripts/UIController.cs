using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

	public void OnClickQuitButton()
    {
        print("Quit button was clicked");
        Application.Quit();
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void OnClickControlsButton()
    {
        SceneManager.LoadScene("HELP");
    }

    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
