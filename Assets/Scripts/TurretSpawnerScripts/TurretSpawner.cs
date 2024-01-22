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
        ToggleTurretPlacement();
        Instantiate(Turret, position, Quaternion.identity);
    }
}
