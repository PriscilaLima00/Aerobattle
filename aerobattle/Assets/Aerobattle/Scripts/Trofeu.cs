using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trofeu : MonoBehaviour
{
    // Este método será chamado quando o jogador colidir com o troféu
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o objeto que colidiu é o jogador
        if (other.CompareTag("Jogador"))
        {
            Debug.Log("Jogador colidiu com o troféu");
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
            // Antes de carregar a nova cena, você pode resetar o estado do jogador aqui
            // Por exemplo, você pode ajustar a posição do jogador ou resetar o estado
            // Esse código vai apenas carregar a próxima cena
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