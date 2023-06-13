using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;
    public TextMeshProUGUI Coins;

    private float playerDistance;
    public TextMeshProUGUI DistanceLength;
    public TextMeshProUGUI DistanceLength2;

    public GameObject Car;

    private CarDistance distanceScript;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
        playerDistance = 0f;

        distanceScript = Car.GetComponent<CarDistance>();
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        Coins.text = "Coins: " + numberOfCoins;

        if (isGameStarted)
        {
            playerDistance += distanceScript.GetDistanceDelta();
            DistanceLength.text = "Distance: " + playerDistance.ToString("F0") + " m";
            DistanceLength2.text = "Distance: " + playerDistance.ToString("F0") + " m";
        }

        if (SwipeController.tap || Input.GetKeyDown(KeyCode.Return))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}