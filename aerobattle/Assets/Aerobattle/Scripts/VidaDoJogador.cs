using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    public int vidaMaximaDoJogador;

    public int vidaAtualDoJogador;

    public bool temEscudo;
    
    public int danoParaInimigos = 5; // Dano que o jogador aplica aos inimigos
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
    
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Inimigo"))
        {
            // Reduz a vida do jogador
            vidaAtualDoJogador -= danoParaInimigos;

            // Verifica se o jogador morreu
            if (vidaAtualDoJogador <= 0)
            {
                Morrer();
            }

            // Aplica dano ao inimigo
            Inimigo inimigo = colisao.gameObject.GetComponent<Inimigo>();
            if (inimigo != null)
            {
                inimigo.AplicaDano(danoParaInimigos);
            }
        }
    }
    
    void Morrer()
    {
        // Lógica para a morte do jogador
        Debug.Log("Jogador morreu!");
        // Você pode adicionar aqui a lógica para mostrar a tela de Game Over ou reiniciar o nível
        Destroy(gameObject); // Exclui o GameObject do jogador
    }
}
