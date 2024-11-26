using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel; 
    public Button restartButton;      
    
    void Start()
    {
       
        gameOverPanel.SetActive(false);

        
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        
    }

    
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    
    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        AudoiManager.instance.JogadorMorreu();
        Time.timeScale = 1;
    }

    
}