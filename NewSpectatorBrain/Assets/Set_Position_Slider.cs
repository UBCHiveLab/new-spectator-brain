using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class Set_Position_Slider : MonoBehaviour
{
    public float distance = 5f;
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
    public void OnSliderUpdate_SetPosition(SliderEventData eventData)
    {
        if (brain != null && eventData.NewValue != 0)
        {
            brain.GetComponent<SetPosition>().Set();
        }
        else
        {
            brain.GetComponent<SetPosition>().ResetPosition();
        }
        if (textMeshPro != null)
        {
            textMeshPro.text = $": {eventData.NewValue}";
        }
        Debug.Log(eventData.NewValue);
    }
}
