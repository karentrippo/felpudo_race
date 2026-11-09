using UnityEngine;

public class MovimentoInimigo : MonoBehaviour
{
    public float velocidade = 4f;
    public float dano = 20f; // Quanto de vida ela vai tirar da barra

    void Update()
    {
        // Move a lesma continuamente para a esquerda
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);

        // Se a lesma andar muito para a esquerda e sumir da tela, ela se destrói para não pesar o jogo
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

    // Detecta quando o carro do Fofuxo encosta nela
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se quem bateu foi o jogador
        ControleJogador jogador = collision.GetComponent<ControleJogador>();

        if (jogador != null)
        {
            // Aplica o dano na barra de vida
            jogador.TomarDano(dano);

            // Destrói a lesma após acertar o jogador para não dar múltiplos danos
            Destroy(gameObject);
        }
    }
}
