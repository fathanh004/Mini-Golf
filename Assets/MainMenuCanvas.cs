using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject levelSelectorPanel;
    [SerializeField] GameObject optionPanel;

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void Play()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        mainMenuPanel.SetActive(false);
        levelSelectorPanel.SetActive(true);
    }

    public void Option()
    {
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void Back()
    {
        mainMenuPanel.SetActive(true);
        levelSelectorPanel.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
