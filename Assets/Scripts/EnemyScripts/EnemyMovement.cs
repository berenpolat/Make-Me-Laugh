using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform tower;
        NavMeshAgent _agent;
        
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            _agent.destination = tower.position;
        }
    }
}