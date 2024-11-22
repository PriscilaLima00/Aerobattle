using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteoro : MonoBehaviour
{
    public int vidaAtualDoMeteoro;

    public int vidaMaximaDoMeteoro;

    public int velocidadeDoMeteoro;

    public GameObject efeitoDeExplosão;
    void Start()
    {
        vidaAtualDoMeteoro = vidaMaximaDoMeteoro;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarDoMeteoro();
       
    }
    
    private void MovimentarDoMeteoro()
    {
        transform.Translate(Vector3.left * velocidadeDoMeteoro * Time.deltaTime);
    }
    
    public void MachucarMeteoro(int danoParaReceber)
    {
        vidaAtualDoMeteoro -= danoParaReceber;

        
        if (vidaAtualDoMeteoro <= 0)
        {
            Instantiate(efeitoDeExplosão, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    
    
    public void AplicaDano(int dano)
    {
        vidaAtualDoMeteoro -= dano;
        
        if (vidaAtualDoMeteoro <= 0)
        {
            Morrer();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    void Morrer()
    {
        Destroy(gameObject); 
    }
}

