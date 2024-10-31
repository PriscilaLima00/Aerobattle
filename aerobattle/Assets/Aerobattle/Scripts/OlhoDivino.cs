using UnityEngine;
using UnityEngine.SceneManagement;

public class OlhoDivino : MonoBehaviour
{
    public Vitória victoryScreenController; 
    // Este método será chamado quando o jogador colidir com o Olho Divino
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o objeto que colidiu é o jogador
        if (other.CompareTag("Jogador"))
        {
            AdvanceToVictoryScreen();
            Destroy(this.gameObject);
            
        }
    
    }
    
    private void AdvanceToVictoryScreen()
    {
        if (victoryScreenController != null)
        {
            victoryScreenController.ShowVictoryScreen();
        }
    }
}

