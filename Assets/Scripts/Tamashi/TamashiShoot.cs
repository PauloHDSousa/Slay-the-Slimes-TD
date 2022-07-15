using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamashiShoot : MonoBehaviour
{
    TamashiIA towerIA;
    Transform currentTarget;

    [SerializeField] float fireRate = 1f;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    float fireCountDown = 0f;

    void Start()
    {
        towerIA = GetComponent<TamashiIA>();
    }

    void Update()
    {
        currentTarget = towerIA.GetCurrentTarget();
        //If there is no target avaliable, just return;
        if(currentTarget == null)
            return;

        if(fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    void Shoot(){


        GameObject leftBulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet leftBullet = leftBulletInstance.GetComponent<Bullet>();
        if(leftBullet != null)
            leftBullet.SetTarget(currentTarget);
    }
}
