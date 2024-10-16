using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform cenario; // Referência ao objeto do cenário
    public Vector3 offset; // Distância entre a câmera e o cenário

    void Start()
    {
        // Define um offset padrão se não for definido no Inspector
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 0, -10); // Ajuste conforme necessário
        }
    }

    void LateUpdate()
    {
        // Move a câmera para seguir o cenário
        transform.position = cenario.position + offset;
    }
}
