using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    public string targetObjectName = "Car"; 


    // Start is called before the first frame update
    void Start()
    {
            }

    // Update is called once per frame
    void Update()
    {
        GameObject Car = GameObject.Find(targetObjectName);
        if (Car)
        {
            if (gameObject.transform.position.z < Car.transform.position.z - 20)
            {
                Destroy(gameObject);
            }
        }
    }
}
