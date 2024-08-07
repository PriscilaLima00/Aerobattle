using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuracoNegro : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Jogador"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
