using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused;

    public GameObject uiMenu;
    public GameObject btnPause;
    public GameObject scoreText;

    private void Start()
    {
        scoreText.SetActive(false);
        PauseGame(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame(true);

                return;
            }
            else
            {
                PauseGame(false);
            }
        }
    }

    public void PauseGame(bool state)
    {
        if (state == true)
        {
            isPaused = true;

            uiMenu.SetActive(true);
            btnPause.SetActive(false);

            Time.timeScale = 0f;
        }
        else
        {
            isPaused = false;

            uiMenu.SetActive(false);
            btnPause.SetActive(true);

            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
