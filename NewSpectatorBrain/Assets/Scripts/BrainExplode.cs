using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class BrainExplode : MonoBehaviour
{

    private class BrainStructure
    {
        public Transform modelTransform;
        public Vector3 initialPosition;
        public Vector3 explodingDirection;
        //public Vector3 furthestPosition;
        public BrainStructure(Transform structure, Vector3 centerOfBrainModel)
        {
            modelTransform = structure;
            initialPosition = modelTransform.localPosition;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            foreach (Renderer renderer in structure.GetComponentsInChildren<Renderer>())
            {
                if (bounds.size.Equals(Vector3.zero))
                {
                    bounds = new Bounds(renderer.bounds.center, renderer.bounds.size);
                }
                else
                {
                    bounds.Encapsulate(renderer.bounds.center);
                }
            }
            explodingDirection = modelTransform.InverseTransformPoint(bounds.center) - modelTransform.InverseTransformPoint(centerOfBrainModel);
            //furthestPosition = initialPosition + (explodingDirection * MAX_EXPLODE_DISTANCE_MULTIPLE);
        }

        public void MoveToPosition(float distance)
        {
            modelTransform.localPosition = initialPosition + (explodingDirection * distance);
        }
    }

    [SerializeField]
    private GameObject cortex;

    private Vector3 centerOfBrainModel;
    private readonly List<string> STRUCTURES_THAT_DO_NOT_EXPLODE = new List<string> { "Cortex", "Ventricles", "Arteries", "Sinuses" };
    private List<BrainStructure> explodingStructures;

    private void OnEnable()
    {
        if (cortex == null)
            return;

        //calculate brain center
        Renderer[] cortexRenderers = cortex.GetComponentsInChildren<Renderer>();
        Bounds brainBounds = new Bounds(cortex.GetComponentInChildren<Renderer>().bounds.center, Vector3.zero);
        foreach (Renderer renderer in cortexRenderers)
        {
            brainBounds.Encapsulate(renderer.bounds.center);
        }

        centerOfBrainModel = brainBounds.center;
        explodingStructures = new List<BrainStructure>();
        foreach (GameObject structure in GameObject.FindGameObjectsWithTag("Structure"))
        {
            if (!STRUCTURES_THAT_DO_NOT_EXPLODE.Contains(structure.name))
            {
                explodingStructures.Add(new BrainStructure(structure.transform, centerOfBrainModel));
            }
        }
    }
    void OnDisable()
    {
        explodingStructures.Clear();
    }

    public void Explode(SliderEventData eventData)
    {
        if (explodingStructures.Count == 0)
            return;
        foreach (BrainStructure sub in explodingStructures)
        {
            sub.MoveToPosition(eventData.NewValue*0.01f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
