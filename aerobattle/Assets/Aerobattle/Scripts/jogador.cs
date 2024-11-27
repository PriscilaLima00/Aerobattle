using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class jogador : MonoBehaviour
{
     public Rigidbody2D rigi;
    public float velocidadeDaNave;
    public bool temLaserDuplo;

    private Vector2 teclasApertadas;
    public GameObject laser;
    public Transform localDoDisparoUnico;
    public Transform localDoDisparoDaEsquerda;
    public Transform localDoDisparoDaDireita;

    public float tempoMaximoDosLasersDuplos;
    public float tempoAtualDosLasersDuplos;

    public Text scoreText;
    public int score;

    // Novo campo para controle do intervalo de disparo do laser
    public float intervaloEntreTiros = 0.2f;  // Tempo entre disparos consecutivos
    private float tempoParaProximoTiro;

    void Start()
    {
        temLaserDuplo = false;
        tempoAtualDosLasersDuplos = tempoMaximoDosLasersDuplos;
        score = 0;
        tempoParaProximoTiro = 0f; // Inicializa o tempo de espera
    }

    void Update()
    {
        MovimentoPlay();
        AtirarLaser();

        if (temLaserDuplo)
        {
            tempoAtualDosLasersDuplos -= Time.deltaTime;

            if (tempoAtualDosLasersDuplos <= 0)
            {
                DesativarLaserDuplo();
            }
        }

        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Moeda"))
        {
            AudioObserver.OnPlaySfxEvent("coletavel");
            score++;
            Destroy(col.gameObject);
        }
    }

    private void MovimentoPlay()
    {
        teclasApertadas = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigi.velocity = teclasApertadas.normalized * velocidadeDaNave;

        if (transform.position.x > 9)
        {
            transform.position = new Vector3(9, transform.position.y, transform.position.z); // Limite para frente
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(-9, transform.position.y, transform.position.z); // Limite para trás
        }
    }

    private void AtirarLaser()
    {
        // Verifica se a tecla K está sendo pressionada
        if (Input.GetKey(KeyCode.K))
        {
            // Verifica se o tempo de disparo passou o intervalo
            if (Time.time >= tempoParaProximoTiro)
            {
                if (temLaserDuplo)
                {
                    Instantiate(laser, localDoDisparoDaEsquerda.position, localDoDisparoDaEsquerda.rotation);
                    Instantiate(laser, localDoDisparoDaDireita.position, localDoDisparoDaDireita.rotation);
                }
                else
                {
                    Instantiate(laser, localDoDisparoUnico.position, localDoDisparoUnico.rotation);
                }

                // Atualiza o tempo para o próximo disparo
                tempoParaProximoTiro = Time.time + intervaloEntreTiros;

                // Reproduz o som do laser
                EfeitoSonoro.instance.somDoLaser.Play();
            }
        }
    }

    private void DesativarLaserDuplo()
    {
        temLaserDuplo = false;
        tempoAtualDosLasersDuplos = tempoMaximoDosLasersDuplos;
    }
}