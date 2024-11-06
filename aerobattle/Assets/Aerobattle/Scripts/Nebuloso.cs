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
    public int velocidade;

    public float tempoMaximoEntreOsLasers;
    public float tempoAtualDosLasers;

    public bool inimigoAtirador = true;

    public int vidaMaximaDoNebuloso;
    public int vidaAtualDoNebuloso;

    public float limiteVertical;
    public float limiteHorizontal;
    private bool jogadorDentroRaio = false;
    private bool perseguindoJogador = false; // Nova variável de controle
    public float raioDeDeteccao;
    public Transform jogador;

    private Vector3 posicaoInicial;
    private bool indoParaDireita = true;

    public GameObject itemParaDropar;
    public int chanceDeDropar;

// Start is called before the first frame update
    void Start()
    {
        vidaAtualDoNebuloso = vidaMaximaDoNebuloso;
        tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        inicioVertical = transform.position.y;
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
        if (perseguindoJogador)
        {
            MoverParaJogador();
        }
        else
        {
            // Movimenta o inimigo verticalmente usando PingPong sem Lerp
            float novaPosicaoY = Mathf.PingPong(Time.time * velocidadeDoNebuloso, limiteVertical * 2) - limiteVertical +
                                 inicioVertical;
            Vector3 posicaoVertical = new Vector3(transform.position.x, novaPosicaoY, transform.position.z);
            transform.position = posicaoVertical;

            // Movimenta o inimigo horizontalmente
            float novaPosicaoX = transform.position.x +
                                 (indoParaDireita ? velocidadeFrontal : -velocidadeFrontal) * Time.deltaTime;

            // Verifica se o inimigo atingiu os limites horizontais e inverte a direção
            if (novaPosicaoX > posicaoInicial.x + limiteHorizontal)
            {
                indoParaDireita = false;
            }
            else if (novaPosicaoX < posicaoInicial.x - limiteHorizontal)
            {
                indoParaDireita = true;
            }

            // Atualiza a posição final horizontal
            transform.position = new Vector3(novaPosicaoX, transform.position.y, transform.position.z);
        }
    }

    private void MoverParaJogador()
    {
        // Move o inimigo em direção ao jogador
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        transform.position += direcaoParaJogador * velocidadeFrontal * Time.deltaTime;

        // Mantém a posição vertical dentro dos limites definidos
        float novaPosicaoY = Mathf.Clamp(transform.position.y, posicaoInicial.y - limiteVertical,
            posicaoInicial.y + limiteVertical);
        transform.position = new Vector3(transform.position.x, novaPosicaoY, transform.position.z);
    }

    private void VerificarJogador()
    {
        // Verifica se o jogador está dentro do raio de detecção
        if (jogador != null && !perseguindoJogador) // Só verifica se o jogador não está sendo perseguido
        {
            float distanciaParaJogador = Vector3.Distance(transform.position, jogador.position);
            jogadorDentroRaio = distanciaParaJogador < raioDeDeteccao;

            if (jogadorDentroRaio)
            {
                perseguindoJogador = true; // Começa a perseguir o jogador
            }
        }
    }

    private void Mv()
    {
        transform.Translate(Vector3.left * velocidade* Time.deltaTime);
    }
    private void AtirarSeNecessario()
    {
        if (inimigoAtirador && perseguindoJogador)
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
            GameObject tiro = Instantiate(laserDoInimigo, localDoDisparo.position, localDoDisparo.rotation);

            Vector2 direcao = (jogador.position - transform.position).normalized;
            float angulo = Mathf.Rad2Deg * Mathf.Atan2(direcao.y, direcao.x);

            tiro.transform.eulerAngles = new Vector3(0, 0, angulo);
        }
    }

    public void MachucarNebuloso(int danoparareceber)
    {
        vidaAtualDoNebuloso -= danoparareceber;

        if (vidaAtualDoNebuloso <= 0)
        {
            int numeroAleatorio = Random.Range(0, 100);

            if (numeroAleatorio <= chanceDeDropar)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }

            Morrer();
        }
    }
    
    

    public void AplicaDano(int dano)
    {
        vidaAtualDoNebuloso -= dano;

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
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Zera a velocidade do Rigidbody2D para impedir o empurrão
        Rigidbody2D rbNebuloso = GetComponent<Rigidbody2D>();
        if (rbNebuloso != null)
        {
            rbNebuloso.velocity = Vector2.zero;
            rbNebuloso.angularVelocity = 0f; // Zera a velocidade angular também
        }
    }

    
}