using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vitória : MonoBehaviour
{public GameObject victoryPanel;

    public void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0; // Pausa o jogo
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Reinicia o tempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1; // Reinicia o tempo
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
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
        Time.timeScale = 1; // Reinicia o tempo
        SceneManager.LoadScene("MainMenu"); // Verifique se "MainMenu" é o nome correto da sua cena
    }
}
