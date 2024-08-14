using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteoro : MonoBehaviour
{
    public int vidaAtualDoMeteoro;

    public int vidaMaximaDoMeteoro;

    public int velocidadeDoMeteoro;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoMeteoro = vidaMaximaDoMeteoro;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarDoMeteoro();
       
    }
    
    private void MovimentarDoMeteoro()
    {
        transform.Translate(Vector3.left * velocidadeDoMeteoro * Time.deltaTime);
    }
    
    public void MachucarMeteoro(int danoParaReceber)
    {
        vidaAtualDoMeteoro -= danoParaReceber;

        if (vidaAtualDoMeteoro <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    
    public void AplicaDano(int dano)
    {
        vidaAtualDoMeteoro -= dano;

        // Verifica se o inimigo morreu
        if (vidaAtualDoMeteoro <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Destroy(gameObject); // Exclui o GameObject do inimigo
    }
}

