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

    // Método chamado pelo script DeactivateBox para parar a animação
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
            jogadorNoCollider = true;
            jogador = collision.gameObject;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (jogadorNoCollider && collision.gameObject.CompareTag("Jogador"))
        {
            VidaDoJogador playerHealth = jogador.GetComponent<VidaDoJogador>();
            if (playerHealth != null)
            {
                playerHealth.MachucarJogador(danoParaDar);
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