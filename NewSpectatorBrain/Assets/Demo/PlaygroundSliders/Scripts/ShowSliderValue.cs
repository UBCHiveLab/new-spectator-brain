using System.Collections;
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
        if (brain != null)
        {
            Quaternion target = Quaternion.Euler(0, eventData.NewValue*360, 0);
            brain.transform.rotation = target;
        }
        if (textMeshPro != null)
        {
            textMeshPro.text = $": {eventData.NewValue}";
        }
        Debug.Log(eventData.NewValue);
    }
}
