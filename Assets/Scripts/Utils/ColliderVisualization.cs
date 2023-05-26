using UnityEngine;

public class ColliderVisualization : MonoBehaviour
{
    [SerializeField] private Color colliderColor = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = colliderColor;

        // Iterate through all colliders in the scene
        Collider[] colliders = FindObjectsOfType<Collider>();
        foreach (Collider collider in colliders)
        {
            // Draw the collider's bounds
            Gizmos.matrix = collider.transform.localToWorldMatrix;
            if (collider is BoxCollider)
            {
                BoxCollider boxCollider = (BoxCollider)collider;
                Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            }
            else if (collider is SphereCollider)
            {
                SphereCollider sphereCollider = (SphereCollider)collider;
                Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);
            }
            // Add additional cases for other collider types as needed

        }
    }
}
