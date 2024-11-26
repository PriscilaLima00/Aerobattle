using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PausarJogo : MonoBehaviour
{
    public Transform pauseMenu;
    public Transform gameOverScreen;

// Start is called before the first frame update
    void Start()
    {

    }

// Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && !gameOverScreen.gameObject.activeSelf)
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        AudoiManager.instance.EntrarNoMenu();
        SceneManager.LoadScene("MainMenu");
        
        
    }
}