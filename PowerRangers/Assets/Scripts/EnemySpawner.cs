using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject inimigoPrefab;

    [Header("Configurações de Spawn")]
    public int maxInimigos = 5;
    public float intervaloInicial = 5f;
    public float intervaloMinimo = 1.5f;
    public float reducaoIntervalo = 0.1f;

    [Header("Limites do Mapa")]
    public float xMin = -6.5f;
    public float xMax = 6.5f;
    public float yMin = -3.5f;
    public float yMax = 3.5f;

    private float intervaloAtual;
    private float timerSpawn = 0f;

    void Start()
    {
        intervaloAtual = intervaloInicial;
    }

    void Update()
    {
        if (!GameManager.Instance.jogoAtivo) return;

        timerSpawn += Time.deltaTime;

        if (timerSpawn >= intervaloAtual)
        {
            timerSpawn = 0f;

            // Dificuldade aumenta com o tempo
            intervaloAtual = Mathf.Max(intervaloMinimo, intervaloAtual - reducaoIntervalo);

            VerificarESpawnar();
        }
    }

    void VerificarESpawnar()
    {
        int inimigosNaTela = GameObject.FindGameObjectsWithTag("Inimigo").Length;

        if (inimigosNaTela < maxInimigos)
            SpawnarInimigo();
    }

    void SpawnarInimigo()
    {
        // Spawna nas bordas do mapa
        Vector3 posicao = GerarPosicaoBorda();
        Instantiate(inimigoPrefab, posicao, Quaternion.identity);
    }

    Vector3 GerarPosicaoBorda()
    {
        int borda = Random.Range(0, 4);
        float x, y;

        switch (borda)
        {
            case 0: // cima
                x = Random.Range(xMin, xMax);
                y = yMax;
                break;
            case 1: // baixo
                x = Random.Range(xMin, xMax);
                y = yMin;
                break;
            case 2: // esquerda
                x = xMin;
                y = Random.Range(yMin, yMax);
                break;
            default: // direita
                x = xMax;
                y = Random.Range(yMin, yMax);
                break;
        }

        return new Vector3(x, y, 0);
    }
}
