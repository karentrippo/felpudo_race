using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D colisor;

    [Header("Configurações de Voo")]
    public float forcaPulo = 15f;
    private bool estaNoChao = true;

    [Header("Sistema de Vida (UI)")]
    public Slider barraDeVida;
    public TMPro.TMP_Text textoVida;
    public float vidaMaxima = 100f;
    private float vidaAtual;

    [Header("Telas de Fim de Jogo")]
    public GameObject telaGameOver;
    public GameObject telaVitoria;

    [Header("Ataque do Jogador")]
    public GameObject limaoProjetilPrefab;

    private Vector2 tamanhoOriginalColisor;
    private Vector2 offsetOriginalColisor;

    private GerenciadorFase gerenciadorFase;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colisor = GetComponent<BoxCollider2D>();

        if (colisor != null)
        {
            tamanhoOriginalColisor = colisor.size;
            offsetOriginalColisor = colisor.offset;
        }

        vidaAtual = vidaMaxima;

        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaAtual;
        }

        if (textoVida != null)
        {
            textoVida.text = vidaAtual.ToString() + " / " + vidaMaxima.ToString();
        }

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.freezeRotation = true;
        }

        gerenciadorFase = FindFirstObjectByType<GerenciadorFase>();
    }

    void Update()
    {
        // ==== COMANDO PARA REINICIAR O JOGO (Tecla R) ==== 
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        if (Time.timeScale == 0f) return;

        // CONTROLE DE VELOCIDADE VERTICAL (Para detectar queda no Animator)
        if (rb != null && !estaNoChao)
        {
            if (rb.linearVelocity.y < -0.1f)
            {
                anim.SetBool("estaPulando", false);
                anim.SetBool("estaCaindo", true);
            }
        }

        // ==== COMANDO PARA ABAIXAR (Seta Para Baixo ou S) - Apenas se estiver no chão! ==== 
        if (estaNoChao && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
        {
            // Ativa o estado abaixado apenas se já não estiver ativado (evita trepidação de colisor)
            if (!anim.GetBool("estaAbaixado"))
            {
                anim.SetBool("estaAbaixado", true);
                anim.SetBool("estaPulando", false);
                anim.SetBool("estaCaindo", false);

                if (colisor != null)
                {
                    colisor.size = new Vector2(tamanhoOriginalColisor.x, tamanhoOriginalColisor.y * 0.5f);
                    colisor.offset = new Vector2(offsetOriginalColisor.x, offsetOriginalColisor.y - (tamanhoOriginalColisor.y * 0.25f));
                }
            }
        }
        else
        {
            // Só redefine o colisor para o tamanho original se ele de fato estava abaixado antes
            if (anim.GetBool("estaAbaixado") || !estaNoChao)
            {
                anim.SetBool("estaAbaixado", false);

                if (colisor != null)
                {
                    colisor.size = tamanhoOriginalColisor;
                    colisor.offset = offsetOriginalColisor;
                }
            }
        }

        // ==== COMANDO PARA PULAR (Apenas se estiver no chão e NÃO estiver abaixado) ==== 
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && estaNoChao && !anim.GetBool("estaAbaixado"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            estaNoChao = false;
            anim.SetBool("estaPulando", true);
            anim.SetBool("estaCaindo", false);
        }

        // ==== COMANDO PARA ATIRAR LIMÃO (Barra de Espaço) ==== 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gerenciadorFase != null && gerenciadorFase.tempoRestante <= 0)
            {
                if (limaoProjetilPrefab != null)
                {
                    Vector3 posicaoTiro = transform.position + new Vector3(1.5f, 0f, 0f);
                    Instantiate(limaoProjetilPrefab, posicaoTiro, Quaternion.identity);
                }
            }
        }
    }

    public void TomarDano(float quantidadeDano)
    {
        vidaAtual -= quantidadeDano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        if (barraDeVida != null) barraDeVida.value = vidaAtual;
        if (textoVida != null) textoVida.text = vidaAtual.ToString() + " / " + vidaMaxima.ToString();

        if (vidaAtual <= 0) MudarParaTelaDerrota();
    }

    public void RecuperarVida(float quantidadeCura)
    {
        vidaAtual += quantidadeCura;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        if (barraDeVida != null) barraDeVida.value = vidaAtual;
        if (textoVida != null) textoVida.text = vidaAtual.ToString() + " / " + vidaMaxima.ToString();
    }

    void MudarParaTelaDerrota()
    {
        if (telaGameOver != null) telaGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AtivarVitoria()
    {
        if (telaVitoria != null) telaVitoria.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Força a validação do chão de maneira imediata e limpa os estados do Animator
        estaNoChao = true;

        if (anim != null)
        {
            anim.SetBool("estaPulando", false);
            anim.SetBool("estaCaindo", false);
            anim.SetBool("estaAbaixado", false);
        }

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.angularVelocity = 0f;
        }
    }
}
