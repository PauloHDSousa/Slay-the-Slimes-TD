using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;


    [Header("VFX")]
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] ParticleSystem damageEffect;
    [SerializeField] GameObject Body;

    [Header("Health Bar")]
    [SerializeField] Image healthBar;
    [SerializeField] float enemyHealth = 100;

    [Header("Attr")]
    [SerializeField] public float enemySpeed = 3f;
    [SerializeField] int goldWorth = 15;
    [SerializeField] public float poisonDamage = 20f;


    [Header("Sound")]
    [SerializeField] AudioClip enemyDeathSFX;

    bool isDead = false;
    float health;

    CurrencyManager currencyManager;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currencyManager = FindObjectOfType<CurrencyManager>();
        speed = enemySpeed;
        health = enemyHealth;
    }

    public void TakeDamage(float amount, bool takePoisonDamage = false)
    {
        health -= amount;
        healthBar.fillAmount = health / enemyHealth;

        if (health <= 0 && !isDead)
            Die();
        else
            Instantiate(damageEffect, transform.position, Quaternion.identity);

        if (takePoisonDamage)
            InvokeRepeating("TakePoisonDamage", 0f, .5f);

    }

    public void TakePoisonDamage()
    {
        float amount = poisonDamage;
        health -= amount;
        healthBar.fillAmount = health / enemyHealth;

        if (health <= 0 && !isDead)
            Die();
    }

    public void Slow(float pct)
    {
        speed = enemySpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;
        currencyManager.WonGold(goldWorth);
        audioSource.PlayOneShot(enemyDeathSFX);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Body.SetActive(false);
        Destroy(gameObject, enemyDeathSFX.length);
    }

    public bool IsDead()
    {
        return isDead;
    }
}