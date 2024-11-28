using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mv_Azul : MonoBehaviour
{
    public float speed = 2f; 
    public float distance = 3f;

    private Vector3 startPosition; 
    private bool movingUp = true; 
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        float movement = speed * Time.deltaTime;

        if (movingUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + movement, transform.position.z);
            if (transform.position.y >= startPosition.y + distance)
                movingUp = false;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - movement, transform.position.z);
            if (transform.position.y <= startPosition.y - distance)
                movingUp = true; 
        }
    }
}
