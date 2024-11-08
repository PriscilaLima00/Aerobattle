using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VitóriaFinal : MonoBehaviour
{
    
    public GameObject telaDeVitoria; // Referência à tela de vitória (UI ou Canvas)
    public string sceneToLoad = "MainMenu"; // Cena para carregar quando o jogador voltar ao menu principal

    // Esse método é chamado quando o objeto colide com outro objeto com um Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto com o qual colidimos tem a tag "Jogador"
        if (other.CompareTag("Jogador"))
        {
            Debug.Log("Jogador colidiu com o troféu!");
            MostrarVitoria(); // Chama a função para mostrar a tela de vitória
            Destroy(gameObject); // Destrói o troféu após a coleta
        }
    }

    // Método para mostrar a tela de vitória
    public void MostrarVitoria()
    {
        if (telaDeVitoria != null)
        {
            telaDeVitoria.SetActive(true); // Ativa a tela de vitória
        }
        else
        {
            Debug.LogError("Tela de Vitória não está atribuída!");
        }
    }

    // Método para voltar para o menu principal quando o botão for pressionado
    public void LoadMainMenu()
    {
        Time.timeScale = 1;  // Retorna o tempo ao normal
        SceneManager.LoadScene(sceneToLoad); // Carrega a cena especificada (por padrão, "MainMenu")
    }

}
