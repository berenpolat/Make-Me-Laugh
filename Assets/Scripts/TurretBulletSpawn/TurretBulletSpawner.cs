using System.Collections;
using UnityEngine;

public class TurretBulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    public float bulletSpeed = 10f;
    public float spawnInterval = 10f;
    public float bulletLifetime = 5f;

    void Start()
    {
        InvokeRepeating("SpawnBullet", 0f, spawnInterval);
    }

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Vector3 direction = (enemyPrefab.transform.position - transform.position).normalized;

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = direction * bulletSpeed;

        Destroy(bullet, bulletLifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);//enem kill
        }
    }
}