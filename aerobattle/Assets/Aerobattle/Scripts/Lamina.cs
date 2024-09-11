using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamina : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidade de rotação
    private bool isActive = false; // Estado da lâmina

    private void Update()
    {
        if (isActive)
        {
            // Rotaciona a lâmina
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
