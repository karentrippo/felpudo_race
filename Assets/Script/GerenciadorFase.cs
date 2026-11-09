using UnityEngine;
using TMPro;

public class GerenciadorFase : MonoBehaviour
{
    [Header("Configuração de Tempo")]
    public float tempoRestante = 60f;
    public TMP_Text textoCronometro;

    [Header("Geradores de Obstáculos")]
    public GameObject geradorDeLesmas;
    public GameObject geradorDeFrutas;

    [Header("Configuração do Boss")]
    public GameObject bossUrucaPrefab;
    public Transform pontoDeEntradaDoBoss;
    public GameObject barraVidaBossUI; // NOVA LINHA: Arrastaremos o Slider_VidaBoss para cá

    private bool faseTerminou = false;

    void Update()
    {
        if (faseTerminou) return;

        if (tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime;
            AtualizarTextoRelogio(tempoRestante);
        }
        else
        {
            tempoRestante = 0;
            AtualizarTextoRelogio(tempoRestante);
            IniciarBatalhaDeBoss();
        }
    }

    void AtualizarTextoRelogio(float tempoParaMostrar)
    {
        float minutos = Mathf.FloorToInt(tempoParaMostrar / 60);
        float segundos = Mathf.FloorToInt(tempoParaMostrar % 60);

        if (textoCronometro != null)
        {
            textoCronometro.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    void IniciarBatalhaDeBoss()
    {
        faseTerminou = true;
        Debug.Log("O tempo acabou! Limpando a pista para a perseguição do Uruca!");

        if (geradorDeLesmas != null) geradorDeLesmas.SetActive(false);
        if (geradorDeFrutas != null) geradorDeFrutas.SetActive(false);

        // Deleta os inimigos e frutas restantes
        MovimentoInimigo[] inimigosRestantes = FindObjectsByType<MovimentoInimigo>(FindObjectsSortMode.None);
        foreach (MovimentoInimigo inimigo in inimigosRestantes) Destroy(inimigo.gameObject);

        ItemFruta[] frutasRestantes = FindObjectsByType<ItemFruta>(FindObjectsSortMode.None);
        foreach (ItemFruta fruta in frutasRestantes) Destroy(fruta.gameObject);

        // NOVA AÇÃO: Ativa a barra de vida do Boss na tela de forma direta!
        if (barraVidaBossUI != null)
        {
            barraVidaBossUI.SetActive(true);
        }

        // Cria o Boss Uruca
        if (bossUrucaPrefab != null && pontoDeEntradaDoBoss != null)
        {
            Instantiate(bossUrucaPrefab, pontoDeEntradaDoBoss.position, Quaternion.identity);
        }

        if (textoCronometro != null)
        {
            textoCronometro.text = "BOSS: URUCA!";
            textoCronometro.color = Color.red;
        }
    }
}
