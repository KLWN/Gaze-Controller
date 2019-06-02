using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class LoadingBarAnimated : MonoBehaviour
{
    public static bool Activated = false;
    public static bool Deactivated = true;
    public GameObject loadingBarUI;
    public Animator animator;
    private readonly int activatedHash = Animator.StringToHash("activated");
    private readonly int deactivatedHash = Animator.StringToHash("deactivated");

    private void Awake()
    {
        animator = loadingBarUI.GetComponent<Animator>();
    }


    private void Update()
    {


        Activated = GazeController.triggerStarted;
        Deactivated = GazeController.triggerAborted;
        
        animator.SetBool(activatedHash, Activated);
        animator.SetBool(deactivatedHash, Deactivated);
    }
}