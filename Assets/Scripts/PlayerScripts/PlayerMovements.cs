using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerScripts
{
    public class PlayerMovements : MonoBehaviour
    {
        public static PlayerMovements Instance;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

            MovePlayer(movement);
        }

        private void MovePlayer(Vector3 movement)
        {
            float speed = 5f;
            transform.Translate(movement * speed * Time.deltaTime);
        }

        
        public void MoveToPosition(Vector3 targetPosition)
        {
            
        }
    }
}