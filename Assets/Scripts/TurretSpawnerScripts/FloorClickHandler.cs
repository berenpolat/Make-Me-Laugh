using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorClickHandler : MonoBehaviour
{
    public TurretSpawner turretButton;

    void Update()
    {
        if (turretButton.isPlacingTurret && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                turretButton.PlaceTurret(new Vector3(hit.point.x,hit.point.y+0.5f,hit.point.z));
            }
        }
    }
}
