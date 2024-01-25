using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public float spawnInterval = 2f;
        public int maxEnemies = 10;

        private void Start()
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }
    
        void SpawnEnemy()
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                Instantiate(enemyPrefab, transform.position, transform.rotation);
            }
        }
    }
}

