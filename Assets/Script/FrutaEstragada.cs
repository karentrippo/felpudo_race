using UnityEngine;

public class FrutaEstragada : MonoBehaviour
{
    [Header("Configurações do Ataque do Boss")]
    public float velocidadeVoo = 8f;
    public float danoNoJogador = 15f; // Quanto tira da barra do Fofuxo

    void Update()
    {
        // Voa da direita para a esquerda (na direção do carro)
        transform.Translate(Vector3.left * velocidadeVoo * Time.deltaTime);

        // Se passar do jogador e sair da tela, se destrói para economizar memória
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a fruta podre bateu no carro do Fofuxo
        ControleJogador jogador = collision.GetComponent<ControleJogador>();

        if (jogador != null)
        {
            // Fofuxo toma dano na barra de vida!
            jogador.TomarDano(danoNoJogador);

            Debug.Log("Fofuxo foi atingido por uma fruta estragada!");

            // Some com a fruta podre após o impacto
            Destroy(gameObject);
        }
    }
}
