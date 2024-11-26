using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarregarCena : MonoBehaviour
{

    public string cenaFase01; 
    public float tempoDeCutscene ; 

    private void Start()
    {
        StartCoroutine(IniciarCenaApósCutscene());
    }

    private IEnumerator IniciarCenaApósCutscene()
    {
        
        yield return new WaitForSeconds(tempoDeCutscene);

        
        SceneManager.LoadScene(cenaFase01);
        AudoiManager.instance.SairDoMenu();
    }
}