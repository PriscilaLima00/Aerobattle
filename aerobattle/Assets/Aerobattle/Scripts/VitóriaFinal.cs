using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VitóriaFinal : MonoBehaviour
{
    
    public GameObject telaDeVitoria;
    public string sceneToLoad = "MainMenu";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            Debug.Log("Jogador colidiu com o troféu!");
            MostrarVitoria(); 
            Destroy(gameObject);
        }
    }
    
    public void MostrarVitoria()
    {
        if (telaDeVitoria != null)
        {
            telaDeVitoria.SetActive(true); 
        }
        else
        {
            Debug.LogError("Tela de Vitória não está atribuída!");
        }
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1;  
        AudoiManager.instance.EntrarNoMenu();
        SceneManager.LoadScene(sceneToLoad); 
    }

}
