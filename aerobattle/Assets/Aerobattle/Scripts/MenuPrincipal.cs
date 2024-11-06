using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public string nomeDoLevelDeJogo;

    public GameObject painelMenuPrincipal;

    public GameObject painelControle;


    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
        
    }

    public void AbrirControle()
    {
        painelMenuPrincipal.SetActive(false);
        painelControle.SetActive(true);
    }

    public void FecharControle()
    {
        painelControle.SetActive(false);
        painelMenuPrincipal.SetActive(true);
    }

    public void SairDoJogo()
    {
        Debug.Log("saiu");
        Application.Quit();
    }
}
