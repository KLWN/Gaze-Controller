using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable


public class GazeController : MonoBehaviour
{
    [Space(5)] [Header("Gaze Controller Settings:")] 
    [SerializeField]
    private string selectableTag = "Selectable";
    private Camera _camera;
    private bool firstBlood;
    public Transform mySelection;
    public string whatWasHit;
    [Space(10)] 
    public int TimeBeforeSelected = 10;
    private int waitBeforeSelected;
    public static bool selected;
    [Space(10)] 
    public int TimeBeforeDeselected = 10;
    private int waitBeforeDeselected;
    public static bool deselect;
    [Space(10)] 
    public int TimeBeforeTriggerStarts = 30;
    private int waitBeforeTriggerStarts;
    public static bool triggerStarted;
    [Space(10)]
    public int TimeAfterTriggerStarted = 120;
    private int waitAfterTriggerStarted;
    public static bool triggerAborted;


    private void Awake()
    {
        _camera = Camera.main;
    }


    private void Update()
    {

        if (waitBeforeSelected <= TimeBeforeSelected && firstBlood)
        {
            waitBeforeSelected++;
        }

        if (waitBeforeSelected > TimeBeforeSelected)
        {
            if (!selected && mySelection != null)
            {
                OnSelect(mySelection);
                deselect = false;
                triggerStarted = false;
                triggerAborted = false;
                selected = true;
            }    
        }
        
        
        if (waitBeforeDeselected <= TimeBeforeDeselected && firstBlood)
        {
            waitBeforeDeselected++;
        }

        if (waitBeforeDeselected > TimeBeforeDeselected)
        {
            if (!deselect)
            {
                OnDeselect(mySelection);
                selected = false;
                triggerStarted = false;
                triggerAborted = false;
                waitAfterTriggerStarted = 0;
                deselect = true;
            }
        }
        
        
        if (waitBeforeTriggerStarts <= TimeBeforeTriggerStarts && selected)
        {
            waitBeforeTriggerStarts++;
        }

        if (waitBeforeTriggerStarts > TimeBeforeTriggerStarts && selected)
        {
            if (!triggerStarted)
            {
                OnTriggerStarted();
                triggerStarted = true;
            }
        }
        
        
        if (waitAfterTriggerStarted <= TimeAfterTriggerStarted && triggerStarted)
        {
            waitAfterTriggerStarted++;
        }

        if (waitAfterTriggerStarted < TimeAfterTriggerStarted && triggerStarted && whatWasHit == null)
        {
            if (!triggerAborted)
            {
                OnTriggerAborted();
                triggerAborted = true;
                Debug.Log("Trigger aborted!!!!!");
            }
        }


        // Creating a RayCast and check if we hit something
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                mySelection = selection;
                whatWasHit = selection.name;
                waitBeforeDeselected = 0;

                if (!firstBlood)
                {
                    firstBlood = true;
                }
            }
            else
            {
                whatWasHit = null;
                waitBeforeSelected = 0;
                waitBeforeTriggerStarts = 0;
            }
        }
    }


    private static void OnSelect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        outline.OutlineWidth = 10;
    }


    private static void OnDeselect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }
    
    
    private static void OnTriggerStarted()
    {
        // do something 
    }
    
    
    private static void OnTriggerAborted()
    {
        // do something 
    }
    
    
}