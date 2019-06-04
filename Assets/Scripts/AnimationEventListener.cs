﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable



public class AnimationEventListener : MonoBehaviour
{
    public bool isLoaded = false;


    private IEnumerator FinishedLoading()
    {
        isLoaded = true;
        
        yield return new WaitForSeconds(1);
        isLoaded = false;
    }
}
