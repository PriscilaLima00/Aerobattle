using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missel : MonoBehaviour
{
    public Transform jogador; // Referência ao Transform do jogador
    public float velocidade;
    public float detectionRadius;
    public float destroyRadius;

    private bool isTracking = false; // Para verificar se o míssil deve começar a seguir o jogador

    private void Update()
    {
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

            // Atualizar a rotação do míssil para apontar para o jogador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle -180));
            
            
            if (distance < destroyRadius)
            {
                // Destrói ambos os GameObjects
                Destroy(jogador.gameObject);
                Destroy(gameObject);
            }
        }
    }

    // Detecta colisões com outros colliders
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Verifica se o objeto com o qual o míssil colidiu tem a tag "Laser do Jogador"
        if (collider.gameObject.CompareTag("Laser do Jogador"))
        {
            // Destrói o míssil
            Destroy(gameObject);
        }
    }
}