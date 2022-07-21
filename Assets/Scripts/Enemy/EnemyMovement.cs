using UnityEngine;

public class EnemyMovement : MonoBehaviour
{ 
    Transform targetPoint;
    int currentPoint = 0;

    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        targetPoint = WayPoints.wayPoints[0];
    }

    void Update()
    {
        Vector3 dir = targetPoint.position - transform.position;
        dir.y = 0;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        
        //Look
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Vector3.Distance(transform.position, targetPoint.position) <= 0.2f)
            GoToNextPoint();

        enemy.speed = enemy.startSpeed; 
    }

    void GoToNextPoint()
    {
        currentPoint++;
        if (currentPoint >= WayPoints.wayPoints.Length)
        {
            Destroy(this.gameObject);
            return;
        }

        targetPoint = WayPoints.wayPoints[currentPoint];
    }
}
