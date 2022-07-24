using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    public GameObject deathEffect;

    [Header("Health Bar")]
    [SerializeField] Image healthBar;
    [SerializeField] float enemyHealth = 100;

    [Header("Attr")]
    [SerializeField] public float enemySpeed = 3f;
    [SerializeField] int goldWorth = 15;
    [SerializeField] public float poisonDamage = 20f;

    bool isDead = false;
    float health;

    CurrencyManager currencyManager;

    void Start()
    {
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

        if(takePoisonDamage)
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

        //GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 1f);

        Destroy(gameObject);
    }

}