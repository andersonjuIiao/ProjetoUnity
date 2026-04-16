using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidade = 3f;
    public float forcaEmpurrao = 5f;
    public float distanciaSeparacao = 1.5f;
    public float forcaSeparacao = 2f;

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

        float distancia = Vector2.Distance(transform.position, player.position);
        Vector2 direcaoPlayer = (player.position - transform.position).normalized;

        // Calcula força de separação entre inimigos
        Vector2 forcaDeSeparacao = CalcularSeparacao();

        // Combina direção ao player com separação
        Vector2 direcaoFinal = direcaoPlayer + forcaDeSeparacao;

        if (distancia > 0.8f)
            rb.MovePosition(rb.position + direcaoFinal.normalized * velocidade * Time.fixedDeltaTime);
        else
            rb.linearVelocity = Vector2.zero;
    }

    Vector2 CalcularSeparacao()
    {
        Vector2 separacao = Vector2.zero;
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        foreach (GameObject inimigo in inimigos)
        {
            if (inimigo == gameObject) continue;

            float distancia = Vector2.Distance(transform.position, inimigo.transform.position);

            if (distancia < distanciaSeparacao && distancia > 0)
            {
                Vector2 direcaoAfastamento = (transform.position - inimigo.transform.position).normalized;
                separacao += direcaoAfastamento * (distanciaSeparacao - distancia) * forcaSeparacao;
            }
        }

        return separacao;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerMovement.estado == PlayerMovement.PlayerState.Ranger)
            {
                playerMovement.ReceberDanoRanger();
            }
            else
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