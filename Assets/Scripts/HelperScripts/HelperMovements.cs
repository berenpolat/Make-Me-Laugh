using System;
using System.Collections;
using System.Collections.Generic;
using BodyScripts;
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
    private GameObject player;

    void Start()
    {
        transform.Rotate(180, 0, 0);
        towerTransform = GameObject.FindGameObjectWithTag(towerTag).transform;
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void Update()
    {
        int i = 2;

        if (!isCollidingWithBody)
        {

            GameObject body = FindClosestBody();

            if (player != null && !isCollidingWithBody)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(player.transform.position.x + i, player.transform.position.y,
                        player.transform.position.z), speed * Time.deltaTime);
                i += 2;
            }

            if (body != null && !isCollidingWithBody)
            {
                float distanceToBody = Vector3.Distance(transform.position, body.transform.position);

                currentBody = body;
                transform.position = Vector3.MoveTowards(transform.position, body.transform.position,
                        speed * 5f * Time.deltaTime);
            }
        }
        else
        {
            // Move towards the tower when carrying a body
             if (currentBody != null)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, towerTransform.position, speed * Time.deltaTime);

                float distanceToTower = Vector3.Distance(transform.position, towerTransform.position);

                if (distanceToTower < 0.1f)
                {
                    isCollidingWithBody = false;
                    currentBody = null;
                }
            }
        }
    }


GameObject FindClosestBody()
    {
        GameObject[] bodies = GameObject.FindGameObjectsWithTag(bodyTag);
        GameObject closestBody = null;
        float closestDistance = 10f;

        foreach (GameObject body in bodies)
        {
            var b = body.GetComponent<Body>();
            if (b.IsAssigned)
            {
                continue;
            }
            float distance = Vector3.Distance(transform.position, body.transform.position);

            if (distance < closestDistance)
            {
                closestBody = body;
                b.AssignHelper(this);
                break;
            }
        }
        return closestBody;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(bodyTag))
        {
            isCollidingWithBody = true;
        }
        if (other.CompareTag(towerTag))
        {
            Destroy(currentBody);
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            isCollidingWithBody = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(bodyTag))
        {
            isCollidingWithBody = false;
        }
    }
    
}
