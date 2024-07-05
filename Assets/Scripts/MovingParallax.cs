using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingParallax : MonoBehaviour
{
    public Transform mainCam;
    public Transform middleBG;
    public Transform backBG;
    public float length = 47.5f;
    public float range = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCam.position.x + range > middleBG.position.x)
            backBG.position = middleBG.position + Vector3.right * length;
        if (mainCam.position.x + range < middleBG.position.x)
            backBG.position = middleBG.position + Vector3.left * length;
    }
}
