using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float levitationHeight = 1.0f; // Wysokoœæ lewitacji obiektu
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Wy³¹czamy fizykê obiektu
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 down = transform.TransformDirection(Vector3.down);

        // Sprawdzanie, czy obiekt dotyka innego obiektu pod nim
        if (Physics.Raycast(transform.position, down, out hit))
        {
            float distanceToGround = hit.distance;

            // Sprawdzenie, czy obiekt znajduje siê wystarczaj¹co wysoko nad innym obiektem
            if (distanceToGround > levitationHeight)
            {
                // W³¹czenie kinematyki i "zamro¿enie" obiektu na swoim miejscu
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                // Poprawa pozycji obiektu, aby lewitowa³ na w³aœciwej wysokoœci
                transform.position = new Vector3(transform.position.x, hit.point.y + levitationHeight, transform.position.z);
            }
            else
            {
                // Wy³¹czenie kinematyki, aby obiekt podlega³ fizyce
                rb.isKinematic = false;
            }
        }
    }
}
