using UnityEngine;

public class GeradorInimigos : MonoBehaviour
{
    [Header("Configurações do Molde")]
    public GameObject prefabInimigo;

    [Header("Ritmo do Jogo")]
    public float tempoMinimoEntreInimigos = 2.5f;
    public float tempoMaximoEntreInimigos = 5.0f;

    [Header("REGRA DO CHÃO: Altura Variável")]
    public float asfaltoAlturaMaisBaixa = -2.5f;
    public float asfaltoAlturaMaisAlta = -1.8f;

    [Header("REGRA DO MURO: Altura Totalmente Fixa")]
    public float Y_ExatoDoMuroFixado = 0.5f;

    void Start()
    {
        InvocarProximoInimigo();
    }

    void InvocarProximoInimigo()
    {
        // Corrigido para usar a biblioteca exata do Unity
        float tempoAleatorio = UnityEngine.Random.Range(tempoMinimoEntreInimigos, tempoMaximoEntreInimigos);
        Invoke("CriarInimigoComVariacao", tempoAleatorio);
    }

    void CriarInimigoComVariacao()
    {
        GerenciadorFase gerenciador = FindFirstObjectByType<GerenciadorFase>();

        if (gerenciador != null && gerenciador.tempoRestante <= 0)
        {
            return;
        }

        Vector3 posicaoDeSpawn = new Vector3(transform.position.x, 0f, transform.position.z);

        // Corrigido para evitar ambiguidade no Unity 6
        int escolhaDeTipo = UnityEngine.Random.Range(0, 2);

        if (escolhaDeTipo == 0)
        {
            // Corrigido para evitar ambiguidade no Unity 6
            posicaoDeSpawn.y = UnityEngine.Random.Range(asfaltoAlturaMaisBaixa, asfaltoAlturaMaisAlta);
            Debug.Log("Lesma no CHÃO (Altura Variada!)");
        }
        else
        {
            posicaoDeSpawn.y = Y_ExatoDoMuroFixado;
            Debug.Log("Lesma no MURO (Altura FIXA!)");
        }

        if (prefabInimigo != null)
        {
            Instantiate(prefabInimigo, posicaoDeSpawn, Quaternion.identity);
        }

        InvocarProximoInimigo();
    }
}
