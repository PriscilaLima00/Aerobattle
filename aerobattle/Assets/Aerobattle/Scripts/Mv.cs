using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mv : MonoBehaviour
{
    public int velocidade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
    }

    private void Movimento()
    {
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
