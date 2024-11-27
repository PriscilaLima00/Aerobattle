using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mv_Azul : MonoBehaviour
{
    public float speed = 2f; 
    public float distance = 3f; // Distância máxima que o inimigo pode se mover para cima e para baixo

    private Vector3 startPosition; // Posição inicial do inimigo
    private bool movingUp = true; // Direção inicial do movimento

    // Start é chamado antes do primeiro quadro
    void Start()
    {
        // Armazena a posição inicial do inimigo
        startPosition = transform.position;
    }

    // Update é chamado uma vez por quadro
    void Update()
    {
        // Movimentação do inimigo
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // Calcula a nova posição com base na direção do movimento
        float movement = speed * Time.deltaTime;

        if (movingUp)
        {
            // Move o inimigo para cima
            transform.position = new Vector3(transform.position.x, transform.position.y + movement, transform.position.z);
            if (transform.position.y >= startPosition.y + distance)
                movingUp = false; // Inverte a direção quando o inimigo atinge o limite superior
        }
        else
        {
            // Move o inimigo para baixo
            transform.position = new Vector3(transform.position.x, transform.position.y - movement, transform.position.z);
            if (transform.position.y <= startPosition.y - distance)
                movingUp = true; // Inverte a direção quando o inimigo atinge o limite inferior
        }
    }
}
