using System;
using UnityEngine;

namespace TurretBulletSpawn
{
    public class TurretBulletSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private float bulletLifetime = 5f;
        [SerializeField] private float shootingRange = 5f;

        private GameObject targetEnemy;
        private bool canSpawnBullet = true;

        void Update()
        {
            if (canSpawnBullet)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (distance <= shootingRange)
                    {
                        targetEnemy = enemy;
                        SpawnBullet();
                        canSpawnBullet = false; // Set the flag to prevent spawning multiple bullets
                        Invoke("ResetSpawnFlag", spawnInterval); // Reset the flag after spawnInterval seconds
                        break; // Break the loop after spawning one bullet
                    }
                }
            }
        }
        

        void SpawnBullet()
        {
            if (targetEnemy != null)
            {
                transform.LookAt(targetEnemy.transform);
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;

                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.velocity = direction * bulletSpeed;

                Destroy(bullet, bulletLifetime);
            }
        }

        void ResetSpawnFlag()
        {
            canSpawnBullet = true;
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject); // enemy kill
            }
        }

    }
}
