using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Efeitos Sonoros")]
    public AudioClip somDano;
    public AudioClip somInimigoMorto;
    public AudioClip somDisparo;
    public AudioClip somColeta;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void TocarDano()
    {
        audioSource.PlayOneShot(somDano);
    }

    public void TocarInimigoMorto()
    {
        audioSource.PlayOneShot(somInimigoMorto);
    }

    public void TocarDisparo()
    {
        audioSource.PlayOneShot(somDisparo);
    }

    public void TocarColeta()
    {
        audioSource.PlayOneShot(somColeta);
    }
}