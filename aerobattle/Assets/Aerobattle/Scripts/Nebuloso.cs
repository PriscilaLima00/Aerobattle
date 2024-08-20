using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebuloso : MonoBehaviour
{
    public GameObject laserDoNebuloso;

    public Transform locaDoDisparo;

    public float velocidadeDoNebuloso;
    public float velocidadeFrontal;
    public float tempoMaximoEntreOsLasers;
    
    public float tempoAtualDosLasers;

    public bool inimigoAtirador = true;

    public int vidaMaximaDoInimigo;
    public int vidaAtualDoInimigo;
    
    public float limiteVertical;  // Limite vertical de movimentação do inimigo
    private bool jogadorDentroRaio = false;   // Flag para verificar se o jogador está dentro do raio
    private float inicioVertical;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoInimigo = vidaMaximaDoInimigo;
        tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        inicioVertical = transform.position.y;// Armazena a posição inicial vertical do inimigo
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarNebuloso();
        // Atualiza o tempo para o próximo tiro
        if (inimigoAtirador && jogadorDentroRaio)
        {
            tempoAtualDosLasers -= Time.deltaTime;

            if (tempoAtualDosLasers <= 0)
            {
                AtirarLaser();
                tempoAtualDosLasers = tempoMaximoEntreOsLasers; // Reinicia o tempo dos lasers
            }
        }
    }

    private void MovimentarNebuloso()
    {
        // Movimenta o inimigo para cima e para baixo
        float novaPosicaoY = Mathf.PingPong(Time.time * velocidadeDoNebuloso, limiteVertical * 2) - limiteVertical;
        transform.position = new Vector3(transform.position.x, novaPosicaoY, transform.position.z);
        
        // Movimento contínuo para frente
        transform.Translate(Vector3.left * velocidadeFrontal * Time.deltaTime);
    }

    private void AtirarLaser()
    {
        if (locaDoDisparo != null)
        {
            Instantiate(laserDoNebuloso, locaDoDisparo.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            jogadorDentroRaio = true;   // O jogador está dentro do raio de colisão
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            jogadorDentroRaio = false;  // O jogador saiu do raio de colisão
        }
    }

    public void MachucarNebuloso(int danoParaReceber)
    {
        vidaAtualDoInimigo -= danoParaReceber;

        if (vidaAtualDoInimigo <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Destroy(gameObject); // Exclui o GameObject do inimigo
    }
}
