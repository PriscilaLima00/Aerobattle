using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaDoJogador : MonoBehaviour
{
    public static VidaDoJogador Instance { get; private set; }

    public GameObject escudoDoJogador;
    public GameObject lanternaDoJogador;
    public Slider barraDeVidaDoJogador;
    public int vidaMaximaDoJogador;
    public int vidaAtualDoJogador;
    //public int vidaAtualDaLanterna;
    public int vidaMaximaDaLanterna;
    public int vidaMaximaDoEscudo;
    public int vidaAtualDoEscudo;

    public bool temEscudo;
    //public bool temLanterna;

    public int danoParaInimigos = 5; 
    public static int danoParaNebuloso = 10; 
    public int danoParaMeteoro = 3; 
    public int danoParaOAsteroide = 3;

    public GameManager gameOver;

    private void Awake()
    {
        // Implementa o padrão Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o objeto entre as cenas
        }
        else
        {
            Destroy(gameObject); // Garante que haja apenas uma instância
        }
    }

    void Start()
    {
        vidaAtualDoEscudo = vidaMaximaDoEscudo;
        vidaAtualDoJogador = vidaMaximaDoJogador;
        
        barraDeVidaDoJogador.maxValue = vidaMaximaDoJogador;
        barraDeVidaDoJogador.value = vidaAtualDoJogador;
        
        escudoDoJogador.SetActive(false);
        temEscudo = false;

        //vidaAtualDaLanterna = vidaMaximaDaLanterna;
        lanternaDoJogador.SetActive(false);
        //temLanterna = false;
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
    }
    
    public void AtivarLanterna()
    {
        //vidaAtualDaLanterna = vidaMaximaDaLanterna;
        lanternaDoJogador.SetActive(true);
        //temLanterna = true;
    }

    public void MachucarJogador(int danoParaReceber)
    {
        if (temEscudo == false)
        {
            vidaAtualDoJogador -= danoParaReceber;
            barraDeVidaDoJogador.value = vidaAtualDoJogador;

            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
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

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Inimigo"))
        {
            vidaAtualDoJogador -= danoParaInimigos;

            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
            }

            Inimigo inimigo = colisao.gameObject.GetComponent<Inimigo>();
            if (inimigo != null)
            {
                inimigo.AplicaDano(danoParaInimigos);
            }
        }
        else if (colisao.gameObject.CompareTag("Meteoro"))
        {
            vidaAtualDoJogador -= danoParaMeteoro;

            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
            }

            Meteoro meteoro = colisao.gameObject.GetComponent<Meteoro>();
            if (meteoro != null)
            {
                meteoro.MachucarMeteoro(danoParaMeteoro);
            }
        }
        else if (colisao.gameObject.CompareTag("Nebuloso"))
        {
            vidaAtualDoJogador -= danoParaNebuloso;

            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
            }

            Nebuloso nebuloso = colisao.gameObject.GetComponent<Nebuloso>();
            if (nebuloso != null)
            {
                nebuloso.AplicaDano(danoParaNebuloso);
            }
        }
        else if (colisao.gameObject.CompareTag("Asteroide P."))
        {
            vidaAtualDoJogador -= danoParaOAsteroide;

            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
            }

            AsteroideP asteroide = colisao.gameObject.GetComponent<AsteroideP>();
            if (asteroide != null)
            {
                asteroide.MachucarAsteroide(danoParaOAsteroide);
            }
        }
        else if (colisao.gameObject.CompareTag("Asteroide G."))
        {
            vidaAtualDoJogador -= danoParaOAsteroide;

            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
            }

            AsteroideG asteroide = colisao.gameObject.GetComponent<AsteroideG>();
            if (asteroide != null)
            {
                asteroide.MachucarAsteroideG(danoParaOAsteroide);
            }
        }
    }

    void Morrer()
    {
        // Adicione a lógica para mostrar a tela de Game Over ou reiniciar o nível
        if (gameOver != null)
        {
            gameOver.ShowGameOver();
        }
        Destroy(gameObject); // Exclui o GameObject do jogador
    }
}


    
    
   

