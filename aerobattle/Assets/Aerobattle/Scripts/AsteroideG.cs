using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroideG : MonoBehaviour
{
    public int velocidadeDoAsteroide;

    void Start()
    {
    }

    void Update()
    {
        MovimentoDoAsteroideG();
    }

    private void MovimentoDoAsteroideG()
    {
        // Move o asteroide para a esquerda
        transform.Translate(Vector3.left * velocidadeDoAsteroide * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador"))
        {
           
            VidaDoJogador vidaDoJogador = colisao.gameObject.GetComponent<VidaDoJogador>();
            
            if (vidaDoJogador != null)
            {
                vidaDoJogador.MachucarJogador(1);
            }
            
            //Destroy(gameObject);
        }
    }
}