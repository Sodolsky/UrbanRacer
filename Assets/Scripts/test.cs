using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float levitationHeight = 1.0f; // Wysoko�� lewitacji obiektu
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Wy��czamy fizyk� obiektu
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 down = transform.TransformDirection(Vector3.down);

        // Sprawdzanie, czy obiekt dotyka innego obiektu pod nim
        if (Physics.Raycast(transform.position, down, out hit))
        {
            float distanceToGround = hit.distance;

            // Sprawdzenie, czy obiekt znajduje si� wystarczaj�co wysoko nad innym obiektem
            if (distanceToGround > levitationHeight)
            {
                // W��czenie kinematyki i "zamro�enie" obiektu na swoim miejscu
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                // Poprawa pozycji obiektu, aby lewitowa� na w�a�ciwej wysoko�ci
                transform.position = new Vector3(transform.position.x, hit.point.y + levitationHeight, transform.position.z);
            }
            else
            {
                // Wy��czenie kinematyki, aby obiekt podlega� fizyce
                rb.isKinematic = false;
            }
        }
    }
}
