using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trofeu : MonoBehaviour
{
    // Este método será chamado quando o jogador colidir com o troféu
    private void OnTriggerEnter(Collider other)
    {
        // Verifique se o objeto que colidiu é o jogador
        if (other.CompareTag("Jogador"))
        {
            Debug.Log("jogador colidiu com o trofeu");
            // Passa para a próxima fase
            AdvanceToNextLevel();
        }
    }

    // Função para avançar para a próxima fase
    private void AdvanceToNextLevel()
    {
        // Obtém o índice da cena atual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Carrega a próxima cena
        // Verifica se a próxima cena existe antes de tentar carregar
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // Se não houver próxima fase, reinicie o nível atual ou faça o que desejar
            Debug.Log("Última fase. Reiniciando o nível.");
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
