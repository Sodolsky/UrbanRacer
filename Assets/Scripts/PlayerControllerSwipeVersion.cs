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
     1 - �rodkowy
     2 - Prawy
     Zmienna currentLane m�wi nam o tym, na jakim pasie znajduje si� aktualnie samoch�d.
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

        //zwieksza pr�dko��
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


        // Sprawdzamy, czy zosta� naci�ni�ty klawisz strza�ki w prawo
        if (SwipeController.swipeRight)
        {
            currentLane++;
            if(currentLane == 3)
            {
                currentLane = 2;
            }
        }

        // Sprawdzamy, czy zosta� naci�ni�ty klawisz strza�ki w lewo
        if (SwipeController.swipeLeft)
        {
            currentLane--;
            if (currentLane == 3)
            {
                currentLane = 2;
            }
        }
        // Obliczamy pozycj� docelow�, do kt�rej b�dziemy si� porusza�
        Vector3 targetPosition = transform.position.z * transform.forward + transform.up * transform.position.y;
        //WA�NE! W tym momencie kodu b�dziemy si� znajdowali zawsze na �rodkowym pasier a currentLane b�dzie pasem na kt�rym b�dziemy chcieli si� znale��!
        if (currentLane == 0)
        {
            // Je�li chcemy si� znale�� na lewym pasie, dodajemy przesuni�cie w lewo
            targetPosition += Vector3.left * distanceBetweenLanes;
        }
        else if (currentLane == 2)
        {
            // Je�li chcemy si� znale�� na prawym pasie, dodajemy przesuni�cie w prawo
            targetPosition += Vector3.right * distanceBetweenLanes;
        }
        // P�ynnie przechodzimy z obecnej pozycji na pozycj� docelow� u�ywaj�c funkcji Lerp
        //Starajcie si� nie rusza� zmiennej laneSwitchSmoothness bo potrafi� si� odpierdoli� niez�e jaja gracz b�dzie si� trz�s� jak pojebany

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
        // Poruszamy si� w prz�d na podstawie kierunku i pr�dko�ci
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