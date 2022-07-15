using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    TowerIA towerIA;
    Transform currentTarget;


    [Header("Bullets")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 1f;
    float fireCountDown = 0f;

    [Header("Laser")]
    [SerializeField] bool useLaserShot = false;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int damageOverTime = 30;
    [SerializeField] float slowAmount = .5f;



    void Start()
    {
        towerIA = GetComponent<TowerIA>();
    }

    void Update()
    {
        currentTarget = towerIA.GetCurrentTarget();
        //If there is no target avaliable, just return;
        if (currentTarget == null) {
            lineRenderer.enabled = false;
            return;
        }

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

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        if (bullet != null)
            bullet.SetTarget(currentTarget);
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

    public bool IsLaserShot()
    {
        return useLaserShot;
    }
}
