using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioInfinito : MonoBehaviour
{
    public float velocidadeDoCenario;
    private bool direcaoParaDireita = true;

    // Adicione um array de Texturas para as diferentes fases
    public Texture2D[] texturasFases;
    private int faseAtual = 0; // Indica a fase atual

    void Start()
    {
        direcaoParaDireita = true;
        // Define a textura inicial
        GetComponent<Renderer>().material.mainTexture = texturasFases[faseAtual];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direcaoParaDireita = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direcaoParaDireita = true;
        }

      

        MovimentarCenario();
    }

    private void MovimentarCenario()
    {
        float velocidadeAtual = direcaoParaDireita ? velocidadeDoCenario : -velocidadeDoCenario;

        Vector2 deslocamento = new Vector2(Time.time * velocidadeAtual, 0);
        GetComponent<Renderer>().material.mainTextureOffset = deslocamento;
    }

    private void MudarFase()
    {
        // Muda para a próxima fase e redefine a textura
        faseAtual++;
        if (faseAtual >= texturasFases.Length)
        {
            faseAtual = 0; // Volta para a primeira fase se passar do limite
        }

        GetComponent<Renderer>().material.mainTexture = texturasFases[faseAtual];
    }
}