using UnityEngine;

namespace BodyScripts
{
    public class Body : MonoBehaviour
    {
        private HelperMovements currentHelper;
        [SerializeField] private float speed = 5f;

        private void Update()
        {
            if (currentHelper != null && currentHelper.isCollidingWithBody)
            {
                // Follow the current helper when colliding
                transform.position = Vector3.MoveTowards(transform.position, currentHelper.transform.position, speed * Time.deltaTime);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Helper"))
            {
                // Set the current helper to the one it is colliding with
                currentHelper = collision.gameObject.GetComponent<HelperMovements>();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Helper"))
            {
                // Reset the current helper when no longer colliding
                currentHelper = null;
            }
        }
    }
}