using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortar : MonoBehaviour
{
    public Transform firePoint; // Fýrlatma noktasý
    public Rigidbody projectilePrefab; // Fýrlatýlacak obje prefabý
    [SerializeField]private float firingAngle; // Açý (derece cinsinden)
    [SerializeField]private float firingSpeed; // Hýz
    [SerializeField]private float detectionRange; // Algýlama mesafesi
    [SerializeField]private float fireRate; // Atýþ hýzý
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

        // Yatay mesafeyi ve yükseklik farkýný hesapla
        float horizontalDistance = Vector3.Distance(new Vector3(targetPosition.x, 0f, targetPosition.z), new Vector3(firePointPosition.x, 0f, firePointPosition.z));
        float verticalDistance = targetPosition.y - firePointPosition.y;

        // Açýyý dereceden radyana çevir ve atanacak açýyý ayarla
        float angle = firingAngle * Mathf.Deg2Rad;

        // Yatay mesafeyi hesapla
        float distance = Vector3.Distance(firePointPosition, targetPosition);

        // Hýzý kullanarak projectileSpeed'i ayarla
        float projectileSpeed = firingSpeed;

        // Fýrlatma kuvveti vektörünü hesapla ve uygula
        Vector3 velocity = projectileSpeed * targetDir.normalized;
        projectileInstance.velocity = velocity;
    }
}
