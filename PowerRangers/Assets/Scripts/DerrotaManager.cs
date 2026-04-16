using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DerrotaManager : MonoBehaviour
{
    [Header("Textos")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTempo;

    void Start()
    {
        textoScore.text = "Score Final: " + GameManager.scoreFinal;
        textoTempo.text = "Você sobreviveu: " + Mathf.CeilToInt(60f - GameManager.tempoFinal) + "s";
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
