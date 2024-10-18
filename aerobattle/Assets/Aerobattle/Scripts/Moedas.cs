using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moedas : MonoBehaviour
{
    public int velocidadeDaMoeda;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoDaMoeda();
    }

    private void MovimentoDaMoeda()
    {
        transform.Translate(Vector3.left * velocidadeDaMoeda * Time.deltaTime);
    }
}
