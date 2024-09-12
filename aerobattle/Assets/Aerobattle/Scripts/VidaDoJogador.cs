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

    public int danoParaInimigos = 5; 
    public static int danoParaNebuloso = 10; 

    public int danoParaMeteoro = 3; 
    public int danoParaOAsteroide = 3; 

    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoEscudo = vidaMaximaDoEscudo;
        vidaAtualDoJogador = vidaMaximaDoJogador;
        
        barraDeVidaDoJogador.maxValue = vidaMaximaDoJogador;
        barraDeVidaDoJogador.value = vidaAtualDoJogador;
        
        escudoDoJogador.SetActive(false);
        temEscudo = false;

        
    }

    // Update is called once per frame
    void Update()
    {

        
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
        if (temEscudo == false)
        {
            vidaAtualDoJogador -= danoParaReceber;
            barraDeVidaDoJogador.value = vidaAtualDoJogador;

            if (vidaAtualDoJogador <= 0)
            {
                Debug.Log("Game Over");
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

    // Quando o jogador colide com outro objeto
    void OnCollisionEnter2D(Collision2D colisao)
    {
        // Verifica se o objeto colidido é um inimigo
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
        // Verifica se o objeto colidido é um meteoro
        else if (colisao.gameObject.CompareTag("Meteoro"))
        {
            vidaAtualDoJogador -= danoParaMeteoro;

            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
            }

            // Aplica dano ao meteoro, se necessário
            Meteoro meteoro = colisao.gameObject.GetComponent<Meteoro>();
            if (meteoro != null)
            {
                meteoro.MachucarMeteoro(danoParaMeteoro); // Ajuste conforme necessário
            }
        }

        // Verifica se o objeto colidido é o Nebuloso
        if (colisao.gameObject.CompareTag("Nebuloso"))
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

        // Verifica se o objeto colidiu com o asteroide PEQUENO
        if (colisao.gameObject.CompareTag("Asteroide P."))
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
        // Verifica se o objeto colidiu com o asteroide PEQUENO
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
        void Morrer()
        {
            Debug.Log("Jogador morreu!");
            // Adicione a lógica para mostrar a tela de Game Over ou reiniciar o nível
            Destroy(gameObject); // Exclui o GameObject do jogador
        }
    }
}


    
    
   

