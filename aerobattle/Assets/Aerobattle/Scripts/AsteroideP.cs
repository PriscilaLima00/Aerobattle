using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Vector3;

public class AsteroideP : MonoBehaviour
{
    public int vidaAtualDoAsteroide;

    public int vidaMaximaDoAsteroide;

    public int velocidadeDoAsteroide;
    
    public float amplitude = 1.0f; // Amplitude do movimento vertical

    private float tempoInicial; // Tempo inicial para cálculo da oscilação
    
    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoAsteroide= vidaMaximaDoAsteroide;
        tempoInicial = Time.time; // Armazena o tempo inicial
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoDoAsteroide();
    }

    private void MovimentoDoAsteroide()
    {
        // Calcula a nova posição vertical usando uma oscilação senoidal
        float novaPosicaoY = Mathf.Sin((Time.time - tempoInicial) * velocidadeDoAsteroide) * amplitude;
        // Atualiza a posição do asteroide
        transform.position = new Vector3(transform.position.x, novaPosicaoY, transform.position.z);
    }
    
    public void MachucarAsteroide(int danoParaReceber)
    {
        vidaAtualDoAsteroide -= danoParaReceber;

        if (vidaAtualDoAsteroide <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    
    public void AplicaDano(int dano)
    {
        vidaAtualDoAsteroide -= dano;

        // Verifica se o inimigo morreu
        if (vidaAtualDoAsteroide <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Destroy(gameObject); // Exclui o GameObject do inimigo
    }
}
