using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VitoriaManager : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTempo;

    void Start()
    {
        textoScore.text = "Score Final: " + GameManager.scoreFinal;
        textoTempo.text = "Tempo sobrevivido: 60s";
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
