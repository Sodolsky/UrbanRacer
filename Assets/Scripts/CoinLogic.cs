using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren();
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        float resizeFactor = 1.25f;
        capsuleCollider.radius *= resizeFactor;
        capsuleCollider.height *= resizeFactor;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject Car = GameObject.Find("Car");
        if (Car)
        {
            if (gameObject.transform.position.z < Car.transform.position.z - 20)
            {
                Destroy(gameObject);
            }
        }
           //transform.Rotate(50 * Time.deltaTime,0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            PlayerManager.numberOfCoins += 1;
            Destroy(gameObject);
        }
    }
}
