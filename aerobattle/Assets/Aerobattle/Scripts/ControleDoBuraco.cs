using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDoBuraco : MonoBehaviour
{
    public float pullStrength = 10f; // Força de atração do buraco negro
    public float detectionRadius = 10f; // Raio de detecção ao redor do buraco negro

    private void FixedUpdate()
    {
        // Encontra todos os colliders dentro do raio de detecção
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        
        foreach (Collider collider in colliders)
        {
            // Verifica se o objeto é o jogador
            if (collider.CompareTag("Player"))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                
                if (rb != null)
                {
                    // Calcula a direção do buraco negro para o jogador
                    Vector3 direction = transform.position - collider.transform.position;
                    // Aplica uma força na direção do buraco negro
                    rb.AddForce(direction.normalized * pullStrength);
                }
            }
        }
    }
}
