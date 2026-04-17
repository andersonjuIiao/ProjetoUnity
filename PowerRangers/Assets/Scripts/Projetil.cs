using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 15f;
    public float tempoDeVida = 2f;
    private Vector2 direcao;

    public void Inicializar(Vector2 dir)
    {
        direcao = dir.normalized;
        Destroy(gameObject, tempoDeVida);
    }

    void FixedUpdate()
    {
        transform.Translate(direcao * velocidade * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Inimigo"))
        {
            GameManager.Instance.AdicionarScore(10);
            GameManager.Instance.InimigoDerrotado();
            AudioManager.Instance?.TocarInimigoMorto();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
