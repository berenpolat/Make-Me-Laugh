using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    enum GunType
    {
        Shotgun = 0,
        Rifle = 1,
        Pistol = 2,
    }
    public Transform bulletSpawnPoint;

    public GameObject bulletPrefab;

    [SerializeField]private float shootCooldown;
    public float bulletSpeed = 300;
    private float lastShotTime;
    private float bulletDestroyTime = 1f;
    [SerializeField] private float damageAmount;
    [SerializeField] private GunType Type;
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
