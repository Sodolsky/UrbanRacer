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
     1 - Œrodkowy
     2 - Prawy
     Zmienna currentLane mówi nam o tym, na jakim pasie znajduje siê aktualnie samochód.
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

        // Sprawdzamy, czy zosta³ naciœniêty klawisz strza³ki w prawo
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Sprawdzamy, czy nie znajdujemy siê ju¿ na skrajnym prawym pasie (pas 2)
            if (!(currentLane + 1 > 2))
            {
                // Jeœli nie znajdujemy siê na skrajnym pasie, przesuwamy siê o jeden pas w prawo
                currentLane += 1;
            }
        }

        // Sprawdzamy, czy zosta³ naciœniêty klawisz strza³ki w lewo
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Sprawdzamy, czy nie znajdujemy siê ju¿ na skrajnym lewym pasie (pas 0)
            if (!(currentLane - 1 < 0))
            {
                // Jeœli nie znajdujemy siê na nim, przesuwamy siê o jeden pas w lewo
                currentLane -= 1;
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSmoothness * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        // Poruszamy siê w przód na podstawie kierunku i prêdkoœci
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
