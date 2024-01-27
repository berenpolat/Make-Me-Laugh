using System;
using UnityEngine;

namespace BodyScripts
{
    public class Body : MonoBehaviour
    {
        private HelperMovements currentHelper;
        [SerializeField] private float speed = 5f;
        private bool isAssigned = false; // New boolean flag

        public bool IsAssigned => isAssigned;

        public void AssignHelper(HelperMovements helper)
        {
            currentHelper = helper;
            isAssigned = true;
        }
        

        private void Update()
        {
            if (currentHelper != null && currentHelper.isCollidingWithBody)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentHelper.transform.position, speed * Time.deltaTime);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Helper"))
            {
                currentHelper = other.gameObject.GetComponent<HelperMovements>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Helper"))
            {
                currentHelper = null;
            }
        }

    }
}