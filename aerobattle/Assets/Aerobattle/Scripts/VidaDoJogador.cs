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
    }

    public void MachucarJogador(int danoParaReceber)
    {
            if (!temEscudo)
            {
                vidaAtualDoJogador -= danoParaReceber;
                barraDeVidaDoJogador.value = vidaAtualDoJogador; // Atualiza a barra de vida

                if (an != null)
                {
                    an.SetBool("dano", true);
                }

                StartCoroutine(DesativarAnimacaoDeDano());

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

    private IEnumerator DesativarAnimacaoDeDano()
    {
        yield return new WaitForSeconds(0.2f);
        if (an != null)
        {
            an.SetBool("dano", false);
        }
    }

    void OnCollisionEnter2D(Collision2D colisao)
    { 
        if (colisao.gameObject.CompareTag("Meteoro"))
        {
            MachucarJogador(danoParaMeteoro); // Chama o método de machucar jogador

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

            Inimigo asteroide = colisao.gameObject.GetComponent<Inimigo>();
            if (asteroide != null)
            {
                asteroide.MachucarInimigo(danoParaInimigo);
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



    
    
   

