using UnityEngine;

public class CarDistance : MonoBehaviour
{
    private Vector3 lastPosition;
    private float distanceTraveled;

    void Start()
    {
        lastPosition = transform.position;
        distanceTraveled = 0f;
    }

    public float GetDistanceDelta()
    {
        Vector3 currentPosition = transform.position;
        float distanceDelta = Vector3.Distance(currentPosition, lastPosition);
        lastPosition = currentPosition;
        return distanceDelta;
    }
}
