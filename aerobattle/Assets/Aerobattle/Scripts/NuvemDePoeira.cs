using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuvemDePoeira : MonoBehaviour
{
    public int danoParaCausar = 1; 
    public float tempoDeDano = 1f; 
    private float tempoDesdeUltimoDano = 0.1f;

    void Update()
    {
        // Atualiza o tempo desde o último dano
        tempoDesdeUltimoDano += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Jogador") && tempoDesdeUltimoDano >= tempoDeDano)
        {
            VidaDoJogador jogador = collision.GetComponent<VidaDoJogador>();
            if (jogador != null)
            {
                jogador.MachucarJogador(danoParaCausar); // Passa o dano que a nuvem causará
            }
            tempoDesdeUltimoDano = 0f; // Reseta o tempo desde o último dano
        }
    }
}
