using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
public void Replay()
    {
        SceneManager.LoadScene("Level");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
