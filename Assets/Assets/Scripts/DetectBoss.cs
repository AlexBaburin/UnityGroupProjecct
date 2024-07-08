using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoss : MonoBehaviour
{
    public Canvas Boss;
    public AudioSource source;
    public AudioClip clip;
    AudioSource musicBG;
    private void MusicBoss()
    {
        musicBG = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<AudioSource>();
        musicBG.mute = true;
        clip = (AudioClip)Resources.Load("Sounds/musicBoss");
        source.clip = clip;
        source.loop = true;
        source.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            MusicBoss();
            Boss.gameObject.SetActive(true);
        }
    }
}
