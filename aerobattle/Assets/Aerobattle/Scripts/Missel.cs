using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missel : MonoBehaviour
{
    public Transform jogador;
    public float velocidade;
    public float detectionRadius;
    public float destroyRadius;

    private bool isTracking = false;

    public GameObject efeitoDeExplosão;
    public Animator ani;

    private void Update()
    {

        ani = GetComponent<Animator>();
            if (jogador == null) return;

            float distance = Vector2.Distance(transform.position, jogador.position);

            if (distance < detectionRadius)
            {
                isTracking = true;
            }

            if (isTracking)
            {
                Vector2 direction = (jogador.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, jogador.position, velocidade * Time.deltaTime);
                
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));

                if (distance < destroyRadius)
                {
                    // Chama o método de dano no jogador
                    VidaDoJogador playerHealth = jogador.GetComponent<VidaDoJogador>();
                    if (playerHealth != null)
                    {
                        playerHealth.MachucarJogador(15); 
                    }

                    // Destrói o míssil
                    Destroy(gameObject);
                }
            } 
            Movimento();
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Laser do Jogador"))
        {
            Instantiate(efeitoDeExplosão, transform.position, transform.rotation);
            EfeitoSonoro.instance.somDaExplosão.Play();
            Destroy(gameObject);
        } 
        if (collider.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
    

    private void Movimento()
    {
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);
    }
    
}