using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerScripts
{
    public class PlayerMovements : MonoBehaviour
    {
        public static PlayerMovements Instance; 
        public TurretSpawner turretButton;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveToPosition(Vector3 targetPosition)
        {
            if(!turretButton.isPlacingTurret)
                _navMeshAgent.SetDestination(targetPosition);
           
        }
    }
}
