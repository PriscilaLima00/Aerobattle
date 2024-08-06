using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para gerenciar as cenas

public class PlayerController : MonoBehaviour
{
    public float fallThreshold = -10f; // Profundidade abaixo da qual consideramos que o jogador caiu
    
    void Update()
    {
        // Verifica se o jogador caiu abaixo do limite
        if (transform.position.y < fallThreshold)
        {
            RestartLevel(); // Reinicia o nível
        }
    }

    void RestartLevel()
    {
        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
