using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

#pragma warning disable


public class LoadingBarAnimated : MonoBehaviour
{
    public bool Activated = false;
    public bool Deactivated = true;
    public bool Deselected;
    public GameObject loadingBarUI;
    public Animator animator;
    private readonly int activatedHash = Animator.StringToHash("activated");
    private readonly int deactivatedHash = Animator.StringToHash("deactivated");
    private AnimationEventListener listener;
    public static bool isLoaded;

    private void Start()
    {
        animator = loadingBarUI.GetComponent<Animator>();
        listener = loadingBarUI.GetComponent<AnimationEventListener>();
    }


    private void Update()
    {
        Activated = GazeController.triggerStarted;
        Deactivated = GazeController.triggerAborted;
        Deselected = GazeController.deselect;

        animator.SetBool(activatedHash, Activated);
        animator.SetBool(deactivatedHash, Deactivated);

        isLoaded = listener.isLoaded;

        if (Deselected)
        {
            animator.SetBool(deactivatedHash, true);
        }

        if (isLoaded)
        {
            DoWhenLoadingDone();
        }
        
    }

    private void DoWhenLoadingDone()
    {
        Debug.Log("DoWhenDone Loaded");
        
    }
}