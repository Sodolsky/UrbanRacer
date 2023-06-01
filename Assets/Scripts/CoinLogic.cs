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
       // transform.Rotate(50 * Time.deltaTime,0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            PlayerManager.numberOfCoins += 1;
            Destroy(gameObject);
        }
    }
}
