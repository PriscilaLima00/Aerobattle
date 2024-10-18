using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamina : MonoBehaviour
{
    private Animator animator;
    public int danoParaDar = 2;
    private bool jogadorNoCollider = false;
    private GameObject jogador;
    public int velocidade = 2;
    private Rigidbody2D rig;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Mv();
    }
    
    //Método para parar a animação
    private void StopAnimation()
    {
        if (animator != null)
        {
            animator.enabled = false; // Desativa o Animator para parar a animação
            Destroy(gameObject);
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

    private void Mv()
    {
        // Movimenta a lâmina para trás usando Rigidbody2D
        Vector2 movimento = Vector2.left * velocidade * Time.deltaTime;
        rig.MovePosition(rig.position + movimento);

        // Rotaciona a lâmina
        transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
    }
}