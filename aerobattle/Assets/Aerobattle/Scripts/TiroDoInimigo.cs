using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroDoInimigo : MonoBehaviour
{
    public float velocidadeDoLaser;  // Velocidade do laser
    public int danoParaDar;          // Dano que o laser causará ao jogador

    public Transform jogador;       // Referência ao jogador

    void Start()
    {
        // Encontra o jogador na cena, caso ainda não tenha sido encontrado
        jogador = GameObject.FindGameObjectWithTag("Jogador")?.transform;
        
        if (jogador == null)
        {
            Debug.LogError("Jogador não encontrado! O laser não pode seguir o jogador.");
            Destroy(gameObject); // Destrói o laser caso o jogador não tenha sido encontrado
        }
    }

    void Update()
    {
        if (jogador != null)
        {
            // Move o laser em direção ao jogador
            MovimentarLaser();
        }
    }

    // Método que move o laser em direção ao jogador
    private void MovimentarLaser()
    {
        // Calcula a direção do laser em relação ao jogador
        Vector2 direcao = (jogador.position - transform.position).normalized;

        // Move o laser na direção do jogador
        transform.Translate(direcao * velocidadeDoLaser * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        // Se o laser colidir com o jogador, aplica dano
        if (colision.gameObject.CompareTag("Jogador"))
        {
            colision.gameObject.GetComponent<VidaDoJogador>().MachucarJogador(danoParaDar);
        }
    }
}