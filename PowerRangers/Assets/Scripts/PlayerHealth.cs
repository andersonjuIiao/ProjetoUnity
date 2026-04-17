using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [Header("Configurações")]
    public float cooldownDano = 1f;
    private float timerCooldown = 0f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (timerCooldown > 0)
            timerCooldown -= Time.deltaTime;
    }

    public void PerderVida()
    {
        if (playerMovement.estado == PlayerMovement.PlayerState.Ranger) return;
        if (playerMovement.estado == PlayerMovement.PlayerState.Transformando) return;
        if (timerCooldown > 0) return;

        timerCooldown = cooldownDano;
        AudioManager.Instance?.TocarDano();
        GameManager.Instance.PerderVida();
    }
}