using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    private void Music()
    {
        clip = (AudioClip)Resources.Load("Sounds/whitePalace");
        source.clip = clip;
        source.loop = true;
        source.Play();
    }
    void Start()
    {
        Music();
    }
}
