using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortar : MonoBehaviour
{
    public Transform firePoint; // Fýrlatma noktasý
    public Rigidbody projectilePrefab; // Fýrlatýlacak obje prefabý
    public float firingAngle = 45f; // Açý
    public float firingSpeed = 20f; // Hýz
    public float detectionRange = 10f; // Algýlama mesafesi
    public float fireRate = 1f; // Atýþ hýzý
    private float nextFireTime = 0f; // Sonraki atýþ zamaný
    private Transform currentTarget; // Aktif hedef

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            FindTarget();
            if (currentTarget != null)
            {
                FireAtTarget(currentTarget.position);
                nextFireTime = Time.time + 1f / fireRate;
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
        // Fire point'te bir pozisyon oluþtur
        Vector3 firePointPosition = firePoint.position;

        // Objeyi fýrlatma iþlemi
        Rigidbody projectileInstance = Instantiate(projectilePrefab, firePointPosition, Quaternion.identity);

        // Hedefe doðru vektörü hesapla
        Vector3 targetDir = targetPosition - firePointPosition;

        // Açýyý dereceden radyana çevir
        float angle = firingAngle * Mathf.Deg2Rad;

        // Yatay mesafeyi hesapla
        float distance = Vector3.Distance(firePointPosition, targetPosition);

        // Gerekli hýzý hesapla
        float projectileSpeed = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle));

        // Fýrlatma kuvveti vektörünü hesapla ve uygula
        Vector3 velocity = projectileSpeed * targetDir.normalized;
        projectileInstance.velocity = velocity;
    }
}
