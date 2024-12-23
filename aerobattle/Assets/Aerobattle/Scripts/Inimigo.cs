using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Inimigo : MonoBehaviour
{
    public GameObject laserDoInimigo;
    public Transform[] localDoDisparo;
    
    public float velocidadeDoInimigo;

    public float tempoMaximoEntreOsLasers;
    public float tempoAtualDosLasers;

    public bool inimigoAtirador;

    public int vidaMaximaDoInimigo;
    public int vidaAtualDoInimigo;
    public int danoAoJogador = 1;

    public GameObject itemParaDropar;
    public int chanceDeDropar;
    
    public GameObject efeitoDeExplosão;

    public Animator anin;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoInimigo = vidaMaximaDoInimigo;
    }

    // Update is called once per frame
    void Update()
    {
        anin = GetComponent<Animator>();
        MovimentarInimigo();
        if (inimigoAtirador == true)
        {
            AtirarLaser();
        }

    }

    private void MovimentarInimigo()
    {
        transform.Translate(Vector3.left * velocidadeDoInimigo * Time.deltaTime);
    }

    private void AtirarLaser()
    {
        tempoAtualDosLasers -= Time.deltaTime;

        if (tempoAtualDosLasers <= 0)
        {
            int indice = Random.Range(0, localDoDisparo.Length);
            Instantiate(laserDoInimigo, localDoDisparo[indice].position, Quaternion.Euler(0f, 0f, -180f));
            tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        }
    }

    public void MachucarInimigo(int danoParaReceber)
    {
        vidaAtualDoInimigo -= danoParaReceber;

        if (anin != null)
        {
            anin.SetBool("Hit01",true);
        }

        StartCoroutine(DesativarAnimção());
        
        
        if (vidaAtualDoInimigo <= 0)
        { 
            Instantiate(efeitoDeExplosão, transform.position, transform.rotation);
            EfeitoSonoro.instance.somDaExplosão.Play();
            int numeroAleatorio = Random.Range(0, 100);

            if (numeroAleatorio <= chanceDeDropar)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }

            Destroy(this.gameObject);
        }
    }

    private IEnumerator DesativarAnimção()
    {
        yield return new WaitForSeconds(0.2f);
        if (anin != null)
        {
            anin.SetBool("Hit01",false);
        }
    }
    
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador"))
        {
            // Obtém o componente VidaDoJogador do objeto jogador
            VidaDoJogador vidaDoJogador = colisao.gameObject.GetComponent<VidaDoJogador>();
            if (vidaDoJogador != null)
            {
                vidaDoJogador.MachucarJogador(danoAoJogador);
            }
        }
    }
    
   
    
    public void AplicaDano(int dano)
    {
        vidaAtualDoInimigo -= dano;

        // Verifica se o inimigo morreu
        if (vidaAtualDoInimigo <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Destroy(gameObject); // Exclui o GameObject do inimigo
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto com o qual a moeda colidiu tem a tag "destroy"
        if (other.CompareTag("Destroy"))
        {
            // Destroi a moeda
            Destroy(gameObject);
        }
    }
    
}