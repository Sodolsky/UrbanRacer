using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public AudioManager audioMenager;
    // Start is called before the first frame update
    void Start()
    {

        AudioManager audioManagerComponent = audioMenager.GetComponent<AudioManager>();
        slider.onValueChanged.AddListener((value) =>
        {
            audioManagerComponent.ChangeVolume(value);
        });   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
