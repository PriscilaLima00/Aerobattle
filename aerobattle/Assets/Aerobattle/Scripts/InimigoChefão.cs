using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoChefão : MonoBehaviour
{
    public GameObject laserDoInimigo;
    public Transform[] localDoDisparo;
    
    public float velocidadeDoInimigo;

    public float tempoMaximoEntreOsLasers;
    public float tempoAtualDosLasers;

    public bool inimigoAtirador;

    public int vidaMaximaDoInimigo;
    public int vidaAtualDoInimigo;
    public int danoAoJogador = 1;

    public GameObject itemParaDropar;
    public int chanceDeDropar;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoInimigo = vidaMaximaDoInimigo;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarInimigo();
        if (inimigoAtirador == true)
        {
            AtirarLaser();
        }

    }

    private void MovimentarInimigo()
    {
        transform.Translate(Vector3.left * velocidadeDoInimigo * Time.deltaTime);
    }

    private void AtirarLaser()
    {
        tempoAtualDosLasers -= Time.deltaTime;

        if (tempoAtualDosLasers <= 0)
        {
            // Itera sobre cada local de disparo e instância um laser
            foreach (var local in localDoDisparo)
            {
                Instantiate(laserDoInimigo, local.position, Quaternion.Euler(0f, 0f, -180f));
            }

            // Reinicia o tempo para o próximo disparo
            tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        }
    }

    public void MachucarInimigoChefao(int danoParaReceber)
    {
        vidaAtualDoInimigo -= danoParaReceber;

        if (vidaAtualDoInimigo <= 0)
        {
            int numeroAleatorio = Random.Range(0, 100);

            if (numeroAleatorio <= chanceDeDropar)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }

            Destroy(this.gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador"))
        {
            VidaDoJogador.Instance.MachucarJogador(danoAoJogador); 
            
        }
    }
}
