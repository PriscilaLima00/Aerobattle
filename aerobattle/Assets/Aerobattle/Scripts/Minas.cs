using UnityEngine;

public class MinaEspacial : MonoBehaviour
{
    public float raioDeDeteccao = 5f; // Raio de detecção ao redor da mina

    private bool jogadorDentroDoRaio = false;
    private Collider2D minaCollider;
    public int danoParaJogador;

    void Start()
    {
        // Obtém o Collider2D da mina
        minaCollider = GetComponent<Collider2D>();

        if (minaCollider == null)
        {
            Debug.LogError("Collider2D não encontrado na mina.");
        }
    }

    void Update()
    {
        // Se o jogador estiver dentro do raio de detecção, detona a mina
        if (jogadorDentroDoRaio)
        {
            Detonar();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (other.CompareTag("Jogador"))
        {
            jogadorDentroDoRaio = true;
            // Detona imediatamente ao detectar o jogador
            Detonar();
        }
    }

    private void Detonar()
    {
        // Adiciona a lógica para a detonação
        Debug.Log("Mina detonada!");

        // Instancia o efeito de explosão (comentado, mas aqui para referência futura)
        // if (efeitoDeExplosao != null)
        // {
        //     Instantiate(efeitoDeExplosao, transform.position, Quaternion.identity);
        // }

        // Toca o som da explosão (comentado, mas aqui para referência futura)
        // if (somDeExplosao != null)
        // {
        //     AudioSource.PlayClipAtPoint(somDeExplosao, transform.position);
        // }

        // Remove a mina após a detonação
        Destroy(gameObject);
        
        AplicarDanoAoJogador();
    }
    
    private void AplicarDanoAoJogador()
    {
        // Encontra o jogador
        GameObject jogador = GameObject.FindGameObjectWithTag("Jogador");
        if (jogador != null)
        {
            VidaDoJogador vidaDoJogador = jogador.GetComponent<VidaDoJogador>();
            if (vidaDoJogador != null)
            {
                vidaDoJogador.MachucarJogador(danoParaJogador);
            }
            else
            {
                Debug.LogError("VidaDoJogador script não encontrado no jogador.");
            }
        }
        else
        {
            Debug.LogError("Jogador não encontrado.");
        }
    }
}
