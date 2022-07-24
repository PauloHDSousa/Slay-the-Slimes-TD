using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField] ParticleSystem particlesVFX;
    [SerializeField] AudioClip onCreateSFX;
    [SerializeField] AudioClip onSellSFX;
    [SerializeField] bool isPoisonTamashi;
    [SerializeField] bool isDamageTamashi;
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

    //Tamashi Controller

    public bool GetIsPoisonTamashi()
    {
        return isPoisonTamashi;
    }

    public bool GetIsDamageTamashi()
    {
        return isDamageTamashi;
    }
}
