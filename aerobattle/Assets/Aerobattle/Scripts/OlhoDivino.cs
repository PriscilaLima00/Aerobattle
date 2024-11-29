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
            AudoiManager.instance.PararMusica();
            Time.timeScale = 0;
            Destroy(gameObject);  
        }
    }
}

