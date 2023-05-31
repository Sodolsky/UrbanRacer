using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSwipeVersion : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float moveSpeed;
    public float maximumSpeed;
    public float increasingSpeed;
    public float laneSwitchSmoothness = 80;
    /*
     Nasza droga posiada 3 pasy ponumerowane kolejno: 
     0 - Lewy
     1 - Œrodkowy
     2 - Prawy
     Zmienna currentLane mówi nam o tym, na jakim pasie znajduje siê aktualnie samochód.
    */
    private int currentLane = 1;
    public float distanceBetweenLanes = 5.5f;

    public float jumpForce;
    public float Gravity = -20;

    void Start()
    {
       
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        if(!PlayerManager.isGameStarted)
        {
            return;
        }

        //zwieksza prêdkoœæ
        if (moveSpeed < maximumSpeed)
        {
            moveSpeed += increasingSpeed * Time.deltaTime;
        }

        direction.z = moveSpeed;

        direction.y += Gravity * Time.deltaTime;
        if (controller.isGrounded)
        {
            if (SwipeController.swipeUp)
            {
                Jump();
            }
        }


        // Sprawdzamy, czy zosta³ naciœniêty klawisz strza³ki w prawo
        if (SwipeController.swipeRight)
        {
            currentLane++;
            if(currentLane == 3)
            {
                currentLane = 2;
            }
        }

        // Sprawdzamy, czy zosta³ naciœniêty klawisz strza³ki w lewo
        if (SwipeController.swipeLeft)
        {
            currentLane--;
            if (currentLane == 3)
            {
                currentLane = 2;
            }
        }
        // Obliczamy pozycjê docelow¹, do której bêdziemy siê poruszaæ
        Vector3 targetPosition = transform.position.z * transform.forward + transform.up * transform.position.y;
        //WA¯NE! W tym momencie kodu bêdziemy siê znajdowali zawsze na œrodkowym pasier a currentLane bêdzie pasem na którym bêdziemy chcieli siê znaleŸæ!
        if (currentLane == 0)
        {
            // Jeœli chcemy siê znale¿æ na lewym pasie, dodajemy przesuniêcie w lewo
            targetPosition += Vector3.left * distanceBetweenLanes;
        }
        else if (currentLane == 2)
        {
            // Jeœli chcemy siê znale¿æ na prawym pasie, dodajemy przesuniêcie w prawo
            targetPosition += Vector3.right * distanceBetweenLanes;
        }
        // P³ynnie przechodzimy z obecnej pozycji na pozycjê docelow¹ u¿ywaj¹c funkcji Lerp
        //Starajcie siê nie ruszaæ zmiennej laneSwitchSmoothness bo potrafi¹ siê odpierdoliæ niez³e jaja gracz bêdzie siê trz¹s³ jak pojebany

        if (transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }

    }
    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }
        // Poruszamy siê w przód na podstawie kierunku i prêdkoœci
        controller.Move(direction * Time.deltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}