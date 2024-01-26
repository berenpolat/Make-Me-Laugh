using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    public static float playerSpeed = 20.0f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // Oyuncu hareketi
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
        Aim();
    }

    void Aim()
    {
        // Fare pozisyonunu al
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ray ile týklanan yere doðru bir çizgi çiz ve çarpýþma kontrolü yap
        if (Physics.Raycast(ray, out hit))
        {
            // Niþan alma yönünü belirle
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0f;

            // Topun niþan alma yönüne dönmesini saðla
            transform.forward = targetDirection.normalized;
        }
    }
}
