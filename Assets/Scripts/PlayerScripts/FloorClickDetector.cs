using UnityEngine;

namespace PlayerScripts
{
    public class FloorClickDetector : MonoBehaviour
    {
   
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
            
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")) )
                {
                    PlayerMovements.Instance.MoveToPosition(hit.point);
                }
            }
        }
    }
}