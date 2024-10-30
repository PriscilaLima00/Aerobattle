using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroideG : MonoBehaviour
{
    public int vidaAtualDoAsteroide;

    public int vidaMaximaDoAsteroide;

    public int velocidadeDoAsteroide;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoAsteroide= vidaMaximaDoAsteroide; 
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoDoAsteroideG();
    }

    private void MovimentoDoAsteroideG()
    {
        transform.Translate(Vector3.left * velocidadeDoAsteroide * Time.deltaTime);

    }
    
    public void MachucarAsteroideG(int danoParaReceber)
    {
        vidaAtualDoAsteroide -= danoParaReceber;

        if (vidaAtualDoAsteroide <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    
    public void AplicaDano(int dano)
    {
        vidaAtualDoAsteroide -= dano;

        // Verifica se o inimigo morreu
        if (vidaAtualDoAsteroide <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Destroy(gameObject); // Exclui o GameObject do inimigo
    }
}
