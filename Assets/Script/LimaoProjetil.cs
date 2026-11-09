using UnityEngine;

public class LimaoProjetil : MonoBehaviour
{
    [Header("Configurações do Tiro")]
    public float velocidadeTiro = 12f;
    public float danoNoBoss = 20f;
    public GameObject prefabExplosao;

    void Update()
    {
        transform.Translate(Vector3.right * velocidadeTiro * Time.deltaTime);

        if (transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        VidaBoss boss = collision.GetComponent<VidaBoss>();

        if (boss == null && collision.gameObject.name.Contains("Uruca"))
        {
            boss = collision.gameObject.GetComponent<VidaBoss>();
        }

        if (boss != null)
        {
            boss.TomarDanoBoss(danoNoBoss);

            if (prefabExplosao != null)
            {
                GameObject estourou = Instantiate(prefabExplosao, transform.position, Quaternion.identity);
                Destroy(estourou, 0.5f);
            }

            Destroy(gameObject);
        }
    }
}
