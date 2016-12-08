using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseScript : MonoBehaviour, IInputHandler
{
    // Reset
    public void OnInputDown(InputEventData eventData)
    {
        // (5) Spatial Mapping: If Tap to Place script is enabled then return early.
        StackedBottlesScript parent = transform.parent.GetComponent<StackedBottlesScript>();
        if (parent.IsTapToPlaceEnabled()) return;

        // (2) Gesture Input: Tap to reset bottles
        transform.parent.GetComponent<StackedBottlesScript>().ResetBottles(); //gameObject.SendMessageUpwards("ResetBottles", SendMessageOptions.DontRequireReceiver);
    }

    public void OnInputUp(InputEventData eventData)
    {
    }
}
