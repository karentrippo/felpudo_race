using UnityEngine;

public class ItemFruta : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 3.0f;

    [Header("Benefícios ao Coletar")]
    public float pontosGanhos = 10f;
    public float vidaParaRecuperar = 15f;

    [Header("Efeito de Explosão")]
    public GameObject prefabExplosao;
    public float tempoMinimoParaEstourar = 1.5f;
    public float tempoMaximoParaEstourar = 5.0f;

    void Start()
    {
        // Sorteia o tempo de vida aleatório usando as variáveis do Unity
        float tempoVidaAleatorio = Random.Range(tempoMinimoParaEstourar, tempoMaximoParaEstourar);

        // Agenda a explosão automática
        Invoke("ExplodirSozinha", tempoVidaAleatorio);
    }

    void Update()
    {
        // Move a fruta para a esquerda junto com o cenário
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);

        // Se passar muito da tela para a esquerda, se destrói por segurança
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    // Detecta quando o Fofuxo encosta na fruta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ControleJogador jogador = collision.GetComponent<ControleJogador>();

        if (jogador != null)
        {
            // Cura o Fofuxo normalmente
            jogador.RecuperarVida(vidaParaRecuperar);
            Debug.Log("Fruta coletada! Ganhou " + pontosGanhos + " pontos.");

            // A fruta apenas some direto, sem soltar fumaça na coleta
            Destroy(gameObject);
        }
    }

    // Função chamada APENAS se o tempo acabar e ele NÃO pegar a fruta
    void ExplodirSozinha()
    {
        Debug.Log("O tempo da fruta acabou e ela estourou sozinha!");
        GerarEfeitoExplosao();
        Destroy(gameObject);
    }

    void GerarEfeitoExplosao()
    {
        if (prefabExplosao != null)
        {
            // Cria a animação de explosão na posição exata da fruta
            GameObject novaExplosao = Instantiate(prefabExplosao, transform.position, Quaternion.identity);

            // Destrói o efeito após 0.5 segundos para limpar a memória
            Destroy(novaExplosao, 0.5f);
        }
    }
}
