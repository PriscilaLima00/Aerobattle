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
    public float velocidadeHorizontal = 2.0f; // Velocidade do movimento para trás

    private float tempoInicial; // Tempo inicial para cálculo da oscilação

// Start is called before the first frame update
    void Start()
    {
        vidaAtualDoAsteroide = vidaMaximaDoAsteroide;
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

        // Atualiza a posição do asteroide (movimento horizontal para trás)
        transform.position +=
            new Vector3(-velocidadeHorizontal * Time.deltaTime, novaPosicaoY - transform.position.y, 0);
    }

// Método para receber dano
    public void MachucarAsteroide(int danoParaReceber)
    {
        vidaAtualDoAsteroide -= danoParaReceber;

        if (vidaAtualDoAsteroide <= 0)
        {
            Destroy(this.gameObject);
        }
    }

// Método para aplicar dano
    public void AplicaDano(int dano)
    {
        vidaAtualDoAsteroide -= dano;

        // Verifica se o asteroide morreu
        if (vidaAtualDoAsteroide <= 0)
        {
            Morrer();
        }
    }

// Método para lidar com a morte do asteroide
    void Morrer()
    {
        Destroy(gameObject); // Exclui o GameObject do asteroide
    }
}