using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject impactoDoLaser;
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
        if (colision.gameObject.CompareTag("Inimigo"))
        {
            colision.gameObject.GetComponent<Inimigo>().MachucarInimigo(danoParaDar);
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            Destroy(gameObject); 
        }
        else if (colision.gameObject.CompareTag("Meteoro"))
        {
            colision.gameObject.GetComponent<Meteoro>().MachucarMeteoro(danoParaDar);
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (colision.gameObject.CompareTag("Nebuloso"))
        {
            colision.gameObject.GetComponent<Nebuloso>().MachucarNebuloso(danoParaDar);
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (colision.gameObject.CompareTag("Asteroide P."))
        {
            colision.gameObject.GetComponent<AsteroideP>().MachucarAsteroide(danoParaDar);
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            Destroy(gameObject); 
        }
        else if (colision.gameObject.CompareTag("Vex.09"))
        {
            colision.gameObject.GetComponent<Vex_09>().ReceberDanoVex(danoParaDar);
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            Destroy(gameObject); 
        }
        else if (colision.gameObject.CompareTag("Asteroide G."))
        {
            colision.gameObject.GetComponent<AsteroideG>().MachucarAsteroideG(danoParaDar);
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            Destroy(gameObject); 
        }
        else if (colision.gameObject.CompareTag("Missel"))
        {
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            Destroy(gameObject); 
        }
        else if (colision.gameObject.CompareTag("Frota Vex"))
        {
            colision.gameObject.GetComponent<FrotaVex>().MachucarFrota(danoParaDar);
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (colision.gameObject.CompareTag("Chefão"))
        {
            Instantiate(impactoDoLaser, transform.position, transform.rotation);
            colision.gameObject.GetComponent<InimigoChefão>().MachucarInimigoChefao(danoParaDar);
        }
    }

    
}
