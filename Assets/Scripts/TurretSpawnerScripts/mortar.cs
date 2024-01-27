using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortar : MonoBehaviour
{
    public Transform firePoint; // F�rlatma noktas�
    public Rigidbody projectilePrefab; // F�rlat�lacak obje prefab�
    [SerializeField]private float firingAngle; // A�� (derece cinsinden)
    [SerializeField]private float firingSpeed; // H�z
    [SerializeField]private float detectionRange; // Alg�lama mesafesi
    [SerializeField]private float fireRate; // At�� h�z�
    private float nextFireTime = 0f; // Sonraki at�� zaman�
    private Transform currentTarget; // Aktif hedef

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            // Düşmanları güncellemek için FindTarget fonksiyonunu çağır
            FindTarget();
            if (currentTarget != null)
            {
                FireAtTarget(currentTarget.position);
                nextFireTime = Time.time + 1f / fireRate;
                if (currentTarget != null)
                {
                    Vector3 directionToEnemy = (currentTarget.position -transform.position);
                    directionToEnemy.y = 0; // Ignore vertical component

                    Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

                    // Set the head's rotation without interpolation
                    transform.rotation = lookRotation;
                }
            }
        }
    }
   

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= detectionRange && distance < closestDistance)
            {
                closestDistance = distance;
                currentTarget = enemy.transform;
            }
        }
    }

    void FireAtTarget(Vector3 targetPosition)
    {
        Vector3 firePointPosition = firePoint.position;

        // Calculate the direction towards the target
        Vector3 targetDir = targetPosition - firePointPosition;

        // Calculate horizontal distance
        float horizontalDistance = Vector3.Distance(new Vector3(targetPosition.x, 0f, targetPosition.z), new Vector3(firePointPosition.x, 0f, firePointPosition.z));

        // Calculate vertical distance
        float verticalDistance = targetPosition.y - firePointPosition.y;

        // Calculate the firing angle based on horizontal and vertical distances
        float angle = Mathf.Atan((verticalDistance + horizontalDistance * Mathf.Tan(firingAngle * Mathf.Deg2Rad)) / horizontalDistance);

        // Calculate the distance to the target
        float distance = Vector3.Distance(firePointPosition, targetPosition);

        // Calculate the initial velocity components
        float projectileSpeed = firingSpeed;
        float initialVelocityX = Mathf.Sqrt(projectileSpeed * projectileSpeed / (1 + Mathf.Tan(angle) * Mathf.Tan(angle)));
        float initialVelocityY = initialVelocityX * Mathf.Tan(angle);

        // Calculate the initial velocity vector
        Vector3 velocity = targetDir.normalized * initialVelocityX;
        velocity.y = initialVelocityY;

        // Instantiate the projectile and set its velocity
        Rigidbody projectileInstance = Instantiate(projectilePrefab, firePointPosition, Quaternion.identity);
        projectileInstance.velocity = velocity;
    }
}
