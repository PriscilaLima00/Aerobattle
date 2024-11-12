using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vex_09 : MonoBehaviour
{
    public float velocidadeDoInimigo;
    public int velocidadeHorizontal;
    public float limiteSuperior;
    public float limiteInferior;

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
    private bool movimentoVerticalAtivado = false;

    public int vidaAtualDoVex;
    public int vidaMaximaDoVex;

    private SpriteRenderer spriteRenderer;

    public GameObject itemParaDropar;
    public int chanceDeDropar;

    void Start()
    {
        posicaoInicialY = transform.position.y;
        tempoParaOProximoTiro = tempoEntreOsTiros;
        jogadorDetectado = false;

        jogador = GameObject.FindGameObjectWithTag("Jogador")?.transform;
        vidaAtualDoVex = vidaMaximaDoVex;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no Vex_09.");
        }
    }

    void Update()
    {
        VerificarJogador();

        if (movimentoVerticalAtivado)
        {
            MovimentoVertical();
        }
        else
        {
            MovimentoHorizontal();
        }

        AtirarSeNecessario();
    }

    private void MovimentoHorizontal()
    {
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
        if (jogador != null && !movimentoVerticalAtivado)
        {
            float distanciaParaJogador = Vector3.Distance(transform.position, jogador.position);
            jogadorDetectado = distanciaParaJogador <= raioDeDeteccao;

            if (jogadorDetectado)
            {
                movimentoVerticalAtivado = true;
            }
        }
    }

    private void AtirarSeNecessario()
    {
        if (jogadorDetectado &&
            movimentoVerticalAtivado) // Verifica se o jogador está no raio de detecção e o movimento vertical está ativado
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
    }

    private void AtirarLaser(Transform localDoDisparo)
    {
        if (laserDoInimigo != null && localDoDisparo != null)
        {
            GameObject tiro = Instantiate(laserDoInimigo, localDoDisparo.position, localDoDisparo.rotation);

            AjusteParaLaser(tiro);
        }
    }

    private void AjusteParaLaser(GameObject tiro)
    {
        SpriteRenderer tiroSpriteRenderer = tiro.GetComponent<SpriteRenderer>();
        if (tiroSpriteRenderer != null)
        {
            tiroSpriteRenderer.flipX = spriteRenderer.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Jogador"))
        {
            movimentoVerticalAtivado = true;
        }

        if (col.CompareTag("Laser"))
        {
            ReceberDanoVex(1);
        }
        else if (col.CompareTag("Laser Duplo"))
        {
            ReceberDanoVex(2);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Jogador"))
        {
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