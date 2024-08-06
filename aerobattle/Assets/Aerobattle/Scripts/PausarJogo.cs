using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausarJogo : MonoBehaviour
{
    public bool jogoPausado;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        jogoPausado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pausar();
        }
    }
    
    private void Pausar()
    {
        if (jogoPausado == false)
        {
            Time.timeScale = 0f;
            jogoPausado = true;
        }
        else
        {
            Time.timeScale = 1f;
            jogoPausado = false;
        }
    }
}
