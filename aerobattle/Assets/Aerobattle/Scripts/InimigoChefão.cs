using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoChefão : MonoBehaviour
{
    public GameObject laserDoInimigo;
    public Transform[] localDoDisparo;

    public float velocidadeDoInimigo;
    public float tempoMaximoEntreOsLasers;
    private float tempoAtualDosLasers;

    public bool inimigoAtirador;

    public int vidaMaximaDoInimigo;
    public int vidaAtualDoInimigo;
    public int danoAoJogador = 1;

    public GameObject itemParaDropar;
    public int chanceDeDropar;

    public Transform jogador;
    private VidaDoJogador vidaDoJogador;

    public float distanciaMinimaParaParar = 0.5f; // Distância mínima antes de parar de se mover para o jogador

// Start is called before the first frame update
    void Start()
    {
        vidaAtualDoInimigo = vidaMaximaDoInimigo;
        tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        vidaDoJogador = jogador.GetComponent<VidaDoJogador>();
    }

// Update is called once per frame
    void Update()
    {
        MovimentarInimigo();
        if (inimigoAtirador && vidaDoJogador != null && vidaDoJogador.EstahVivo())
        {
            AtirarLaser();
        }
    }

    private void MovimentarInimigo()
    {
        if (jogador != null)
        {
            // Calcula a direção para o jogador
            Vector3 direcao = (jogador.position - transform.position).normalized;

            // Verifica a distância entre o inimigo e o jogador
            float distanciaParaJogador = Vector3.Distance(transform.position, jogador.position);

            if (distanciaParaJogador > distanciaMinimaParaParar)
            {
                // Se o inimigo estiver distante o suficiente do jogador, ele pode continuar a perseguição
                transform.position += direcao * velocidadeDoInimigo * Time.deltaTime;
            }
            else
            {
                // Caso o inimigo esteja muito perto, ele para de se mover
                transform.position = transform.position;
            }
        }
    }

    private void AtirarLaser()
    {
        tempoAtualDosLasers -= Time.deltaTime;

        if (tempoAtualDosLasers <= 0)
        {
            // Itera sobre cada local de disparo e instância um laser
            foreach (var local in localDoDisparo)
            {
                GameObject tiro = Instantiate(laserDoInimigo, local.position, local.rotation);

                // Calcula a direção para o jogador
                Vector2 direcao = (jogador.position - local.position).normalized;
                float angulo = Mathf.Rad2Deg * Mathf.Atan2(direcao.y, direcao.x);

                // Aplica a rotação ao tiro
                tiro.transform.eulerAngles = new Vector3(0, 0, angulo);
            }

            // Reinicia o tempo para o próximo disparo
            tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        }
    }

    public void MachucarInimigoChefao(int danoParaReceber)
    {
        vidaAtualDoInimigo -= danoParaReceber;

        if (vidaAtualDoInimigo <= 0)
        {
            int numeroAleatorio = Random.Range(0, 100);

            if (numeroAleatorio <= chanceDeDropar)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }

            Destroy(this.gameObject);
        }
    }

// Detecta colisões com o jogador
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador"))
        {
            VidaDoJogador vidaDoJogador = colisao.gameObject.GetComponent<VidaDoJogador>();
            if (vidaDoJogador != null)
            {
                vidaDoJogador.MachucarJogador(danoAoJogador);
            }
        }
    }
}