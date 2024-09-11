using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missel : MonoBehaviour
{
    public float velocidadeMissel = 10f; // Velocidade do míssil
    public int danoMissel = 10; // Dano que o míssil causa ao jogador

    private Transform jogador; // Referência ao jogador

    void Start()
    {
        // Encontra o jogador na cena
        jogador = GameObject.FindGameObjectWithTag("Jogador")?.transform;

        // Se não encontrar o jogador, destrói o míssil
        if (jogador == null)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (jogador != null)
        {
            // Move o míssil em direção ao jogador
            Vector2 direcaoParaJogador = (jogador.position - transform.position).normalized;
            transform.Translate(direcaoParaJogador * velocidadeMissel * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Verifica se o míssil colidiu com o jogador
        if (col.CompareTag("Jogador"))
        {
            // Envia dano ao jogador
            VidaDoJogador jogadorScript = col.GetComponent<VidaDoJogador>();
            if (jogadorScript != null)
            {
                jogadorScript.MachucarJogador(danoMissel);
            }
            
            // Destrói o míssil após colidir
            Destroy(gameObject);
        }
        else if (col.CompareTag("Ambiente")) // Por exemplo, se o míssil colidir com o ambiente
        {
            // Destrói o míssil ao colidir com o ambiente
            Destroy(gameObject);
        }
    }
}