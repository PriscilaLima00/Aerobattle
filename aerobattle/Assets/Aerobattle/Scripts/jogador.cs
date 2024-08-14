using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jogador : MonoBehaviour
{
    public Rigidbody2D rigi;
    public float velocidadeDaNave;
    public bool temLaserDuplo;

    private Vector2 teclasApertadas;
    public GameObject laser;
    public Transform localDoDisparoUnico;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        temLaserDuplo = false;
    }

    // Update is called once per frame
    void Update()
    {
            MovimentoPlay();
            AtirarLaser();  
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
