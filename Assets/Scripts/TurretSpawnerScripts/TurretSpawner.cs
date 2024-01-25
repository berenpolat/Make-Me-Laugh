using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    [SerializeField] private GameObject TurretPrefab;
    private GameObject turretPreview;
    public bool isPlacingTurret = false;

    public void ToggleTurretPlacement()
    {
        isPlacingTurret = !isPlacingTurret;

        if (isPlacingTurret)
        {
            CreateTurretPreview();
        }
        else
        {
            DestroyTurretPreview();
        }
    }

    private void CreateTurretPreview()
    {
        turretPreview = Instantiate(TurretPrefab, Vector3.zero, Quaternion.identity);
        turretPreview.SetActive(false); // Disable the preview initially
    }

    public void UpdateTurretPreview(Vector3 position)
    {
        if (turretPreview != null)
        {
            turretPreview.SetActive(true);
            turretPreview.transform.position = position;
        }
    }

    private void DestroyTurretPreview()
    {
        if (turretPreview != null)
        {
            Destroy(turretPreview);
        }
    }

    public void PlaceTurret(Vector3 position)
    {
        ToggleTurretPlacement();
        Instantiate(TurretPrefab, position, Quaternion.identity);
    }
}