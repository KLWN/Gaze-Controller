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
    public int OutlineSize = 5;
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
    public int TimeAfterTriggerStarted = 270;
    private int waitAfterTriggerStarted;
    public static bool triggerAborted;

    private GameObject selectedGameObject;
    private Animator selectedAnimator;
    


    private void Start()
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
                OnTriggerStarted(mySelection);
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
                OnTriggerAborted(mySelection);
                triggerAborted = true;
            }
        }

        
        if (LoadingBarAnimated.isLoaded)
        {
            OnDeselect(mySelection);

            Animator selectionAnim;
            
            if (mySelection.GetComponent<Animator>())
            {
                selectedAnimator = mySelection.GetComponent<Animator>();

                if (selectedAnimator.GetBool("open") == true)
                {
                    selectedAnimator.SetBool("open", false);
                    return;
                }
                
                selectedAnimator.SetBool("open", true);
            }
            
    
            
//            if (mySelection.GetComponent<Animator>())
//            {
//                selectionAnim = mySelection.GetComponent<Animator>();
//                selectionAnim.SetBool("ActivateBool", true);
//            }
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

    
    private void OnSelect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        outline.OutlineWidth = OutlineSize;
        Debug.Log("Selected");
    }


    private void OnDeselect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        outline.OutlineWidth = 0;
        Debug.Log("Deselected");
    }
    
    
    private void OnTriggerStarted(Transform selection)
    {
        Debug.Log("Trigger started");
    }
    
    
    private void OnTriggerAborted(Transform selection)
    {
        Debug.Log("Trigger stopped");
    }
    
    
}