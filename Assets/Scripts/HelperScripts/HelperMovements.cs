using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMovements : MonoBehaviour
{
    public float speed;
    public float bodyRange = 5f;
    public string playerTag = "Player";
    public string bodyTag = "Body";
    public string towerTag = "Tower";

    [HideInInspector] public bool isCollidingWithBody = false;

    private GameObject currentBody;
    private Transform towerTransform;

    private float initialYOffset;
    private float yOffset = 3f; // Adjusted Y offset

    void Start()
    {
        transform.Rotate(180, 0, 0);
        towerTransform = GameObject.FindGameObjectWithTag(towerTag).transform;

        // Store the initial Y position to use as a reference for the offset
        initialYOffset = transform.position.y;
    }

    void Update()
    {
        GameObject tower = GameObject.FindGameObjectWithTag(towerTag);
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (!isCollidingWithBody)
        {
            GameObject body = FindClosestBody();

            if (player != null && !isCollidingWithBody)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            if (body != null && !isCollidingWithBody)
            {
                float distanceToBody = Vector3.Distance(transform.position, body.transform.position);

                if (distanceToBody <= bodyRange)
                {
                    currentBody = body;
                    transform.position = Vector3.MoveTowards(transform.position, body.transform.position, speed * 5f * Time.deltaTime);
                }
            }
        }
        else
        {
            // Move towards the tower when carrying a body
            if (currentBody != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, towerTransform.position, speed * Time.deltaTime);

                float distanceToTower = Vector3.Distance(transform.position, towerTransform.position);

                if (distanceToTower < 0.1f) // You can adjust this threshold as needed
                {
                    isCollidingWithBody = false;
                    currentBody = null;
                }
            }
        }

        // Adjust Y-position to maintain a fixed offset
        transform.position = new Vector3(transform.position.x, initialYOffset + yOffset, transform.position.z);
    }

    GameObject FindClosestBody()
    {
        GameObject[] bodies = GameObject.FindGameObjectsWithTag(bodyTag);
        GameObject closestBody = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject body in bodies)
        {
            float distance = Vector3.Distance(transform.position, body.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestBody = body;
            }
        }

        return closestBody;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(bodyTag))
        {
            isCollidingWithBody = true;
        }

        if (other.gameObject.CompareTag(towerTag))
        {
            isCollidingWithBody = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(bodyTag))
        {
            isCollidingWithBody = false;
        }
    }
}
