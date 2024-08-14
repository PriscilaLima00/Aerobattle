using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Moedas : MonoBehaviour
{
    public Text scoreText;
    private int score = 0; // Inicialize o score diretamente aqui
    
    void Start()
    {
        AtualizarPontuacao();
    }

    void Update()
    {
        AtualizarPontuacao();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Jogador"))
        {
            score += 1;
            AtualizarPontuacao();
            Destroy(gameObject);
        }
    }

    private void AtualizarPontuacao()
    {
        scoreText.text = score.ToString();
    }
}
