using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioInfinito : MonoBehaviour
{
    public float velocidadeDoCenario;
    private bool direcaoParaDireita = true; // Variável para controlar a direção

    // Start is called before the first frame update
    void Start()
    {
        // Iniciar com a direção para a direita
        direcaoParaDireita = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Exemplo de como detectar a virada do jogador
        // Você pode ajustar isso de acordo com como você determina a direção do jogador
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direcaoParaDireita = false; // Jogador virou para a esquerda
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direcaoParaDireita = true; // Jogador virou para a direita
        }

        MovimentarCenario();  
    }

    private void MovimentarCenario()
    {
        // Determina a velocidade com base na direção
        float velocidadeAtual = direcaoParaDireita ? velocidadeDoCenario : -velocidadeDoCenario;
        
        Vector2 deslocamento = new Vector2(Time.time * velocidadeAtual, 0);
        GetComponent<Renderer>().material.mainTextureOffset = deslocamento;
    }
}