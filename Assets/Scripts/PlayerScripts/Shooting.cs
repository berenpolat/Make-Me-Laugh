using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public enum BulletType
    {
        Shotgun = 0,
        Rifle = 1,
        Pistol = 2,
        Mortar = 3,
    }
    public Transform bulletSpawnPoint;

    public GameObject bulletPrefab;

    [SerializeField]private float shootCooldown;
    [SerializeField] private BulletType Type;
    public float bulletSpeed = 300;
    private float lastShotTime;
    private float bulletDestroyTime = 1.5f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && Time.time > lastShotTime + shootCooldown)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        Destroy(bullet, bulletDestroyTime);
        lastShotTime = Time.time;
    }
    
    
}
