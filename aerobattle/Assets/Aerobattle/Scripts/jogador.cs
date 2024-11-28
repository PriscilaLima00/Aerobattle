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
    
    public float intervaloEntreTiros = 0.2f;
    private float tempoParaProximoTiro;

    void Start()
    {
        temLaserDuplo = false;
        tempoAtualDosLasersDuplos = tempoMaximoDosLasersDuplos;
        score = 0;
        tempoParaProximoTiro = 0f; 
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

        if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -8)
        {
            transform.position = new Vector3(-8, transform.position.y, transform.position.z); 
        }
    }

    private void AtirarLaser()
    {
        if (Input.GetKey(KeyCode.K))
        {
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
                
                tempoParaProximoTiro = Time.time + intervaloEntreTiros;
                
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