using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;  // dalga ismi
    public int noOfEnemies; // düþman sayýsý
    public GameObject[] typeOfEnemies; // düþman türleri
    public GameObject boss;
    public GameObject[] gifts; // hediyeler
    public float spawnInterval; //spawn aralýðý  
}
public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
   
    public Animator animator;
    public Text waveName;


    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private float destroyGiftTime = 1f;
    private bool canSpawn = true;
    private bool canAnimate = false;
    private bool bossSpawned = false;
    
    
    // Update is called once per frame
    void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0)
        {
            if (currentWaveNumber + 1 != waves.Length)
            {
                if (canAnimate)
                {
                    waveName.text = waves[currentWaveNumber + 1].waveName;
                    animator.SetTrigger("WaveComplate");
                    canAnimate = false;
                    bossSpawned = false;
                    
                }
            }

            else
            {
                Debug.Log("GameFinish");
            }

        }

    }
    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
        SpawnGift(); // Hediye spawn et
    }
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
           
            GameObject Bosses = currentWave.boss;
           
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
           if (!bossSpawned && currentWave.boss != null)
           {
               Instantiate(currentWave.boss, randomPoint.position, Quaternion.identity);
               bossSpawned = true;
           }
       

            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }

        }
    }
    void SpawnGift()
    {
        // Eðer dalga için hediye tanýmlanmamýþsa, bu metodu atla
        if (currentWave.gifts == null || currentWave.gifts.Length == 0)
            return;

        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject randomGift = currentWave.gifts[Random.Range(0, currentWave.gifts.Length)]; // Rastgele hediye seç
        GameObject spawnedGift = Instantiate(randomGift, randomPoint.position, Quaternion.identity); // Hediye spawn et
        StartCoroutine(DestroyGiftAfterDelay(spawnedGift, 10f)); // Hediyeyi belirli bir süre sonra yok et
        IEnumerator DestroyGiftAfterDelay(GameObject gift, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (gift != null)
            {
                Destroy(gift);
            }
        }
    }

}
