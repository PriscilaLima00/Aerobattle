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
    public bool estarVivo { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        temLaserDuplo = false;
        estarVivo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (estarVivo)
        {
            MovimentoPlay();
            AtirarLaser();  
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colidiu com: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Buraco negro"))
        {
            CairNoBuracoNegro();
        }
    }
    
    private void CairNoBuracoNegro()
    {
        if (estarVivo)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        estarVivo = false;
        Debug.Log("o jogador caiu no buraco negro e morreu");
        
        // destroi o gameobject do jogador
        Destroy(gameObject);
        
        //
        Invoke("ReiniciarNivel", 1f);
    }

    private void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
