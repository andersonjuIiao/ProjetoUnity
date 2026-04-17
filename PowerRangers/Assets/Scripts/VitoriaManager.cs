using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VitoriaManager : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTempo;

    [Header("Audio")]
    public AudioSource musicaVitoria;

    void Start()
    {
        textoScore.text = "Score Final: " + GameManager.scoreFinal;
        textoTempo.text = "Tempo sobrevivido: 60s";

        if (musicaVitoria != null)
            musicaVitoria.Stop();
    }

    public void IniciarMusica()
    {
        if (musicaVitoria != null)
            musicaVitoria.Play();
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