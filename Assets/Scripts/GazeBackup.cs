using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable


public class GazeBackup : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    private Transform _selection;
    private Camera _camera;


    private void Awake()
    {
        _camera = Camera.main;
    }


    private void Update()
    {
        // When the Raycast hits an Object (with "Selectable" Tag) it calls OnSelect() as well as DeSelect().
        // Because we keep hitting the Object when it is selected 

        // Deselection
        if (_selection != null)
        {
            OnDeselect(_selection);
        }


        // Creating Ray
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        //Selection Determination
        _selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }


        // Selection
        if (_selection != null)
        {
            OnSelect(_selection);
        }
    }


    private void OnSelect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();

        outline.OutlineWidth = 10;
        Debug.Log("Selected");
        LoadingBar.Activated = true;
    }


    private void OnDeselect(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();

        outline.OutlineWidth = 0;
        Debug.Log("Deselected");
        LoadingBar.Activated = false;
    }
}