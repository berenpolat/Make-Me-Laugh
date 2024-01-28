using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private GameObject Shotgun;
    [SerializeField] private GameObject Ak;
    [SerializeField] private GameObject Pistol;
    [SerializeField] private Transform playerHand;

    private void Start()
    {
        // Ensure initial setup is correct
        DisableAllWeapons();
    }

    public void EnableShotgun()
    {
        Debug.Log("EnableShotgun clicked");
        DisableAllWeapons();
        Shotgun.SetActive(true);
    }

    public void EnableAK()
    {
        Debug.Log("EnableAK clicked");
        DisableAllWeapons();
        Ak.SetActive(true);
    }

    public void EnablePistol()
    {
        Debug.Log("EnablePistol clicked");
        DisableAllWeapons();
        Pistol.SetActive(true);
    }

    private void DisableAllWeapons()
    {
        Shotgun.SetActive(false);
        Ak.SetActive(false);
        Pistol.SetActive(false);
    }
}