using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScaleScript : MonoBehaviour
{

    public GameObject scaleObject;
    private float scaleFactor = 1.5f;
    private float scaleDownFactor = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void scaleUp()
    {
        scaleObject.transform.localScale *= scaleFactor;
    }

    public void scaleDown()
    {
        scaleObject.transform.localScale *= scaleDownFactor;
    }
}
