using UnityEngine;

public class ChronoShard : MonoBehaviour
{
    [Header("Movimento")]
    public float amplitudeFlutuar = 0.1f;
    public float velocidadeFlutuar = 2f;

    private Rigidbody2D rb;
    private Vector3 posicaoInicial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicaoInicial = transform.position;
    }

    void FixedUpdate()
    {
        float novoY = posicaoInicial.y + Mathf.Sin(Time.time * velocidadeFlutuar) * amplitudeFlutuar;
        rb.MovePosition(new Vector2(posicaoInicial.x, novoY));
    }
}