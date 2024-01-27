using UnityEngine;

namespace TurretSpawnerScripts
{
    public class FloorClickHandler : MonoBehaviour
    {
        public TurretSpawner turretButton;
        public TurretSpawner turretButton2;

        
        void Update()
        {
            if (turretButton.isPlacingTurret)
            {
                UpdateTurretPreview();
            }
            
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
        
        private void UpdateTurretPreview()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                turretButton.UpdateTurretPreview(new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z));
            }
        }
    }
}
