using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFix : MonoBehaviour
{
    public float hoverDistance = 0.1f;

    private void OnCollisionEnter(Collision collision)
    {
        // Pobierz normaln� kolizji
        Vector3 collisionNormal = collision.contacts[0].normal;

        // Popraw pozycj� samochodu
        transform.position += collisionNormal * hoverDistance;
    }
}

