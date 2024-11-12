using UnityEngine;
using UnityEngine.SceneManagement;

public class OlhoDivino : MonoBehaviour
{
    public VitóriaFinal vitoriaFinalScript; 
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))  
        {
            
            vitoriaFinalScript.MostrarVitoria();
            Destroy(gameObject);  
        }
    }
}

