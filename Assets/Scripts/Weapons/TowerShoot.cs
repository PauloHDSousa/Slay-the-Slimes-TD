using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    TowerIA towerIA;
    Transform currentTarget;


    [Header("Bullets")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject poisonBulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 1f;
    [SerializeField] int bulletDamage = 50;
    [SerializeField] AudioClip shootSFX;
    float fireCountDown = 0f;

    [Header("Laser")]
    [SerializeField] bool useLaserShot = false;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int damageOverTime = 25;
    [SerializeField] float slowAmount = .3f;
    [SerializeField] AudioClip laserSFX;

    public GameObject currentBullet;


    //PowerUp Tamashi
    string tagTamashiDamage = "TamashiDamage";
    string tagTamashiPoison = "TamashiPoison";


    AudioSource audioSource;
    void Start()
    {
        towerIA = GetComponent<TowerIA>();
        GameObject[] damageTamashis = GameObject.FindGameObjectsWithTag(tagTamashiDamage);
        foreach (var damage in damageTamashis)
            UpgradeTowersWithTamashi();

        GameObject poisonTamashi = GameObject.FindGameObjectWithTag(tagTamashiPoison);
        if (poisonTamashi != null)
            currentBullet = poisonBulletPrefab;
        else
            currentBullet = bulletPrefab;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        currentTarget = towerIA.GetCurrentTarget();
        //If there is no target avaliable, just return;
        if (currentTarget == null)
        {
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;

                if (useLaserShot)
                    audioSource.Stop();
            }
            return;
        }
        if (!audioSource.isPlaying && useLaserShot)
            audioSource.Play();

        if (useLaserShot)
            Laser();
        else
        {

            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
    }

    #region Actions
    void Shoot()
    {
        GameObject bulletInstance = Instantiate(currentBullet, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        if (bullet != null)
        {
            audioSource.PlayOneShot(shootSFX);
            bullet.SetTarget(currentTarget);
            bullet.SetDamage(bulletDamage);
        }
    }

    void Laser()
    {
        var enemey = currentTarget.GetComponent<Enemy>();
        enemey.TakeDamage(damageOverTime * Time.deltaTime);
        enemey.Slow(slowAmount);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, currentTarget.position);
    }
    #endregion

    #region Upgrades

    public void UpgradeTower()
    {
        fireRate += .5f;
        slowAmount += .1f;
        bulletDamage += (bulletDamage / 2);
    }

    public void UpgradeTowersWithTamashi()
    {
        fireRate += .5f;
        slowAmount += .1f;
        bulletDamage += (bulletDamage / 2);
    }

    public void UpgradeTowerWIthPoisonTamashi()
    {
        currentBullet = poisonBulletPrefab;
    }

    #endregion

    #region Utils
    public bool IsLaserShot()
    {
        return useLaserShot;
    }
    #endregion
}
