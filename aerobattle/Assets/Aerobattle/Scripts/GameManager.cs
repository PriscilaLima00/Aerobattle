using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject cutsceneDeVitoria; // O GameObject ou prefab da cutscene de vitória
    public string proximaCena = "CutsceneDaVitoria"; // Nome da cena para a qual você deseja ir após a cutscene

    private void Start()
    {
        if (cutsceneDeVitoria != null)
        {
            cutsceneDeVitoria.SetActive(false); // Inicialmente desativa a cutscene
        }
    }

    public void IniciarCutsceneDeVitoria()
    {
        if (cutsceneDeVitoria != null)
        {
            cutsceneDeVitoria.SetActive(true); // Ativa a cutscene
        }

        // Se você deseja carregar a cena de vitória após um tempo, use o método abaixo:
        StartCoroutine(CarregarCenaDeVitoria());
    }

    private IEnumerator CarregarCenaDeVitoria()
    {
        // Aguarda o tempo necessário para a cutscene ser exibida (exemplo: 5 segundos)
        yield return new WaitForSeconds(5f);

        // Carrega a cena de vitória
        SceneManager.LoadScene(proximaCena);
    }
}