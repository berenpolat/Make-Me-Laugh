using System;
using System.Collections;
using System.Collections.Generic;
using BodyScripts;
using UnityEngine;

public class HelperFeatures : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject childTower;

    private float moveSpeed = 10f;
    private float maxDistanceFromBody = 5f;
    private Body currentBody;
    private Collider[] collidersInsideRadius;
    private bool isCollidingWithBody;
    private bool isAbleToSearchForBody = true;
    
    public bool IsCollidingWithBody => isCollidingWithBody;

    private void FixedUpdate()
    {
        SearchForBody();
        if (isAbleToSearchForBody && !isCollidingWithBody)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (!isAbleToSearchForBody && !isCollidingWithBody)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentBody.gameObject.transform.position,
                moveSpeed * Time.deltaTime);
        }
        else if (!isAbleToSearchForBody && isCollidingWithBody)
        {
            transform.position = Vector3.MoveTowards(transform.position, childTower.transform.position,
                moveSpeed * Time.deltaTime);
            
            float distance = Vector3.Distance(transform.position, childTower.transform.position);

            if (distance < 0.1f)
            {
                Destroy(currentBody.gameObject);
                isAbleToSearchForBody = true;
                isCollidingWithBody = false;
            }
        }
    }

    private void SearchForBody()
    {
        if(!isAbleToSearchForBody)
            return;
        collidersInsideRadius = Physics.OverlapSphere(transform.position, maxDistanceFromBody);
        foreach (var collider in collidersInsideRadius)
        {
            if (collider.gameObject.tag.Equals("Body"))
            {
                currentBody = collider.gameObject.GetComponent<Body>();
                if(currentBody.IsAssigned)
                    continue;
                currentBody.MakeAssignStatusTrue();
                isAbleToSearchForBody = false;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isAbleToSearchForBody && !isCollidingWithBody)
            return;
        if (other.CompareTag("Body"))
        {
            isCollidingWithBody = true;
            currentBody.AssignHelper(this);
        }
    }
}