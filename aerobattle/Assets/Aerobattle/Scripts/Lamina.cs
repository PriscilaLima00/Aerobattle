using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamina : MonoBehaviour
{
    private Animator animator;
    public int danoParaDar = 2;
    private bool jogadorNoCollider = false;
    private GameObject jogador;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Método para parar a animação
    private void StopAnimation()
    {
        if (animator != null)
        {
            animator.enabled = false; // Desativa o Animator para parar a animação
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            if (!jogadorNoCollider)
            {
                jogadorNoCollider = true;
                jogador = collision.gameObject;
                // Aplica o dano ao jogador
                VidaDoJogador playerHealth = jogador.GetComponent<VidaDoJogador>();
                if (playerHealth != null)
                {
                    playerHealth.MachucarJogador(danoParaDar);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            jogadorNoCollider = false;
            jogador = null;
        }
    }
}