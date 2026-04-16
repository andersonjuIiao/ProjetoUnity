using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidade = 3f;
    public float forcaEmpurrao = 5f;

    private Transform player;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerMovement = playerObj.GetComponent<PlayerMovement>();
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;
        if (!GameManager.Instance.jogoAtivo) return;

        Vector2 direcao = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerMovement.estado != PlayerMovement.PlayerState.Ranger)
            {
                other.GetComponent<PlayerHealth>()?.PerderVida();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerMovement.estado == PlayerMovement.PlayerState.Ranger)
            {
                Vector2 direcaoEmpurrao = (transform.position - player.position).normalized;
                rb.AddForce(direcaoEmpurrao * forcaEmpurrao, ForceMode2D.Impulse);
            }
            else
            {
                other.GetComponent<PlayerHealth>()?.PerderVida();
            }
        }
    }
}