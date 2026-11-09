using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidaBoss : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaAtual;
    private Slider barraVidaBoss;
    private TMP_Text textoVidaBoss;

    [Header("Ataque do Uruca")]
    public GameObject frutaEstragadaPrefab;
    public float tempoMinimoAtaque = 1.5f;
    public float tempoMaximoAtaque = 3.5f;

    void Start()
    {
        vidaAtual = vidaMaxima;
        GerenciadorFase geren = FindFirstObjectByType<GerenciadorFase>();
        if (geren != null && geren.barraVidaBossUI != null)
        {
            barraVidaBoss = geren.barraVidaBossUI.GetComponent<Slider>();
            barraVidaBoss.maxValue = vidaMaxima;
            barraVidaBoss.value = vidaAtual;
            textoVidaBoss = geren.barraVidaBossUI.GetComponentInChildren<TMP_Text>();
            if (textoVidaBoss != null) textoVidaBoss.text = vidaAtual + " / " + vidaMaxima;
        }
        // Repete a função de cuspir tomates automaticamente de tempos em tempos
        InvokeRepeating("CuspirFrutaPodre", tempoMinimoAtaque, tempoMaximoAtaque);

    }
    
    void CuspirFrutaPodre()
    {
        // Se o jogo acabou ou o Boss morreu, para o ataque
        if (Time.timeScale == 0f || vidaAtual <= 0) return;

        if (frutaEstragadaPrefab != null)
        {
            // Ajuste 2: Cria o tomate bem à frente do bico do Uruca (afastado em 3.0f)
            Vector3 pos = transform.position - new Vector3(3.0f, 0f, 0f);

            // Sorteia se o tiro vai sair por baixo (asfalto) ou por cima (muro)
            int sorteio = UnityEngine.Random.Range(0, 2);
            pos.y = (sorteio == 0) ? -2.1f : 0.5f;

            // Cria o tomate voando na tela!
            Instantiate(frutaEstragadaPrefab, pos, Quaternion.identity);
        }
    }

    public void TomarDanoBoss(float dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        if (barraVidaBoss != null) barraVidaBoss.value = vidaAtual;
        if (textoVidaBoss != null) textoVidaBoss.text = vidaAtual + " / " + vidaMaxima;

        if (vidaAtual <= 0)
        {
            if (barraVidaBoss != null) barraVidaBoss.gameObject.SetActive(false);
            ControleJogador jog = FindFirstObjectByType<ControleJogador>();
            if (jog != null) jog.AtivarVitoria();
            Destroy(gameObject);
        }
    }
}


