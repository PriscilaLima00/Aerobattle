using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vex_09 : MonoBehaviour
{
   public float velocidadeDoInimigo;
    public int velocidadeHorizontal;
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
        if (jogador != null)
        {
            // Verifica se o jogador está dentro do raio de detecção
            float distanciaParaJogador = Vector2.Distance(transform.position, jogador.position);
            if (distanciaParaJogador <= raioDeDeteccao)
            {
                jogadorDetectado = true;
                AtirarSeNecessario(); // Atira se o jogador foi detectado
                PararMovimento(); // Impede o movimento do Vex
            }
            else
            {
                jogadorDetectado = false;
                MovimentoVertical(); // Movimenta o Vex verticalmente
                MovimentoHorizontal(); // Movimenta o Vex horizontalmente
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

    private void MovimentoHorizontal()
    {
        transform.Translate(Vector3.left * velocidadeHorizontal * Time.deltaTime);
    }

    private void PararMovimento()
    {
        // Impede o Vex de se mover quando o jogador está detectado
        velocidadeHorizontal = 0; // Impede o movimento horizontal
        velocidadeDoInimigo = 0;  // Impede o movimento vertical
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
            // Instancia o laser na posição do local de disparo, mantendo a rotação do local
            GameObject tiro = Instantiate(laserDoInimigo, localDoDisparo.position, localDoDisparo.rotation);

            // Cria um script ou lógica para mover o laser em direção ao jogador
            TiroDoInimigo laserScript = tiro.GetComponent<TiroDoInimigo>(); // Use o nome correto da classe
            if (laserScript != null)
            {
                laserScript.MoverLaserEmDirecaoAoJogador(jogador.position); // Chama o método correto
            }

            // Ajusta o flipX do laser com base na orientação do Vex (nave)
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
        if (col.CompareTag("Laser"))
        {
            ReceberDanoVex(1); // O laser simples tira 1 vida
        }
        else if (col.CompareTag("Laser Duplo"))
        {
            ReceberDanoVex(2); // O laser duplo tira 2 vidas
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