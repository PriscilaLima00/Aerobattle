using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrotaVex : MonoBehaviour
{
    public GameObject laserDaFrotaVex;
    public Transform[] localDoDisparo;
    public float velocidadeDaFrotaVex;
    public float tempoMaximoEntreOsLasers;
    private float tempoAtualDosLasers;

    public int vidaMaximaDoFrotaVex;
    public int vidaAtualDoFrotaVex;
    public int danoAoJogador = 1;

    public GameObject itemParaDropar;
    public int chanceDeDropar;
    public GameObject efeitoDeExplosão;
    public Animator ani;

    void Start()
    {
        // Inicializa a vida atual do Frota Vex
        vidaAtualDoFrotaVex = vidaMaximaDoFrotaVex;
        tempoAtualDosLasers = tempoMaximoEntreOsLasers; // Define o tempo atual dos lasers para o máximo no início
    }

    void Update()
    {
        ani = GetComponent<Animator>();
        MovimentarInimigo();
        AtirarLaser(); 
    }

    private void MovimentarInimigo()
    {
        transform.Translate(Vector3.left * velocidadeDaFrotaVex * Time.deltaTime);
    }

    private void AtirarLaser()
    {
        tempoAtualDosLasers -= Time.deltaTime; 

        if (tempoAtualDosLasers <= 0)
        {
            Instantiate(laserDaFrotaVex, localDoDisparo[0].position, Quaternion.Euler(0f, 0f, -180f));
            tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        }
    }

    public void MachucarFrota(int danoParaReceber)
    {
        vidaAtualDoFrotaVex -= danoParaReceber;

        if (ani != null)
        {
            ani.SetBool("Hit04",true);
        }
        
        StartCoroutine(DesativarAnimção());
        
        if (vidaAtualDoFrotaVex <= 0)
        {
            Instantiate(efeitoDeExplosão, transform.position, transform.rotation);
            EfeitoSonoro.instance.somDaExplosão.Play();
            int numeroAleatorio = Random.Range(0, 100);
            if (numeroAleatorio <= chanceDeDropar)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    private IEnumerator DesativarAnimção()
    {
        yield return new WaitForSeconds(0.2f);
        if (ani != null)
        {
            ani.SetBool("Hit04",false);
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}