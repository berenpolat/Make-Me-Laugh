using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    private bool isDash = false;
    public float playerSpeed = 5f;
    public float dashMultiplier = 20f;
    public float dashDuration = 0.5f;
    private float dashTimer;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // Player movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Dash input
        if (Input.GetKeyDown(KeyCode.RightShift) && dashTimer <= 0f)
        {
            dashTimer = dashDuration;
        }

        // Apply dash or normal movement
        if (dashTimer > 0f)
        {
            // Dash movement
            float dashSpeed = playerSpeed * dashMultiplier;
            controller.Move(move * Time.deltaTime * dashSpeed);
            dashTimer -= Time.deltaTime;
        }
        else
        {
            // Normal movement
            controller.Move(move * Time.deltaTime * playerSpeed);
        }

        // Aim method
        Aim();
    }


    void Aim()
    {
        // Fare pozisyonunu al
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ray ile t�klanan yere do�ru bir �izgi �iz ve �arp��ma kontrol� yap
        if (Physics.Raycast(ray, out hit))
        {
            // Ni�an alma y�n�n� belirle
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0f;

            // Topun ni�an alma y�n�ne d�nmesini sa�la
            transform.forward = targetDirection.normalized;
        }
    }
    
}
