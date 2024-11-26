using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Vector3;

public class AsteroideP : MonoBehaviour
{
    public int vidaAtualDoAsteroide;
    public int vidaMaximaDoAsteroide;
    public int velocidadeDoAsteroide;
    public float amplitude = 1.0f;
    public float velocidadeHorizontal = 2.0f;

    private float tempoInicial;
    public GameObject efeitoDeExplosão;
    
    void Start()
    {
        vidaAtualDoAsteroide = vidaMaximaDoAsteroide;
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
    
    public void MachucarAsteroide(int danoParaReceber)
    {
        vidaAtualDoAsteroide -= danoParaReceber;

        if (vidaAtualDoAsteroide <= 0)
        {
            Instantiate(efeitoDeExplosão, transform.position, transform.rotation);
            EfeitoSonoro.instance.somDaExplosão.Play();
            Destroy(this.gameObject);
        }
    }


    public void AplicaDano(int dano)
    {
        vidaAtualDoAsteroide -= dano;
        
        if (vidaAtualDoAsteroide <= 0)
        {
            Morrer();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroy"))
        {
            // Destroi a moeda
            Destroy(gameObject);
        }
    }


    void Morrer()
    {
        Destroy(gameObject); 
    }
}