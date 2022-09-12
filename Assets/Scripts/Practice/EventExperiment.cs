using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

public class EventExperiment : MonoBehaviour
{
    
    public event EventHandler<OnSpacePressEvent> OnSpacePressed;

    public class OnSpacePressEvent : EventArgs
    {

        public int SpaceCount;

    }

    public event TestingDelegate OnFloatEvent;
    public delegate void TestingDelegate(float f);

    public event Action<bool, int> OnActionEvent;

    public UnityEvent OnUnityEvent;

    private int spaceCount;
    
    private void Start()
    {
            
        
        
        
    }

    
    
    private void Update()
    {

        spaceCount++;
        OnSpacePressed?.Invoke(this, new OnSpacePressEvent{SpaceCount = spaceCount });
        OnFloatEvent?.Invoke(10.5f);
        OnActionEvent?.Invoke(true,56);
        OnUnityEvent?.Invoke();
    }
}
