using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuracoNegro : MonoBehaviour
{

    public GameObject painelGameOver;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Jogador"))
        {
            painelGameOver.SetActive(true);
            
            Time.timeScale = 0; 

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Jogador"))
        {
            Time.timeScale = 1;
        }
    }

}
