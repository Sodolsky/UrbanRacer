using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float moveSpeed;
    public float laneSwitchSmoothness = 80;
    /*
     Nasza droga posiada 3 pasy ponumerowane kolejno: 
     0 - Lewy
     1 - �rodkowy
     2 - Prawy
     Zmienna currentLane m�wi nam o tym, na jakim pasie znajduje si� aktualnie samoch�d.
    */
    private int currentLane = 1;
    public float distanceBetweenLanes = 4;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = moveSpeed;

        // Sprawdzamy, czy zosta� naci�ni�ty klawisz strza�ki w prawo
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Sprawdzamy, czy nie znajdujemy si� ju� na skrajnym prawym pasie (pas 2)
            if (!(currentLane + 1 > 2))
            {
                // Je�li nie znajdujemy si� na skrajnym pasie, przesuwamy si� o jeden pas w prawo
                currentLane += 1;
            }
        }

        // Sprawdzamy, czy zosta� naci�ni�ty klawisz strza�ki w lewo
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Sprawdzamy, czy nie znajdujemy si� ju� na skrajnym lewym pasie (pas 0)
            if (!(currentLane - 1 < 0))
            {
                // Je�li nie znajdujemy si� na nim, przesuwamy si� o jeden pas w lewo
                currentLane -= 1;
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSmoothness * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        // Poruszamy si� w prz�d na podstawie kierunku i pr�dko�ci
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
