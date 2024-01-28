using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject pistol;
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
        shotgun.SetActive(true);
    }

    public void EnableAK()
    {
        Debug.Log("EnableAK clicked");
        DisableAllWeapons();
        ak.SetActive(true);
    }

    public void EnablePistol()
    {
        Debug.Log("EnablePistol clicked");
        DisableAllWeapons();
        pistol.SetActive(true);
    }

    private void DisableAllWeapons()
    {
        shotgun.SetActive(false);
        ak.SetActive(false);
        pistol.SetActive(false);
    }
}
