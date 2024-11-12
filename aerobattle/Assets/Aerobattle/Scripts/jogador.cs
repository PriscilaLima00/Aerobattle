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
    
    void Start()
    {
        
        temLaserDuplo = false;

        tempoAtualDosLasersDuplos = tempoMaximoDosLasersDuplos;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
            MovimentoPlay();
            AtirarLaser();

            if (temLaserDuplo == true)
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

        if (col.CompareTag("Moeda") == true)
        {
            AudioObserver.OnPlaySfxEvent("coletavel");
            score = score + 1;
            Destroy(col.gameObject);
        }
    }


    private void MovimentoPlay()
    {
        teclasApertadas = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigi.velocity = teclasApertadas.normalized * velocidadeDaNave;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (transform.position.x > 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z); // Limite para frente
        }
        else if (transform.position.x < -10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z); // Limite para trás
        }
    }

    private void AtirarLaser()
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            
            if (temLaserDuplo == false)
            {
                Instantiate(laser, localDoDisparoUnico.position, localDoDisparoUnico.rotation);
            }
            else 
            {
                Instantiate(laser, localDoDisparoDaEsquerda.position, localDoDisparoDaEsquerda.rotation);
                Instantiate(laser, localDoDisparoDaDireita.position, localDoDisparoDaDireita.rotation);
            }
        }
    }

    private void DesativarLaserDuplo()
    {
        temLaserDuplo = false;

        tempoAtualDosLasersDuplos = tempoMaximoDosLasersDuplos;
    }
    
    
}
