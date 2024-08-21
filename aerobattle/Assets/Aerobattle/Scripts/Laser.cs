using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
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
        if (colision.gameObject.CompareTag("Inimigo"))
        {
            colision.gameObject.GetComponent<Inimigo>().MachucarInimigo(danoParaDar);
            Destroy(this.gameObject);
        }
        else if (colision.gameObject.CompareTag("Meteoro"))
        {
            colision.gameObject.GetComponent<Meteoro>().MachucarMeteoro(danoParaDar);
            Destroy(this.gameObject);
        }
        else if (colision.gameObject.CompareTag("Nebuloso"))
        {
            colision.gameObject.GetComponent<Nebuloso>().MachucarNebuloso(danoParaDar);
            Destroy(this.gameObject);
        }
    }
    
 
}
