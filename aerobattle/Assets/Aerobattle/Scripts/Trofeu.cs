using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trofeu : MonoBehaviour
{
    public Vitória victoryScreenController; // Referência ao controlador da tela de vitória

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            Debug.Log("Jogador colidiu com o troféu");
            AdvanceToVictoryScreen();
            Destroy(gameObject); // Destrói o troféu após a coleta
        }
    }

    private void AdvanceToVictoryScreen()
    {
        if (victoryScreenController != null)
        {
            victoryScreenController.ShowVictoryScreen();
        }
        else
        {
            Debug.LogError("VictoryScreenController não está atribuído!");
        }
    }
}