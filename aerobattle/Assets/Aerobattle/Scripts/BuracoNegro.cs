using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuracoNegro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.CompareTag("Buraco negro"))
        {
            Debug.LogWarning("Tag 'Buraco negro' não encontrada. Definindo tag.");
            gameObject.tag = "Buraco negro";
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
