using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vex_09 : MonoBehaviour
{
    public float velocidadeDoInimigo; // Velocidade vertical
    public int velocidadeHorizontal;  // Velocidade horizontal
    public float limiteSuperior;      // Limite superior do movimento
    public float limiteInferior;      // Limite inferior do movimento

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
    private bool movimentoVerticalAtivado = false; // Controle para saber se o movimento vertical foi ativado

    public int vidaAtualDoVex;
    public int vidaMaximaDoVex;

    private SpriteRenderer spriteRenderer;

    public GameObject itemParaDropar;
    public int chanceDeDropar;

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
        // Verifica se o jogador está dentro do raio de detecção
        VerificarJogador();

        // Se o movimento vertical foi ativado, o inimigo se moverá verticalmente
        if (movimentoVerticalAtivado)
        {
            MovimentoVertical();
        }
        else
        {
            // Continua o movimento horizontal do inimigo
            MovimentoHorizontal();
        }

        // Atira se necessário, independentemente da detecção do jogador
        AtirarSeNecessario();
    }

    private void MovimentoHorizontal()
    {
        // Movimento horizontal do inimigo (sempre indo para a esquerda)
        transform.Translate(Vector3.left * velocidadeHorizontal * Time.deltaTime);
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

    private void VerificarJogador()
    {
        // Verifica se o jogador está dentro do raio de detecção
        if (jogador != null && !movimentoVerticalAtivado) // Só verifica se o jogador não está sendo perseguido
        {
            float distanciaParaJogador = Vector3.Distance(transform.position, jogador.position);
            jogadorDetectado = distanciaParaJogador <= raioDeDeteccao;

            if (jogadorDetectado)
            {
                movimentoVerticalAtivado = true; // Ativa o movimento vertical
            }
        }
    }

    private void AtirarSeNecessario()
    {
        if (tempoParaOProximoTiro <= 0)
        {
            // Atira para ambos os lados (direita e esquerda)
            AtirarLaser(localDoDisparoDaDireita);
            AtirarLaser(localDoDisparoDaEsquerda);
            tempoParaOProximoTiro = tempoEntreOsTiros; // Reseta o tempo entre os tiros
        }
        else
        {
            tempoParaOProximoTiro -= Time.deltaTime; // Reduz o tempo para o próximo tiro
        }
    }

    private void AtirarLaser(Transform localDoDisparo)
    {
        if (laserDoInimigo != null && localDoDisparo != null)
        {
            // Instancia o laser na posição do local de disparo, mantendo a rotação do local
            GameObject tiro = Instantiate(laserDoInimigo, localDoDisparo.position, localDoDisparo.rotation);

            // Ajuste do laser se necessário
            AjusteParaLaser(tiro);
        }
    }

    private void AjusteParaLaser(GameObject tiro)
    {
        // Ajusta a orientação do laser com base no flipX do Vex
        SpriteRenderer tiroSpriteRenderer = tiro.GetComponent<SpriteRenderer>();
        if (tiroSpriteRenderer != null)
        {
            // O flipX do laser deve ser igual ao flipX da nave
            tiroSpriteRenderer.flipX = spriteRenderer.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Jogador"))
        {
            // Quando o jogador entra no colisor, ativa o movimento vertical e interrompe o movimento horizontal
            movimentoVerticalAtivado = true;
        }

        if (col.CompareTag("Laser"))
        {
            ReceberDanoVex(1); // O laser simples tira 1 vida
        }
        else if (col.CompareTag("Laser Duplo"))
        {
            ReceberDanoVex(2); // O laser duplo tira 2 vidas
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Jogador"))
        {
            // Quando o jogador sai do colisor, desativa o movimento vertical e retoma o movimento horizontal
            movimentoVerticalAtivado = false;
        }
    }

    public void ReceberDanoVex(int dano)
    {
        vidaAtualDoVex -= dano;
        if (vidaAtualDoVex <= 0)
        {
            int numeroAleatorio = Random.Range(0, 100);
            if (numeroAleatorio <= chanceDeDropar)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }

            Destroy(this.gameObject);
        }
    }
}