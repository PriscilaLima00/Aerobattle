using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaDoJogador : MonoBehaviour
{
    public GameObject escudoDoJogador;

    public Slider barraDeVidaDoJogador;
    public int vidaMaximaDoJogador;
    public int vidaAtualDoJogador;

    public int vidaMaximaDoEscudo;
    public int vidaAtualDoEscudo;

    public bool temEscudo;

    private Animator an;
    public int danoParaMeteoro = 3;
    public int danoParaOAsteroide = 3;
    public int danoParaInimigo = 2;

    public GameManager gameOver;

    public float duracaoEscudo = 5f; // Duração do escudo em segundos

    void Start()
    {
        an = GetComponent<Animator>();
        vidaAtualDoEscudo = vidaMaximaDoEscudo;
        vidaAtualDoJogador = vidaMaximaDoJogador;

        barraDeVidaDoJogador.maxValue = vidaMaximaDoJogador;
        barraDeVidaDoJogador.value = vidaAtualDoJogador;

        escudoDoJogador.SetActive(false);
        temEscudo = false;
    }

    void Update()
    {
        if (vidaAtualDoJogador <= 0)
        {
            Morrer();
        }
    }

    public void GanharVida(int vidaParaReceber)
    {
        if (vidaAtualDoJogador + vidaParaReceber <= vidaMaximaDoJogador)
        {
            vidaAtualDoJogador += vidaParaReceber;
        }
        else
        {
            vidaAtualDoJogador = vidaMaximaDoJogador;
        }

        barraDeVidaDoJogador.value = vidaAtualDoJogador;
    }

    public void AtivarEscudo()
    {
        vidaAtualDoEscudo = vidaMaximaDoEscudo;
        escudoDoJogador.SetActive(true);
        temEscudo = true;

        // Iniciar a corrotina para desativar o escudo após a duração
        StartCoroutine(DesativarEscudoAposTempo());
    }

    public void MachucarJogador(int danoParaReceber)
    {
        if (!temEscudo)
        {
            vidaAtualDoJogador -= danoParaReceber;
            barraDeVidaDoJogador.value = vidaAtualDoJogador;

            if (an != null)
            {
                an.SetBool("dano", true);
            }

            StartCoroutine(DesativarAnimacaoDeDano());

            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
                Time.timeScale = 0;
            }
        }
        else
        {
            vidaAtualDoEscudo -= danoParaReceber;

            if (vidaAtualDoEscudo <= 0)
            {
                escudoDoJogador.SetActive(false);
                temEscudo = false;
            }
        }
    }

    private IEnumerator DesativarAnimacaoDeDano()
    {
        yield return new WaitForSeconds(0.2f);
        if (an != null)
        {
            an.SetBool("dano", false);
        }
    }

    private IEnumerator DesativarEscudoAposTempo()
    {
        yield return new WaitForSeconds(duracaoEscudo);
        escudoDoJogador.SetActive(false);
        temEscudo = false;
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Meteoro"))
        {
            MachucarJogador(danoParaMeteoro);

            Meteoro meteoro = colisao.gameObject.GetComponent<Meteoro>();
            if (meteoro != null)
            {
                meteoro.MachucarMeteoro(danoParaMeteoro);
            }
        }
        else if (colisao.gameObject.CompareTag("Asteroide P."))
        {
            MachucarJogador(danoParaOAsteroide);
            AsteroideP asteroide = colisao.gameObject.GetComponent<AsteroideP>();
            if (asteroide != null)
            {
                asteroide.MachucarAsteroide(danoParaOAsteroide);
            }
        }
        else if (colisao.gameObject.CompareTag("Asteroide G."))
        {
            MachucarJogador(danoParaOAsteroide);

            AsteroideG asteroide = colisao.gameObject.GetComponent<AsteroideG>();
            if (asteroide != null)
            {
                asteroide.MachucarAsteroideG(danoParaOAsteroide);
            }
        }
        else if (colisao.gameObject.CompareTag("Inimigo"))
        {
            MachucarJogador(danoParaInimigo);

            Inimigo inimigo = colisao.gameObject.GetComponent<Inimigo>();
            if (inimigo != null)
            {
                inimigo.MachucarInimigo(danoParaInimigo);
            }
        }
    }

    void Morrer()
    {
        if (gameOver != null)
        {
            gameOver.ShowGameOver();
        }

        Destroy(gameObject);
    }

    public bool EstahVivo()
    {
        return vidaAtualDoJogador > 0;
    }
}