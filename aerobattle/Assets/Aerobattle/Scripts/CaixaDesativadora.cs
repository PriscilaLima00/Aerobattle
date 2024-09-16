using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaixaDesativadora : MonoBehaviour
{
    public GameObject[] laminas; // Array de lâminas que serão desativadas

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu com a caixinha desativadora é um tiro
        if (other.gameObject.CompareTag("Laser do Jogador"))
        {
            DeactivateBlades(); // Desativa as lâminas
            Destroy(gameObject); // Opcional: destrói a caixinha desativadora após o tiro
        }
    }

    private void DeactivateBlades()
    {
        foreach (var blade in laminas)
        {
            if (blade != null)
            {
                // Envia uma mensagem para parar a animação da lâmina
                blade.SendMessage("StopAnimation", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    
}
