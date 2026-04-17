using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DerrotaManager : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTempo;

    [Header("Audio")]
    public AudioSource musicaDerrota;

    void Start()
    {
        textoScore.text = "Score Final: " + GameManager.scoreFinal;
        textoTempo.text = "Você sobreviveu: " + Mathf.CeilToInt(60f - GameManager.tempoFinal) + "s";

        // Música começa desligada — AnimacaoResultado vai ligar após animação
        if (musicaDerrota != null)
            musicaDerrota.Stop();
    }

    public void IniciarMusica()
    {
        if (musicaDerrota != null)
            musicaDerrota.Play();
    }

    public void JogarNovamente()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void IrParaMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
