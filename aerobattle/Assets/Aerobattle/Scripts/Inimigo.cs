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

    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoInimigo = vidaMaximaDoInimigo;
    }

    // Update is called once per frame
    void Update()
    {
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
    
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador"))
        {
            VidaDoJogador.Instance.MachucarJogador(danoAoJogador); 
            
        }
    }
    
}