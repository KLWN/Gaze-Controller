using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
 



public class LoadingBar : MonoBehaviour
{
    public static bool Activated = false;
    public static bool Finished;
    private float _currentAmount;
    private float _speedModifier;
    private float _finishTimer;

    [SerializeField] private float confirmDuration = 3f;
    [SerializeField] private float waitAfterConfirm = 0.5f;
    [SerializeField] private Transform currentLoadingBar;



    private void Start()
    {
        currentLoadingBar = currentLoadingBar.GetComponent<Transform>();
        _speedModifier = 1f / confirmDuration;
    }


    private void Update()
    {
        currentLoadingBar.GetComponent<Image>().fillAmount = _currentAmount;

        if (!Activated)
        {
            currentLoadingBar.GetComponent<Image>().color = Color.white;
            _currentAmount = 0f;
            _finishTimer = 0f;
            Finished = false;
            return;
        }


        if (Activated && !Finished)
        {
            if (_finishTimer <= waitAfterConfirm && _currentAmount < 1f)
            {
                Debug.Log("LOADING");
                currentLoadingBar.GetComponent<Image>().enabled = true;
                _currentAmount += Time.deltaTime * _speedModifier;
                return;
            }
            
            if (_finishTimer <= waitAfterConfirm && _currentAmount >= 1f)
            { 
                currentLoadingBar.GetComponent<Image>().color = Color.green;
                _finishTimer += Time.deltaTime;
                return;
            }
            
            if (_finishTimer >= waitAfterConfirm)
            {
                // do something cool...
                
                Debug.Log("DONE");
                currentLoadingBar.GetComponent<Image>().enabled = false;
                Finished = true;
            }


        }
        
    }
    
}
    

