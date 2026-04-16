using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Textos da HUD")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoVidas;
    public TextMeshProUGUI textoEnergia;
    public TextMeshProUGUI textoTimerRanger;

    [Header("Bonus de Tempo")]
    public TextMeshProUGUI textoBonus;
    public float tempoBonusVisivel = 2f;
    private float timerBonus = 0f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (timerBonus > 0)
        {
            timerBonus -= Time.deltaTime;
            if (timerBonus <= 0)
                textoBonus.gameObject.SetActive(false);
        }
    }

    public void AtualizarScore(int score)
    {
        if (textoScore != null)
            textoScore.text = "Score: " + score;
    }

    public void AtualizarTempo(float tempo)
    {
        if (textoTempo != null)
            textoTempo.text = "Tempo: " + Mathf.CeilToInt(tempo);
    }

    public void AtualizarVidas(int vidas)
    {
        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;
    }

    public void AtualizarEnergia(int atual, int maximo)
    {
        if (textoEnergia != null)
            textoEnergia.text = "Shards: " + atual + "/" + maximo;
    }

    public void AtualizarTimerRanger(float tempo, float tempoMax)
    {
        if (textoTimerRanger != null)
            textoTimerRanger.text = "Ranger: " + Mathf.CeilToInt(tempo) + "s";
    }

    public void MostrarEnergia(bool mostrar)
    {
        if (textoEnergia != null)
            textoEnergia.gameObject.SetActive(mostrar);
    }

    public void MostrarTimerRanger(bool mostrar)
    {
        if (textoTimerRanger != null)
            textoTimerRanger.gameObject.SetActive(mostrar);
    }

    public void MostrarBonus(string mensagem)
    {
        if (textoBonus != null)
        {
            textoBonus.text = mensagem;
            textoBonus.gameObject.SetActive(true);
            timerBonus = tempoBonusVisivel;
        }
    }
}