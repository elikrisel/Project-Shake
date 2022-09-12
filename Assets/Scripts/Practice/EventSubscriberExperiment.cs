using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriberExperiment : MonoBehaviour
{
    private EventExperiment eventExperiment;
    
    private void Start()
    {
        eventExperiment = GetComponent<EventExperiment>();
        eventExperiment.OnSpacePressed += EventExperimentOnOnSpacePressed;
        eventExperiment.OnFloatEvent += EventExperimentOnOnFloatEvent;
        eventExperiment.OnActionEvent += EventExperimentOnOnActionEvent;
    }

    private void EventExperimentOnOnActionEvent(bool arg1, int arg2)
    {
        Debug.Log(arg1 + " " + arg2);
        eventExperiment.OnActionEvent -= EventExperimentOnOnActionEvent;
    }


    private void EventExperimentOnOnFloatEvent(float f)
    {
        Debug.Log("Float: " + f);
        eventExperiment.OnFloatEvent -= EventExperimentOnOnFloatEvent;
        
    }


    private void EventExperimentOnOnSpacePressed(object sender, EventExperiment.OnSpacePressEvent e)
    {
        Debug.Log("Space " + e.SpaceCount);
        
        eventExperiment.OnSpacePressed -= EventExperimentOnOnSpacePressed;
    }

    public void TestingUnityEvent()
    {
        
        Debug.Log("Testing unity event");
        
    }
    
}
