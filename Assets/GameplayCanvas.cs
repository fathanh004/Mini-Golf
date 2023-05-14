using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject optionMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void Option()
    {
        optionMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Back()
    {
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
