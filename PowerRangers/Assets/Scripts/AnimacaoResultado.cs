using UnityEngine;
using UnityEngine.UI;

public class AnimacaoResultado : MonoBehaviour
{
    [Header("Animação")]
    public RawImage imagemAnimacao;
    public Texture2D spriteSheet;
    public int colunas = 5;
    public int linhas = 50;
    public float frameRate = 10f;

    [Header("UI")]
    public GameObject canvasAnimacao;
    public GameObject canvasResultado;

    private float timerFrame = 0f;
    private int frameAtual = 0;
    private int totalFrames;
    private float larguraFrame;
    private float alturaFrame;
    private bool animando = true;

    void Start()
    {
        totalFrames = colunas * linhas;
        larguraFrame = 1f / colunas;
        alturaFrame = 1f / linhas;

        // Mostra animação e esconde resultado
        canvasAnimacao.SetActive(true);
        canvasResultado.SetActive(false);

        AtualizarFrame();
    }

    void Update()
    {
        if (!animando) return;

        timerFrame += Time.deltaTime;

        if (timerFrame >= 1f / frameRate)
        {
            timerFrame = 0f;
            frameAtual++;

            if (frameAtual >= totalFrames)
            {
                FinalizarAnimacao();
                return;
            }

            AtualizarFrame();
        }
    }

    void AtualizarFrame()
    {
        int coluna = frameAtual % colunas;
        int linha = frameAtual / colunas;

        float x = coluna * larguraFrame;
        float y = 1f - ((linha + 1) * alturaFrame);

        imagemAnimacao.uvRect = new Rect(x, y, larguraFrame, alturaFrame);
        imagemAnimacao.texture = spriteSheet;
    }

    public void PularAnimacao()
    {
        FinalizarAnimacao();
    }

    void FinalizarAnimacao()
    {
        animando = false;
        canvasAnimacao.SetActive(false);
        canvasResultado.SetActive(true);
    }
}
