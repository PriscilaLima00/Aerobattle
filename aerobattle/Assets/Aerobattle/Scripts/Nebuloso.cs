using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebuloso : MonoBehaviour
{
    public GameObject laserDoInimigo;
    public Transform localDoDisparo;

    public float velocidadeDoNebuloso;
    public float velocidadeFrontal;
    private float inicioVertical;
    
    public float tempoMaximoEntreOsLasers;
    public float tempoAtualDosLasers;

    public bool inimigoAtirador = true;

    public int vidaMaximaDoNebuloso;
    public int vidaAtualDoNebuloso;
    
    public float limiteVertical;  // Limite vertical de movimentação do inimigo
    public float limiteHorizontal; // Limite horizontal de movimentação do inimigo
    private bool jogadorDentroRaio = false;   // Flag para verificar se o jogador está dentro do raio
    public float raioDeDeteccao;//raio que detecção do jogador
    public Transform jogador;
    
    private Vector3 posicaoInicial;  // Posição inicial do inimigo
    private bool indoParaDireita = true; // Flag para verificar a direção da patrulha
    
    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoNebuloso = vidaMaximaDoNebuloso;
        tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        inicioVertical = transform.position.y;// Armazena a posição inicial vertical do inimigo
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarNebuloso();
        VerificarJogador();
        AtirarSeNecessario();
    }

    private void MovimentarNebuloso()
    {
        // Se o jogador está dentro do raio de detecção, move-se em direção ao jogador
        if (jogadorDentroRaio)
        {
            MoverParaJogador();
        }
        else
        {
            // Movimenta o inimigo verticalmente
            float novaPosicaoY = Mathf.PingPong(Time.time * velocidadeDoNebuloso, limiteVertical * 2) - limiteVertical;
            Vector3 pp =  new Vector3(transform.position.x, novaPosicaoY, transform.position.z);
             transform.position = Vector3.Lerp(transform.position, pp, 5 * Time.deltaTime);
            
            // Movimenta o inimigo horizontalmente dentro dos limites
            float novaPosicaoX = transform.position.x + (indoParaDireita ? velocidadeFrontal : -velocidadeFrontal) * Time.deltaTime;
            
            // Verifica se o inimigo atingiu o limite horizontal e inverte a direção
            if (novaPosicaoX > posicaoInicial.x + limiteHorizontal)
            {
                indoParaDireita = false;
            }
            else if (novaPosicaoX < posicaoInicial.x - limiteHorizontal)
            {
                indoParaDireita = true;
            }
            Vector3 posFinal = new Vector3(novaPosicaoX, transform.position.y, transform.position.z);
            transform.position = posFinal; //Vector3.Lerp(transform.position, posFinal, 5 * Time.deltaTime);
        }
    }
    
    private void MoverParaJogador()
    {
        // Move o inimigo em direção ao jogador
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        transform.position += direcaoParaJogador * velocidadeFrontal * Time.deltaTime;
        
        // Mantém a posição vertical dentro dos limites definidos
        float novaPosicaoY = Mathf.Clamp(transform.position.y, posicaoInicial.y - limiteVertical, posicaoInicial.y + limiteVertical);
        transform.position = new Vector3(transform.position.x, novaPosicaoY, transform.position.z);
    }

    private void VerificarJogador()
    {
        // Verifica se o jogador está dentro do raio de detecção
        if (jogador != null)
        {
            float distanciaParaJogador = Vector3.Distance(transform.position, jogador.position);
            jogadorDentroRaio = distanciaParaJogador < raioDeDeteccao;
        }
        else
        {
            jogadorDentroRaio = false;
        }
    }

    private void AtirarSeNecessario()
    {
        if (inimigoAtirador && jogadorDentroRaio)
        {
            tempoAtualDosLasers -= Time.deltaTime;

            if (tempoAtualDosLasers <= 0)
            {
                AtirarLaser();
                tempoAtualDosLasers = tempoMaximoEntreOsLasers;
            }
        }
    }

    private void AtirarLaser()
    {
        if (laserDoInimigo != null && localDoDisparo != null)
        {
            GameObject tiro =  Instantiate(laserDoInimigo, localDoDisparo.position, localDoDisparo.rotation);

            Vector2 direcao = (jogador.position - transform.position).normalized;
            float angulo = Mathf.Rad2Deg * Mathf.Atan2(direcao.y, direcao.x) ;
           
            tiro.transform.eulerAngles = new Vector3(0, 0, angulo);

        }
    }

    public void MachucarNebuloso(int danoparareceber)
    {
        vidaAtualDoNebuloso -= danoparareceber;

        if (vidaAtualDoNebuloso <= 0)
        {
            Destroy(this.gameObject);
        }
    
    }
    
    public void AplicaDano(int dano)
    {
        vidaAtualDoNebuloso-= dano;

        // Verifica se o inimigo morreu
        if (vidaAtualDoNebuloso <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Destroy(gameObject); // Exclui o GameObject do inimigo
    }
}
