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

    int ratio = 1;
    float additionalRatio = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCam.position.x + range > middleBG.position.x * ratio  * additionalRatio)
        {
            if (mainCam.position.x + range > middleBG.position.x * (ratio + 1)  * additionalRatio)
                ratio++;
            Debug.Log($"ratio={ratio}, mainCam={mainCam.position.x}, range*(ratio + 1)={range * (ratio + 1)}, middleBG={middleBG.position.x}");
            backBG.position = middleBG.position + Vector3.right * length * ratio;
        }
        if (mainCam.position.x + range * ratio < middleBG.position.x)
        {
            Debug.Log("DOWNER" + "ratio =  " + ratio + "     " + mainCam.position.x);
            if (mainCam.position.x + range * (ratio + 1) < middleBG.position.x)
                ratio--;
            backBG.position = middleBG.position + Vector3.left * length;
        }
    }
}
