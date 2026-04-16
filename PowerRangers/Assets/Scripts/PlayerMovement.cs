using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState { Humano, Transformando, Ranger }
    public PlayerState estado = PlayerState.Humano;

    [Header("Movimento")]
    public float velocidadeHumano = 10f;
    public float velocidadeRanger = 14f;

    [Header("Chrono Shards")]
    public int shardsNecessarios = 5;
    private int shardsColetados = 0;

    [Header("Transformação")]
    public float duracaoRanger = 10f;
    private float timerRanger = 0f;

    [Header("Ataque")]
    public GameObject projetilPrefab;
    public Transform pontoDeDisparo;
    public float intervaloAtaque = 0.3f;
    private float timerAtaque = 0f;
    private Vector2 ultimaDirecao = Vector2.right;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (estado == PlayerState.Ranger)
        {
            timerRanger -= Time.deltaTime;
            timerAtaque -= Time.deltaTime;

            if (timerRanger <= 0)
                VoltarParaHumano();

            if (Input.GetKey(KeyCode.J) && timerAtaque <= 0)
                Atirar();
        }

        if (Input.GetKeyDown(KeyCode.Space) && estado == PlayerState.Humano)
        {
            if (shardsColetados >= shardsNecessarios)
                IniciarTransformacao();
        }
    }

    void FixedUpdate()
    {
        if (estado == PlayerState.Transformando) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (movement != Vector2.zero)
            ultimaDirecao = movement.normalized;

        float velocidade = (estado == PlayerState.Ranger) ? velocidadeRanger : velocidadeHumano;
        rb.MovePosition(rb.position + movement.normalized * velocidade * Time.fixedDeltaTime);
    }

    void Atirar()
    {
        timerAtaque = intervaloAtaque;
        Vector3 posDisparo = pontoDeDisparo != null ? pontoDeDisparo.position : transform.position;
        GameObject proj = Instantiate(projetilPrefab, posDisparo, Quaternion.identity);
        proj.GetComponent<Projetil>().Inicializar(ultimaDirecao);
    }

    void IniciarTransformacao()
    {
        estado = PlayerState.Transformando;
        shardsColetados = 0;
        HUDManager.Instance?.AtualizarEnergia(0, shardsNecessarios);
        TransformationController.Instance.IniciarAnimacao();
    }

    public void AtivarRanger()
    {
        estado = PlayerState.Ranger;
        timerRanger = duracaoRanger;
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    void VoltarParaHumano()
    {
        estado = PlayerState.Humano;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void ColetarShard()
    {
        if (estado != PlayerState.Humano) return;

        shardsColetados++;
        audioSource.Play();
        HUDManager.Instance?.AtualizarEnergia(shardsColetados, shardsNecessarios);

        if (shardsColetados >= shardsNecessarios)
            Debug.Log("Energia cheia! Pressione Espaço para transformar!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coletavel"))
        {
            ColetarShard();
            Destroy(other.gameObject);
        }
    }
}