using UnityEngine;

public class GeradorFrutas : MonoBehaviour
{
    [Header("Configurações do Molde")]
    public GameObject frutaPrefab;

    [Header("Ritmo das Frutas")]
    public float tempoMinimo = 2.0f;
    public float tempoMaximo = 5.0f;

    [Header("Alturas Livres para Flutuar")]
    public float alturaMinima = 0.5f;
    public float alturaMaxima = 3.5f;

    void Start()
    {
        InvocarProximaFruta();
    }

    void InvocarProximaFruta()
    {
        // Corrigido para usar a biblioteca exata do Unity
        float tempoAleatorio = UnityEngine.Random.Range(tempoMinimo, tempoMaximo);
        Invoke("CriarFrutaAleatoria", tempoAleatorio);
    }

    void CriarFrutaAleatoria()
    {
        GerenciadorFase gerenciador = FindFirstObjectByType<GerenciadorFase>();

        if (gerenciador != null && gerenciador.tempoRestante <= 0)
        {
            return;
        }

        Vector3 posicaoDeSpawn = transform.position;

        // Corrigido para evitar ambiguidade no Unity 6
        posicaoDeSpawn.y = UnityEngine.Random.Range(alturaMinima, alturaMaxima);

        if (frutaPrefab != null)
        {
            Instantiate(frutaPrefab, posicaoDeSpawn, Quaternion.identity);
        }

        InvocarProximaFruta();
    }
}
