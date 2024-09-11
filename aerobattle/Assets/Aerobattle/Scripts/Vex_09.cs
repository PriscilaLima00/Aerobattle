using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vex_09 : MonoBehaviour
{
    public float velocidadeDoInimigo; // Velocidade do movimento vertical
    public float limiteSuperior; // Limite superior do movimento
    public float limiteInferior; // Limite inferior do movimento

    private float posicaoInicialY;
    private bool indoParaCima = true;
    private bool indoParaDireita = true;

    public Transform localDoDisparoDaDireita;
    public Transform localDoDisparoDaEsquerda;
    public GameObject laserDoInimigo;
    public float raioDeDeteccao;
    public float tempoEntreOsTiros;

    private Transform jogador;
    private float tempoParaOProximoTiro;
    private bool jogadorDetectado;

    public int vidaAtualDoVex;
    public int vidaMaximaDoVex;

    void Start()
    {
        // Guarda a posição inicial do inimigo
        posicaoInicialY = transform.position.y;
        tempoParaOProximoTiro = tempoEntreOsTiros;
        jogadorDetectado = false;

        // Encontra o jogador na cena
        jogador = GameObject.FindGameObjectWithTag("Jogador")?.transform;
        vidaAtualDoVex = vidaMaximaDoVex;
    }

    void Update()
    {
        MovimentoVertical();

        if (jogador != null)
        {
            // Verifica se o jogador está dentro do raio de detecção
            float distanciaParaJogador = Vector2.Distance(transform.position, jogador.position);
            if (distanciaParaJogador <= raioDeDeteccao)
            {
                jogadorDetectado = true;
                PerseguirJogador();
                Atirar();
                
            }
            else
            {
                jogadorDetectado = false;
            }
        }
    }

    private void MovimentoVertical()
    {
        // Calcula o movimento vertical baseado no tempo
        float novaPosicaoY = transform.position.y + (indoParaCima ? 1 : -1) * velocidadeDoInimigo * Time.deltaTime;

        // Verifica se o inimigo atingiu o limite superior ou inferior
        if (novaPosicaoY > posicaoInicialY + limiteSuperior)
        {
            novaPosicaoY = posicaoInicialY + limiteSuperior;
            indoParaCima = false; // Muda a direção para baixo
        }
        else if (novaPosicaoY < posicaoInicialY + limiteInferior)
        {
            novaPosicaoY = posicaoInicialY + limiteInferior;
            indoParaCima = true; // Muda a direção para cima
        }

        // Atualiza a posição do inimigo
        transform.position = new Vector2(transform.position.x, novaPosicaoY);
    }

    private void PerseguirJogador()
    {
        if (jogadorDetectado)
        {
            Vector2 direcaoParaJogador = (jogador.position - transform.position).normalized;
            transform.Translate(direcaoParaJogador * velocidadeDoInimigo * Time.deltaTime, Space.World);
        }
    }

    private void Atirar()
    {
        if (tempoParaOProximoTiro <= 0)
        {
            // Instancia o tiro na posição dos locais de disparo
            Instantiate(laserDoInimigo, localDoDisparoDaDireita.position, localDoDisparoDaDireita.rotation);
            Instantiate(laserDoInimigo, localDoDisparoDaEsquerda.position, localDoDisparoDaEsquerda.rotation);
            tempoParaOProximoTiro = tempoEntreOsTiros;
        }
        else
        {
            tempoParaOProximoTiro -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("LaserSimples"))
            {
                ReceberDanoVex(1); // O laser simples tira 1 vida
            }
            else if (col.CompareTag("LaserDuplo"))
            {
                ReceberDanoVex(2); // O laser duplo tira 2 vidas
            }
        }
    public void ReceberDanoVex(int dano)
    {
        vidaAtualDoVex -= dano;
        if (vidaAtualDoVex <= 0)
        {
            Morrer();
        }
    }
    private void Morrer()
    {
        // Código para destruir o inimigo
        Destroy(gameObject);
    }
    
    
}
