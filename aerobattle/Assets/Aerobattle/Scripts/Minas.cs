using UnityEngine;

public class MinaEspacial : MonoBehaviour
{
    public float raioDeDeteccao = 5f; // Raio de detecção ao redor da mina

    private bool jogadorDentroDoRaio = false;
    private Collider2D minaCollider;
    public int danoParaJogador;
    public int velocidadeDaMina;

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
        
        Movimento();
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
        if (other.CompareTag("Destroy"))
        { 
            Destroy(gameObject);
        }
    }

    private void Detonar()
    {
        // Adiciona a lógica para a detonação
        Debug.Log("Mina detonada!");
        
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
    
    private void Movimento()
    {
        transform.Translate(Vector3.left * velocidadeDaMina* Time.deltaTime);
    }
}
