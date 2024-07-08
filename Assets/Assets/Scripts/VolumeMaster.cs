using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMaster : MonoBehaviour
{
    public Slider soundSlider;
    private void Start()
    {
        soundSlider.value = AudioListener.volume;
    }
}
