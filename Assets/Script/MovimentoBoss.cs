using UnityEngine;

public class MovimentoBoss : MonoBehaviour
{
    [Header("Configurações de Entrada")]
    public float velocidadeEntrada = 3f;
    // O ponto X na tela onde o Boss vai parar para começar a atacar (geralmente perto do X = 6)
    public float posicaoX_DeParada = 6f;
    private bool jaChegouNaPosicao = false;

    [Header("Movimento Flutuante (Céu/Muro)")]
    public float velocidadeFlutuar = 2f;
    public float amplitudeMovimento = 1f; // O quanto ele sobe e desce

    private Vector3 posicaoBase;

    void Start()
    {
        // Salva a posição inicial dele assim que nasce
        posicaoBase = transform.position;
    }

    void Update()
    {
        // 1. COMPORTAMENTO DE ENTRADA: Vem voando da direita até a posição de parada
        if (!jaChegouNaPosicao)
        {
            transform.Translate(Vector3.left * velocidadeEntrada * Time.deltaTime);

            if (transform.position.x <= posicaoX_DeParada)
            {
                jaChegouNaPosicao = true;
                // Atualiza a posição base para o ponto atual onde ele parou
                posicaoBase = transform.position;
            }
        }
        // 2. COMPORTAMENTO DE LUTA: Fica subindo e descendo suavemente no muro
        else
        {
            float novoY = posicaoBase.y + Mathf.Sin(Time.time * velocidadeFlutuar) * amplitudeMovimento;
            transform.position = new Vector3(transform.position.x, novoY, transform.position.z);
        }
    }
}
