using UnityEngine;
using UnityEngine.UI;

public class TransformationController : MonoBehaviour
{
    public static TransformationController Instance;

    [Header("Canvas de Transformação")]
    public GameObject canvasTransformacao;
    public RawImage imagemTransformacao;

    [Header("Sprite Sheet")]
    public Texture2D spriteSheet;
    public int colunas = 5;
    public int linhas = 12;
    public float frameRate = 12f;

    [Header("Audio")]
    public AudioClip audioMorfagem;
    private AudioSource audioSource;

    private float timerFrame = 0f;
    private int frameAtual = 0;
    private int totalFrames;
    private bool animando = false;
    private float larguraFrame;
    private float alturaFrame;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        totalFrames = colunas * linhas;
        larguraFrame = 1f / colunas;
        alturaFrame = 1f / linhas;
    }

    void Update()
    {
        if (!animando) return;

        timerFrame += Time.unscaledDeltaTime;

        if (timerFrame >= 1f / frameRate)
        {
            timerFrame = 0f;
            frameAtual++;

            if (frameAtual >= totalFrames)
            {
                FinalizarTransformacao();
                return;
            }

            AtualizarFrame();
        }
    }

    public void IniciarAnimacao()
    {
        frameAtual = 0;
        animando = true;
        canvasTransformacao.SetActive(true);
        Time.timeScale = 0f;
        AtualizarFrame();

        if (audioSource != null && audioMorfagem != null)
            audioSource.PlayOneShot(audioMorfagem);
    }

    void AtualizarFrame()
    {
        int coluna = frameAtual % colunas;
        int linha = frameAtual / colunas;

        float x = coluna * larguraFrame;
        float y = 1f - ((linha + 1) * alturaFrame);

        imagemTransformacao.uvRect = new Rect(x, y, larguraFrame, alturaFrame);
        imagemTransformacao.texture = spriteSheet;
    }

    public void PularAnimacao()
    {
        if (audioSource != null)
            audioSource.Stop();

        FinalizarTransformacao();
    }

    void FinalizarTransformacao()
    {
        animando = false;
        canvasTransformacao.SetActive(false);
        Time.timeScale = 1f;

        FindFirstObjectByType<PlayerMovement>().AtivarRanger();
    }
}