using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public GameObject resetSavedTimesConfirmationPanel;

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

    public void OnClickLevelOneReplayButton()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void OnClickLevelTwoReplayButton()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void OnClickResetSavedTimesButton()
    {
        resetSavedTimesConfirmationPanel.SetActive(true);
    }

    public void OnClickResetSavedTimesYesButton()
    {
        for (int i = 0; i < 2; i++)
        {
            PlayerPrefs.DeleteKey("LatestTimeLevel" + i);
            PlayerPrefs.DeleteKey("BestTimeLevel" + i);
        }

        PlayerPrefs.Save();
    }

    public void OnClickResetSavedTimesNoButton()
    {
        resetSavedTimesConfirmationPanel.SetActive(false);
    }
}
