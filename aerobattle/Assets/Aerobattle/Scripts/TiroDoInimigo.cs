using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroDoInimigo : MonoBehaviour
{   
    public float velocidadeDoLaser; // Velocidade do laser
    public int danoParaDar;         // Dano que o laser causará ao jogador
    public float raioDeDeteccao = 10f; // Distância do raio de detecção
    public LayerMask layerDeColisao;  // Camada do jogador para o Raycast

    private Transform jogador;      // Referência ao jogador

    void Start()
    {
        // Encontra o jogador na cena
        jogador = GameObject.FindGameObjectWithTag("Jogador")?.transform;
    }

    void Update()
    {
        if (jogador != null)
        {
            // Move o laser em direção ao jogador
            MoverLaserEmDirecaoAoJogador(jogador.position);
            
            // Detecta colisões com o jogador usando Raycast
            DetectarColisaoComJogador();
        }
        else
        {
            // Caso o jogador não seja encontrado, destrua o laser
            Destroy(gameObject);
        }
    }

    // Método que move o laser em direção ao jogador
    public void MoverLaserEmDirecaoAoJogador(Vector2 jogadorPosicao)
    {
        // Calcula a direção do laser em relação ao jogador
        Vector2 direcao = (jogadorPosicao - (Vector2)transform.position).normalized;

        // Move o laser em direção ao jogador
        transform.Translate(direcao * velocidadeDoLaser * Time.deltaTime);
    }

    // Detecta colisão com o jogador usando Raycast
    private void DetectarColisaoComJogador()
    {
        // Lança um Raycast em direção ao jogador
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (jogador.position - transform.position).normalized, raioDeDeteccao, layerDeColisao);
        
        if (hit.collider != null)
        {
            // Verifica se o Raycast acertou o jogador
            if (hit.collider.CompareTag("Jogador"))
            {
                // Aplica dano no jogador
                hit.collider.gameObject.GetComponent<VidaDoJogador>().MachucarJogador(danoParaDar);
                
                // Destrói o laser após atingir o jogador
                Destroy(gameObject);
            }
        }
    }

    // Opcional: Destruição do laser caso ele saia da tela
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
