using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetavéis : MonoBehaviour
{
    public bool itemDeEscudo;
    public bool itemDeLaserDuplo;
    public bool itemDeVida;

    public int vidaParaDar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Jogador"))
        {
            if (itemDeEscudo == true)
            {
                other.gameObject.GetComponent<VidaDoJogador>().AtivarEscudo();
            }

            if (itemDeLaserDuplo == true)
            {
                other.gameObject.GetComponent<jogador>().temLaserDuplo = false;
                other.gameObject.GetComponent<jogador>().tempoAtualDosLasersDuplos = 
                    other.gameObject.GetComponent<jogador>().tempoMaximoDosLasersDuplos;
                other.gameObject.GetComponent<jogador>().temLaserDuplo = true;
            }

            if (itemDeVida == true)
            {
                other.gameObject.GetComponent<VidaDoJogador>().GanharVida(vidaParaDar);
            }
            EfeitoSonoro.instance.somDeColeta.Play();
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Destroy"))
        {
            Destroy(this.gameObject);
        }
        
        
    }
}
