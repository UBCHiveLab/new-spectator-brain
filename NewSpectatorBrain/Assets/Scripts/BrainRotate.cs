using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainRotate : MonoBehaviour
{
    public float rotationSpeed;
    private bool isRotating;

    public void Rotate()
    {
        if(isRotating)
        {
            return;
        } else
        {
            isRotating = true;
        }
    }

    public void Stop()
    {
        if(!isRotating)
        {
            return;
        } else
        {
            isRotating = false;
        }
    }

    private void Update()
    {
        if(isRotating)
        {
            transform.Rotate(transform.up, rotationSpeed);
        }
    }
}
