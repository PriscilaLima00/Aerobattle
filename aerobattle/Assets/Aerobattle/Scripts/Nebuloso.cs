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
    private bool perseguindoJogador = false; 
    public float raioDeDeteccao;
    public Transform jogador;

    private Vector3 posicaoInicial;
    private bool indoParaDireita = true;

    public GameObject itemParaDropar;
    public int chanceDeDropar;

    public GameObject efeitoDeExplosão;
    public Animator anima;


    void Start()
    {
        vidaAtualDoNebuloso = vidaMaximaDoNebuloso;
        tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        inicioVertical = transform.position.y;
    }
    
    void Update()
    {
        anima = GetComponent<Animator>(); 
        
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
            float novaPosicaoY = Mathf.PingPong(Time.time * velocidadeDoNebuloso, limiteVertical * 2) - limiteVertical +
                                 inicioVertical;
            Vector3 posicaoVertical = new Vector3(transform.position.x, novaPosicaoY, transform.position.z);
            transform.position = posicaoVertical;
            
            float novaPosicaoX = transform.position.x +
                                 (indoParaDireita ? velocidadeFrontal : -velocidadeFrontal) * Time.deltaTime;
            
            if (novaPosicaoX > posicaoInicial.x + limiteHorizontal)
            {
                indoParaDireita = false;
            }
            else if (novaPosicaoX < posicaoInicial.x - limiteHorizontal)
            {
                indoParaDireita = true;
            }
            transform.position = new Vector3(novaPosicaoX, transform.position.y, transform.position.z);
        }
    }

    private void MoverParaJogador()
    {
        Vector3 direcaoParaJogador = (jogador.position - transform.position).normalized;
        transform.position += direcaoParaJogador * velocidadeFrontal * Time.deltaTime;
        
        float novaPosicaoY = Mathf.Clamp(transform.position.y, posicaoInicial.y - limiteVertical,
            posicaoInicial.y + limiteVertical);
        transform.position = new Vector3(transform.position.x, novaPosicaoY, transform.position.z);
    }

    private void VerificarJogador()
    {
        if (jogador != null && !perseguindoJogador)
        {
            float distanciaParaJogador = Vector3.Distance(transform.position, jogador.position);
            jogadorDentroRaio = distanciaParaJogador < raioDeDeteccao;

            if (jogadorDentroRaio)
            {
                perseguindoJogador = true; 
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
        
        if (anima != null)
        {
            anima.SetBool("Hit03",true);
        }
        
        StartCoroutine(DesativarAnimção());
        
        if (vidaAtualDoNebuloso <= 0)
        {
            Instantiate(efeitoDeExplosão, transform.position, transform.rotation);
            EfeitoSonoro.instance.somDaExplosão.Play();
            
            int numeroAleatorio = Random.Range(0, 100);

            if (numeroAleatorio <= chanceDeDropar)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }

            Morrer();
        }
    }
    private IEnumerator DesativarAnimção()
    {
        yield return new WaitForSeconds(0.2f);
        if (anima != null)
        {
            anima.SetBool("Hit03",false);
        }
    }
    
    

    public void AplicaDano(int dano)
    {
        vidaAtualDoNebuloso -= dano;
        
        if (vidaAtualDoNebuloso <= 0)
        {
            Morrer();
            
        }
    }

    void Morrer()
    {
        Destroy(gameObject); 
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rbNebuloso = GetComponent<Rigidbody2D>();
        if (rbNebuloso != null)
        {
            rbNebuloso.velocity = Vector2.zero;
            rbNebuloso.angularVelocity = 0f; 
        }
    }

    
}