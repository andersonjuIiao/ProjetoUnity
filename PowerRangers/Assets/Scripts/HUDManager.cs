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

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
            textoEnergia.text = "Energia: " + atual + "/" + maximo;
    }
}
