using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState { Humano, Transformando, Ranger }
    public PlayerState estado = PlayerState.Humano;

    [Header("Movimento")]
    public float velocidadeHumano = 5f;
    public float velocidadeRanger = 7f;

    [Header("Chrono Shards")]
    public int shardsNecessarios = 5;
    private int shardsColetados = 0;

    [Header("Transformação")]
    public float duracaoRanger = 10f;
    public float tempoBonusRanger = 3f;
    public float tempoReducaoDano = 2f;
    private float timerRanger = 0f;

    [Header("Ataque")]
    public GameObject projetilPrefab;
    public Transform pontoDeDisparo;
    public float intervaloAtaque = 0.3f;
    private float timerAtaque = 0f;
    private Vector2 ultimaDirecao = Vector2.right;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction attackAction;
    private Vector2 movimentoAtual;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            attackAction = playerInput.actions["Attack"];
        }

        HUDManager.Instance?.MostrarEnergia(true);
        HUDManager.Instance?.MostrarTimerRanger(false);
        HUDManager.Instance?.AtualizarEnergia(0, shardsNecessarios);
    }

    void Update()
    {
        // Lê input de movimento
        if (moveAction != null)
            movimentoAtual = moveAction.ReadValue<Vector2>();
        else
            movimentoAtual = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (estado == PlayerState.Ranger)
        {
            timerRanger -= Time.deltaTime;
            timerAtaque -= Time.deltaTime;

            HUDManager.Instance?.AtualizarTimerRanger(timerRanger, duracaoRanger);

            if (timerRanger <= 0)
                VoltarParaHumano();

            // Espaço ou botão Attack para atirar
            bool atacar = attackAction != null ? attackAction.WasPressedThisFrame() : Input.GetKeyDown(KeyCode.Space);
            if (atacar && timerAtaque <= 0)
                Atirar();
        }

        if (estado == PlayerState.Humano)
        {
            bool atacar = attackAction != null ? attackAction.WasPressedThisFrame() : Input.GetKeyDown(KeyCode.Space);
            if (atacar && shardsColetados >= shardsNecessarios)
                IniciarTransformacao();
        }
    }

    void FixedUpdate()
    {
        if (estado == PlayerState.Transformando) return;

        if (movimentoAtual != Vector2.zero)
            ultimaDirecao = movimentoAtual.normalized;

        float velocidade = (estado == PlayerState.Ranger) ? velocidadeRanger : velocidadeHumano;
        rb.MovePosition(rb.position + movimentoAtual.normalized * velocidade * Time.fixedDeltaTime);
    }

    void Atirar()
    {
        timerAtaque = intervaloAtaque;
        Vector3 posDisparo = pontoDeDisparo != null ? pontoDeDisparo.position : transform.position;
        GameObject proj = Instantiate(projetilPrefab, posDisparo, Quaternion.identity);
        proj.GetComponent<Projetil>().Inicializar(ultimaDirecao);
        AudioManager.Instance?.TocarDisparo();
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
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Ranger");
        GetComponent<SpriteRenderer>().color = Color.white;
        transform.localScale = new Vector3(0.175f, 0.175f, 1f);

        HUDManager.Instance?.MostrarEnergia(false);
        HUDManager.Instance?.MostrarTimerRanger(true);
        HUDManager.Instance?.AtualizarTimerRanger(timerRanger, duracaoRanger);
    }

    void VoltarParaHumano()
    {
        estado = PlayerState.Humano;
        shardsColetados = 0;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Humano");
        GetComponent<SpriteRenderer>().color = Color.white;
        transform.localScale = new Vector3(0.33f, 0.33f, 1f);

        HUDManager.Instance?.MostrarTimerRanger(false);
        HUDManager.Instance?.MostrarEnergia(true);
        HUDManager.Instance?.AtualizarEnergia(0, shardsNecessarios);
    }

    public void ColetarShard()
    {
        if (estado == PlayerState.Humano)
        {
            if (shardsColetados >= shardsNecessarios) return;

            shardsColetados++;
            AudioManager.Instance?.TocarColeta();
            HUDManager.Instance?.AtualizarEnergia(shardsColetados, shardsNecessarios);

            if (shardsColetados >= shardsNecessarios)
                Debug.Log("Energia cheia! Pressione Espaço para transformar!");
        }
        else if (estado == PlayerState.Ranger)
        {
            timerRanger += tempoBonusRanger;
            AudioManager.Instance?.TocarColeta();
            HUDManager.Instance?.MostrarBonus("+" + tempoBonusRanger + "s Ranger!");
        }
    }

    public void ReceberDanoRanger()
    {
        if (estado != PlayerState.Ranger) return;

        timerRanger -= tempoReducaoDano;
        HUDManager.Instance?.MostrarBonus("-" + tempoReducaoDano + "s!");

        if (timerRanger <= 0)
            VoltarParaHumano();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coletavel"))
        {
            ColetarShard();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Coletavel"))
        {
            ColetarShard();
            Destroy(other.gameObject);
        }
    }
}