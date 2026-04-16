using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI textoResultado;
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTempo;

    void Start()
    {
        // Exibe resultado
        if (GameManager.venceu)
            textoResultado.text = "VITÓRIA!";
        else
            textoResultado.text = "GAME OVER";

        // Exibe score e tempo finais
        textoScore.text = "Score: " + GameManager.scoreFinal;
        textoTempo.text = "Tempo restante: " + Mathf.CeilToInt(GameManager.tempoFinal) + "s";
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