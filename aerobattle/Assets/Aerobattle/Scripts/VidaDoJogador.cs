using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    public GameObject escudoDoJogador;
    
    public int vidaMaximaDoJogador;
    public int vidaAtualDoJogador;
    public int vidaMaximaDoEscudo;
    public int vidaAtualDoEscudo;

    public bool temEscudo;

    public int danoParaInimigos = 5; // Dano que o jogador aplica aos inimigos da fase 1
    public static int danoParaNebuloso = 10; // Dano que o jogador aplica aos inimigos da fase 2

    public int danoParaMeteoro = 3; // dano que o jogador leva por ser atinguido pelo meteoro
    public int danoParaOAsteroide = 3; // dano que o jogador leva por ser atinguido pelo meteoro

    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoJogador = vidaMaximaDoJogador;
        
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

        //barraDeVidaDoJogador.valeu = vidaAtualDoJogador;
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

        void Morrer()
        {
            Debug.Log("Jogador morreu!");
            // Adicione a lógica para mostrar a tela de Game Over ou reiniciar o nível
            Destroy(gameObject); // Exclui o GameObject do jogador
        }
    }
}
    
    
   

