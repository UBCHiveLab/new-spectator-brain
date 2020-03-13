﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class ShowSliderValue : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textMeshPro = null;
    [SerializeField]
    private GameObject brain;
    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshPro>();
        }
    }
    public void OnSliderUpdated(SliderEventData eventData)
    {
        if (brain != null && eventData.NewValue !=0)
        {
            brain.GetComponent<BrainRotate>().Rotate();
        }
        else
        {
            brain.GetComponent<BrainRotate>().Stop();
        }
        if (textMeshPro != null)
        {
            textMeshPro.text = $": {eventData.NewValue}";
        }
        Debug.Log(eventData.NewValue);
    }
}
