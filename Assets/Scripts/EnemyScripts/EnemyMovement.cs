using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxDistanceBetweenPlayer;

        private GameObject player;
        private Vector3 childTowerPosition;
        private Vector3 playerPosition;
        private Vector3 currentPosition;
        private float moveStep;
        private float distanceBetweenPlayer;
        
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            childTowerPosition = GameObject.FindGameObjectWithTag("Tower").transform.position;
        }

        private void FixedUpdate()
        {
            currentPosition = transform.position;
            //playerPosition = player.transform.position;
            distanceBetweenPlayer = Vector3.Distance(currentPosition, playerPosition);
            moveStep = moveSpeed * Time.deltaTime;
            
            currentPosition = Vector3.MoveTowards(currentPosition,
                distanceBetweenPlayer > maxDistanceBetweenPlayer ? childTowerPosition : playerPosition, 
                moveStep);
            transform.position = currentPosition;
        }
    }
}