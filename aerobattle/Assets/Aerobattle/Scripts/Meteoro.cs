using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteoro : MonoBehaviour
{
    public int vidaAtualDoMeteoro;

    public int vidaMaximaDoMeteoro;

    public int velocidadeDoMeteoro;

    public GameObject efeitoDeExplosão;

    public Animator ani;
    void Start()
    {
        vidaAtualDoMeteoro = vidaMaximaDoMeteoro;
    }

    // Update is called once per frame
    void Update()
    {
        ani = GetComponent<Animator>();
        MovimentarDoMeteoro();
       
    }
    
    private void MovimentarDoMeteoro()
    {
        transform.Translate(Vector3.left * velocidadeDoMeteoro * Time.deltaTime);
    }
    
    public void MachucarMeteoro(int danoParaReceber)
    {
        vidaAtualDoMeteoro -= danoParaReceber;

        if (ani != null)
        {
            ani.SetBool("Hit02",true);
        }
        
        StartCoroutine(DesativarAnimção());
        
        if (vidaAtualDoMeteoro <= 0)
        {
            Instantiate(efeitoDeExplosão, transform.position, transform.rotation);
            EfeitoSonoro.instance.somDaExplosão.Play();
            Destroy(this.gameObject);
        }
    }
    private IEnumerator DesativarAnimção()
    {
        yield return new WaitForSeconds(0.2f);
        if (ani != null)
        {
            ani.SetBool("Hit02",false);
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

