using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lanterna : MonoBehaviour
{
    public static Lanterna Instance { get; private set; }

    public Light2D lanternaLight; // Referência ao componente Light2D da lanterna
    public GameObject lanternaGameObject; // Referência ao GameObject da lanterna (para ativação/desativação)

    private void Awake()
    {
        // Implementa o padrão Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o objeto entre as cenas
        }
        else
        {
            Destroy(gameObject); // Garante que haja apenas uma instância
        }
    }

    void Start()
    {
        // Inicialmente desativa a lanterna
        if (lanternaLight != null)
        {
            lanternaLight.enabled = false;
        }
        if (lanternaGameObject != null)
        {
            lanternaGameObject.SetActive(false);
        }
    }

    // Método para ativar a lanterna quando coletada
    public void AtivarLanterna()
    {
        if (lanternaLight != null)
        {
            lanternaLight.enabled = true;
        }
        if (lanternaGameObject != null)
        {
            lanternaGameObject.SetActive(true);
        }
    }

    // Método para desativar a lanterna
    public void DesativarLanterna()
    {
        if (lanternaLight != null)
        {
            lanternaLight.enabled = false;
        }
        if (lanternaGameObject != null)
        {
            lanternaGameObject.SetActive(false);
        }
    }
}
