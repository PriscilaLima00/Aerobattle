using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    public int vidaMaximaDoJogador;

    public int vidaAtualDoJogador;

    public bool temEscudo;
    
    public int danoParaInimigos = 5; // Dano que o jogador aplica aos inimigos da fase 1
    public int danoParaNebuloso = 10;
    public int  danoParaMeteoro = 3;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoJogador = vidaMaximaDoJogador;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MachucarJogador(int danoParaReceber)
    {
        if(temEscudo == false)
        {
            vidaAtualDoJogador -= danoParaReceber;

            if (vidaAtualDoJogador <= 0)
            {
                Debug.Log("Game Over");
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
    }

    void Morrer()
    {
        Debug.Log("Jogador morreu!");
        // Adicione a lógica para mostrar a tela de Game Over ou reiniciar o nível
        Destroy(gameObject); // Exclui o GameObject do jogador
    }
}
   

