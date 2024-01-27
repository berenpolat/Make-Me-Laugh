using System;
using UnityEngine;

namespace BodyScripts
{
    public class Body : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        private HelperFeatures currentHelper;
        private bool isAssigned;
        private float givenHappinessLevelToPlayer;

        public bool IsAssigned => isAssigned;

        public float GivenHappinessLevelToPlayer
        {
            get => givenHappinessLevelToPlayer;
            set => givenHappinessLevelToPlayer = value;
        }

        public void AssignHelper(HelperFeatures helper)
        {
            if(helper != null)
                currentHelper = helper;
        }

        public void MakeAssignStatusTrue()
        {
            isAssigned = true;
        }

        private void FixedUpdate()
        {
            if(!currentHelper)
                return;
            if (currentHelper.IsCollidingWithBody)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentHelper.transform.position, speed * Time.deltaTime);
            }
        }
    }
}