using UnityEngine;

public class ShardSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject shardPrefab;

    [Header("Configurações de Spawn")]
    public int maxShardsTela = 5;
    public float intervaloSpawn = 3f;

    [Header("Limites do Mapa")]
    public float xMin = -6.5f;
    public float xMax = 6.5f;
    public float yMin = -3.5f;
    public float yMax = 3.5f;

    private float timerSpawn = 0f;

    void Update()
    {
        if (!GameManager.Instance.jogoAtivo) return;

        timerSpawn += Time.deltaTime;

        if (timerSpawn >= intervaloSpawn)
        {
            timerSpawn = 0f;
            VerificarESpawnar();
        }
    }

    void VerificarESpawnar()
    {
        // Conta quantos shards existem na cena atualmente
        int shardsNaTela = GameObject.FindGameObjectsWithTag("Coletavel").Length;

        if (shardsNaTela < maxShardsTela)
        {
            SpawnarShard();
        }
    }

    void SpawnarShard()
    {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        Vector3 posicao = new Vector3(x, y, 0);

        Instantiate(shardPrefab, posicao, Quaternion.identity);
    }
}