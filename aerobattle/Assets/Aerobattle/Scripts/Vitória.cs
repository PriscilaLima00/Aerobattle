using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vitória : MonoBehaviour
{
    public GameObject victoryPanel;

    public void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1; 
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log(""+SceneManager.GetActiveScene());
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        AudoiManager.instance.EntrarNoMenu();
        SceneManager.LoadScene("MainMenu"); 
    }
}
