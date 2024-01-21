using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Turret;
    public bool isPlacingTurret = false;
    
    public void ToggleTurretPlacement()
    {
        isPlacingTurret = !isPlacingTurret;
    }
    
    public void PlaceTurret(Vector3 position)
    {
        Instantiate(Turret, position, Quaternion.identity);
        ToggleTurretPlacement();
    }
}
