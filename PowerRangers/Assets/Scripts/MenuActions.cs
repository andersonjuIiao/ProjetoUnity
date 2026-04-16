using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void IniciaJogo()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AbrirCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }
}