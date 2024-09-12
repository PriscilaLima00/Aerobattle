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

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Guarda a posição inicial do inimigo
        posicaoInicialY = transform.position.y;
        tempoParaOProximoTiro = tempoEntreOsTiros;
        jogadorDetectado = false;

        // Encontra o jogador na cena
        jogador = GameObject.FindGameObjectWithTag("Jogador")?.transform;
        vidaAtualDoVex = vidaMaximaDoVex;

        // Obtém o SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no Vex_09.");
        }
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
                AtirarSeNecessario();
                AjusteParaJogador();
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

    private void AtirarSeNecessario()
    {
        if (tempoParaOProximoTiro <= 0)
        {
            AtirarLaser(localDoDisparoDaDireita);
            AtirarLaser(localDoDisparoDaEsquerda);
            tempoParaOProximoTiro = tempoEntreOsTiros;
        }
        else
        {
            tempoParaOProximoTiro -= Time.deltaTime;
        }
    }

    private void AtirarLaser(Transform localDoDisparo)
    {
        if (laserDoInimigo != null && localDoDisparo != null)
        {
            GameObject tiro = Instantiate(laserDoInimigo, localDoDisparo.position, localDoDisparo.rotation);

            // Calcula a direção do tiro
            Vector2 direcao = (jogador.position - transform.position).normalized;
            float angulo = Mathf.Rad2Deg * Mathf.Atan2(direcao.y, direcao.x);

            // Ajusta a rotação do tiro
            tiro.transform.eulerAngles = new Vector3(0, 0, angulo);

            // Ajusta a orientação do tiro com base na orientação do Vex
            AjusteParaLaser(tiro);
        }
    }

    private void AjusteParaJogador()
    {
        if (jogador != null)
        {
            // Calcula a direção para o jogador
            Vector2 direcaoParaJogador = jogador.position - transform.position;

            // Verifica se o jogador está à direita ou à esquerda
            spriteRenderer.flipX = direcaoParaJogador.x > 0;
        }
    }

    private void AjusteParaLaser(GameObject tiro)
    {
        // Ajusta a orientação do laser com base na orientação do Vex
        SpriteRenderer tiroSpriteRenderer = tiro.GetComponent<SpriteRenderer>();
        if (tiroSpriteRenderer != null)
        {
            tiroSpriteRenderer.flipX = spriteRenderer.flipX;
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
        Destroy(gameObject);
    }
}
