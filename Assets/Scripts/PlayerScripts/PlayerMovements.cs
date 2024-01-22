using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    public static PlayerMovements Instance; 
    public TurretSpawner turretButton;
    

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        if(!turretButton.isPlacingTurret)
            navMeshAgent.SetDestination(targetPosition);
    }
}
