#if UNITY_WSA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using HolobrainConstants;

public class IsolateMode : MonoBehaviour
{
    private GameObject brain;
    private GameObject cortex;
    private GameObject ventricles;
    private List<Transform> structuresList, structureListCopy;
    private List<Transform> isolatedStructures;
    private static bool isInIsolateMode;

    private Dictionary<string, Action> structure_Keywords;

    public KeywordRecognizer structure_recognizer;

    // Start is called before the first frame update
    void Start()
    {
        brain = GameObject.FindGameObjectWithTag("Brain");
        ventricles = GameObject.Find(Names.VENTRICLES_GAMEOBJECT_NAME);
        cortex = GameObject.Find(Names.CORTEX_GAMEOBJECT_NAME);
        structuresList = new List<Transform>();
        //add all children structure in brain to the list
        foreach (Transform structure in brain.transform)
        {
            if(structure.tag== "Structure" && structure != cortex.transform && structure != ventricles.transform)
            structuresList.Add(structure);

        }
        
        structureListCopy = new List<Transform>(structuresList);
        isolatedStructures = new List<Transform>();
        isInIsolateMode = false;
        structure_Keywords = new Dictionary<string, Action>();
        foreach (string item in Names.GetStructureNames())
        {
            structure_Keywords.Add("Add " + item, () => { HandleAddBrainPart(item); });
            structure_Keywords.Add("Accio " + item, () => { HandleAddBrainPart(item); });
            structure_Keywords.Add("Remove " + item, () => { HandleRemoveBrainPart(item); });
            structure_Keywords.Add("Obliviate " + item, () => { HandleRemoveBrainPart(item); });
        }
        structure_recognizer = new KeywordRecognizer(structure_Keywords.Keys.ToArray());
        
        structure_recognizer.OnPhraseRecognized += onPhraseRecognized;
        structure_recognizer.Start();
    }

    public void Isolate()
    {
        if (!isInIsolateMode)
        {
            foreach (Transform structure in structureListCopy)
            {
                structure.gameObject.SetActive(false);
            }
            isInIsolateMode = true;
        }
        else
        {
            return;
        }
    }
    public void QuitIsolate()
    {
        if (isInIsolateMode)
        {
            foreach (Transform structure in structureListCopy)
            {
                structure.gameObject.SetActive(true);
            }
            structureListCopy.Clear();
            structureListCopy = new List<Transform>(structuresList);
            isolatedStructures.Clear();
            isInIsolateMode = false;
        }
        else
        {
            return;
        }
    }
    /// <summary>
    /// enable certain brain parts
    /// </summary>
    /// <param name="structureName"></param>
    public void HandleAddBrainPart(string structureName)
    {
        
        if (!isInIsolateMode)
            return;
        else
        {

            foreach (Transform structure in structureListCopy)
            {
                Transform isolatedStructure;
                if (structure.name == structureName)
                {
                    isolatedStructure = structure;
                    isolatedStructure.gameObject.SetActive(true);
                    isolatedStructures.Add(isolatedStructure);
                    structureListCopy.Remove(isolatedStructure);

                    break;
                }
            }

        }
    }
    /// <summary>
    /// disable certain brain parts 
    /// </summary>
    /// <param name="structureName"></param>
    public void HandleRemoveBrainPart(string structureName)
    {
        if (!isInIsolateMode)
            return;
        else
        {

            foreach (Transform structure in isolatedStructures)
            {
                Transform isolatedStructure;
                if (structure.name == structureName)
                {
                    isolatedStructure = structure;
                    isolatedStructure.gameObject.SetActive(false);
                    isolatedStructures.Remove(isolatedStructure);
                    structureListCopy.Add(isolatedStructure);

                    break;
                }
            }

        }
    }
    public void onPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.ToString());
        Action keywordAction;
        if (structure_Keywords.TryGetValue(args.text, out keywordAction))
        {
            Debug.Log("voice keyword: " + args.text);

            if (brain != null)
            {
                keywordAction.Invoke();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif