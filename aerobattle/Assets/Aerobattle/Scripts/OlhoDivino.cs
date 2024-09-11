using UnityEngine;

public class OlhoDivino : MonoBehaviour
{
    // Referência ao gerenciador de eventos ou sistema de cutscenes
    public GameManager gameManager;

    // Método chamado quando o jogador colide com o Olho Divino
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            // Inicia a cutscene de vitória
            if (gameManager != null)
            {
                gameManager.IniciarCutsceneDeVitoria();
            }

            // Destrói o Olho Divino após a coleta
            Destroy(gameObject);
        }
    }
}

