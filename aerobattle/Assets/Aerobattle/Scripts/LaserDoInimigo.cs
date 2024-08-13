using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoInimigo : MonoBehaviour
{
    public float velocidadeDoLaser;

    public int danoParaDar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarLaser();
    }

    private void MovimentarLaser()
    {
        transform.Translate(Vector3.up * velocidadeDoLaser * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Jogador"))
        {
            colision.gameObject.GetComponent<VidaDoJogador>().MachucarJogador(danoParaDar);
        }
    }
}
