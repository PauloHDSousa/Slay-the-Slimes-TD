using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform currentTarget;

    [SerializeField] float speed = 70f;
    [SerializeField] float explosionRadius = 0f;
    [SerializeField] GameObject impactEffect;
    [SerializeField] bool isPoisonBullet = false;


    float damage;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetTarget(Transform _currentTarget)
    {
        currentTarget = _currentTarget;
    }

    void Update()
    {
        if (currentTarget == null)
        {
            Destroy(gameObject);
            return;
        }
        var aimToEnemyAt = new Vector3(currentTarget.position.x, currentTarget.position.y + .5f, currentTarget.position.z);

        Vector3 dir = aimToEnemyAt - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //GameObject instantiatedImpactEffect = Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(instantiatedImpactEffect, 1f);

        if (explosionRadius > 0f)
            Explode();
        else
            Damage(currentTarget);
        
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
            if (collider.CompareTag("Enemy"))
                Damage(collider.transform);
    }

    void Damage(Transform _enemy)
    {
        var enemy = _enemy.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(damage, isPoisonBullet);
    }
}
