using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Vector3;

public class AsteroideP : MonoBehaviour
{
    public int velocidadeDoAsteroide;
    public float amplitude = 1.0f;
    public float velocidadeHorizontal = 2.0f;

    private float tempoInicial;
    
    
    void Start()
    {
        tempoInicial = Time.time; 
    }
    
    void Update()
    {
        MovimentoDoAsteroide();
    }

    private void MovimentoDoAsteroide()
    {
        float novaPosicaoY = Mathf.Sin((Time.time - tempoInicial) * velocidadeDoAsteroide) * amplitude;
        
        transform.position +=
            new Vector3(-velocidadeHorizontal * Time.deltaTime, novaPosicaoY - transform.position.y, 0);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroy"))
        {
            // Destroi a moeda
            Destroy(gameObject);
        }
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
            
        }
    }
    
}