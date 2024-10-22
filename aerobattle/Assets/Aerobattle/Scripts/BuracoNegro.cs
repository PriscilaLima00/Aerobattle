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
            // Ativa o painel de Game Over
            painelGameOver.SetActive(true);

            // Opcional: Pause o jogo
            Time.timeScale = 0; // Para pausar o jogo, se necessário
        }
    }

    
}
