using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    private bool isSetPosition;
    public float distance=5f;

    // Start is called before the first frame update
    void Start()
    {
        isSetPosition = false;
    }

    public void Set()
    {
        if (isSetPosition)
            return;
        else
            isSetPosition = true;
    }
    public void ResetPosition()
    {
        if (!isSetPosition)
            return;
        else
            isSetPosition = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isSetPosition)
        {
            this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        }
    }
}
