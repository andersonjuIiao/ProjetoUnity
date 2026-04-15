using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void PerderVida()
    {
        // Ranger é invulnerável
        if (playerMovement.estado == PlayerMovement.PlayerState.Ranger) return;
        // Durante transformação também é invulnerável
        if (playerMovement.estado == PlayerMovement.PlayerState.Transformando) return;

        GameManager.Instance.PerderVida();
    }
}