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
    
    public Text scoreText;
    public int score;
    
    void Start()
    {
        temLaserDuplo = false;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
            MovimentoPlay();
            AtirarLaser();

            scoreText.text = score.ToString();
            
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Moeda") == true)
        {
            score = score + 1;
            Destroy(col.gameObject);
        }
    }

    private void MovimentoPlay()
    {
        teclasApertadas = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        rigi.velocity = teclasApertadas.normalized * velocidadeDaNave;
    }

    private void AtirarLaser()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (temLaserDuplo == false)
            {
                Instantiate(laser,localDoDisparoUnico.position,localDoDisparoUnico.rotation);
            } 
        }
    }
    
}
