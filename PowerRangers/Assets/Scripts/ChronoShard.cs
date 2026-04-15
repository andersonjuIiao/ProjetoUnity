using UnityEngine;

public class ChronoShard : MonoBehaviour
{
    [Header("Movimento")]
    public float amplitudeFlutuar = 0.1f;
    public float velocidadeFlutuar = 2f;

    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        // Faz o shard flutuar levemente para cima e para baixo
        float novoY = posicaoInicial.y + Mathf.Sin(Time.time * velocidadeFlutuar) * amplitudeFlutuar;
        transform.position = new Vector3(posicaoInicial.x, novoY, posicaoInicial.z);
    }
}
