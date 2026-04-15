using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configurações")]
    public int vidasIniciais = 3;
    public float tempoDaPartida = 60f;

    [HideInInspector] public int score = 0;
    [HideInInspector] public int vidas;
    [HideInInspector] public float tempoRestante;
    [HideInInspector] public bool jogoAtivo = false;

    public static int scoreFinal = 0;
    public static float tempoFinal = 0f;
    public static bool venceu = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        vidas = vidasIniciais;
        tempoRestante = tempoDaPartida;
        jogoAtivo = true;
    }

    void Update()
    {
        if (!jogoAtivo) return;

        tempoRestante -= Time.deltaTime;
        HUDManager.Instance?.AtualizarTempo(tempoRestante);

        if (tempoRestante <= 0)
        {
            tempoRestante = 0;
            GameOver(false);
        }
    }

    public void AdicionarScore(int pontos)
    {
        score += pontos;
        HUDManager.Instance?.AtualizarScore(score);
    }

    public void PerderVida()
    {
        vidas--;
        HUDManager.Instance?.AtualizarVidas(vidas);

        if (vidas <= 0)
            GameOver(false);
    }

    public void GameOver(bool ganhou)
    {
        if (!jogoAtivo) return;

        jogoAtivo = false;
        scoreFinal = score;
        tempoFinal = tempoRestante;
        venceu = ganhou;

        SceneManager.LoadScene("GameOver");
    }
}
