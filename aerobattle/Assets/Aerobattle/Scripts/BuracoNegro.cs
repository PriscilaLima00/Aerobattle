using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuracoNegro : MonoBehaviour
{

    public GameObject painelGameOver;
   // public float velocidadeDoBuraco = 2f;
    //private Rigidbody2D rig;

    private void Start()
    {
        //rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ///MovimentoDoBuraco();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Jogador"))
        {
            // Ativa o painel de Game Over
            painelGameOver.SetActive(true);

            // Opcional: Pause o jogo
            Time.timeScale = 0; // Para pausar o jogo, se necessário
        }
    }

    private void MovimentoDoBuraco()
    {
        //Vector2 movimento = Vector2.left * velocidadeDoBuraco * Time.deltaTime;
        //transform.position += (Vector3)movimento; // Mover diretamente pelo transform

    }
}
