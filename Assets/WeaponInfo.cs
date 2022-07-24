using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField] ParticleSystem particlesVFX;
    [SerializeField] AudioClip onCreateSFX;
    [SerializeField] AudioClip onSellSFX;
    public ParticleSystem GetParticlesVFX()
    {
        return particlesVFX;
    }

    public AudioClip GetOnCreateSFX()
    {
        return onCreateSFX;
    }

    public AudioClip GetOnSellSFX()
    {
        return onSellSFX;
    }
}
