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
        transform.Translate(Vector3.down * velocidadeDoInimigo *Time.deltaTime);
    }

    private void AtirarLaser()
    {
        tempoAtualDosLasers -= Time.deltaTime;

        if (tempoAtualDosLasers <= 0)
        {
            int indice = Random.Range(0, localDoDisparo.Length);
            Instantiate(laserDoInimigo, localDoDisparo[indice].position, Quaternion.Euler(0f, 0f, 90f));
            tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        }
    }

    public void MachucarInimigo(int danoParaReceber)
    {
        vidaAtualDoInimigo -= danoParaReceber;

        if (vidaAtualDoInimigo <= 0)
        {
            Destroy(this.gameObject);
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
}
