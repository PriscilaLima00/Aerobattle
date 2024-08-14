using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo02 : MonoBehaviour
{
    public float velocidadeMovimento;

    public float intervaloTiro;

    public GameObject localDoDisparo;

    public Transform pontoDeTiro;


    private Vector3[] pontosTriangular;

    private int pontoAtual = 0;

    private float tempo = 0;
    // Start is called before the first frame update
    void Start()
    {
        pontosTriangular = new Vector3[3];
        pontosTriangular[0] = transform.position;
        pontosTriangular[1] = transform.position + new Vector3(3f, 0f, 0f); // Ponto à direita
        pontosTriangular[2] = transform.position + new Vector3(1.5f, Mathf.Sqrt(3) * 1.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
