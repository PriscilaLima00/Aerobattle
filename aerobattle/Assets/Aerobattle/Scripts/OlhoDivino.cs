using UnityEngine;
using UnityEngine.SceneManagement;

public class OlhoDivino : MonoBehaviour
{
    public VitóriaFinal vitoriaFinalScript; // Referência para o script de Vitória Final

    // Quando o jogador colide com o "olho divino"
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jogador"))  
        {
            
            vitoriaFinalScript.MostrarVitoria();
            Destroy(gameObject);  
        }
    }
}

