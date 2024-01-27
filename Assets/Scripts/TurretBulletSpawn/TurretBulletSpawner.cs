using System;
using DG.Tweening;
using UnityEngine;

namespace TurretBulletSpawn
{
    public class TurretBulletSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private float spawnInterval = 10f;
        [SerializeField] private float bulletLifetime = 5f;
        [SerializeField] private float shootingRange = 5f;
        [SerializeField] private GameObject head;
        [SerializeField] private float rotationSpeed;
        
        private GameObject targetEnemy;

        void Start()
        {
            InvokeRepeating("CheckForEnemy", 0f, spawnInterval);
        }

        void CheckForEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance <= shootingRange)
                {
                    targetEnemy = enemy;

                    

                    SpawnBullet();
                    break; // Break the loop after spawning one bullet
                }
            }
        }
   
        private void Update()
        {
            if (targetEnemy != null)
            {
                Vector3 directionToEnemy = (targetEnemy.transform.position - head.transform.position);
                directionToEnemy.y = 0; // Ignore vertical component

                Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

                // Set the head's rotation without interpolation
                head.transform.rotation = lookRotation;
            }
        }



        void SpawnBullet()
        {
            if (targetEnemy != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;

                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.velocity = direction * bulletSpeed;

                Destroy(bullet, bulletLifetime);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject); // enemy kill
            }
        }
    }
}