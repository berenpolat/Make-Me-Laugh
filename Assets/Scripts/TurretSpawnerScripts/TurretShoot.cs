using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    private Transform target;  // Hedefin transformu
    public float rotationSpeed = 1000f;  // Taret dönme hýzý

    private float bulletspeed = 70f;
    // Ateþleme parametreleri
    public Transform firePoint;  // Mermi atýþ noktasý
    public GameObject bulletPrefab;  // Mermi prefabý
    public float fireRate = 0.5f;  // Ateþ hýzý
    private float fireCountdown = 0f;  // Ateþe kadar geri sayým

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
        // Hedefi belirlemek için baþlangýçta null atanmýþ olabilir
        if (target == null)
        {
            Debug.LogError("Taretin bir hedefi olmalý!");
        }
    }

    void Update()
    {
        // Hedefe doðru dönme
        LockOnTarget();

        // Ateþ etme kontrolü
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void LockOnTarget()
    {
        // Hedefe doðru dönme
        Vector3 directionToTarget = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        // Mermi oluþturma ve ateþleme
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGO.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletspeed;

    }

}
