using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float velocidade = 3.0f;
    private float larguraImagem;
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        larguraImagem = sr.size.x * transform.localScale.x;
    }

    void Update()
    {
        // Move o cenário infinitamente sem parar nunca!
        float novoDeslocamento = Time.time * velocidade;
        float posicaoRepetida = Mathf.Repeat(novoDeslocamento, larguraImagem / 2f);
        transform.position = posicaoInicial + Vector3.left * posicaoRepetida;
    }
}
