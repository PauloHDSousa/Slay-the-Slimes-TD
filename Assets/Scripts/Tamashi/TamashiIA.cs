using UnityEngine;

public class TamashiIA : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float turnSpeed = 10f;


    [SerializeField] float fireRate;

    Transform target;
    Quaternion startRotation= Quaternion.identity;
    public Transform GetCurrentTarget()
    {
        return target;
    }

    void Start()
    {
        startRotation = transform.rotation;
        //Every half seconds try to get the closest target
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    void UpdateTarget()
    {
        //Get all enemies in the scene and check if the enemy is inside the range.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }
    void Update()
    {
        if (target == null)
        {
            //Return to the original position
            Vector3 goBackToRotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, goBackToRotation.y, 0f);
            return;
        }

        //Rotate towards the closest enemy
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
