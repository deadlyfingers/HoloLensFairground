using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StackedBottlesScript : MonoBehaviour
{
    public void ResetBottles()
    {
        Debug.Log("ResetBottles");
        var children = GetComponentsInChildren<BottleScript>();
        foreach (var child in children)
        {
            child.ResetBottle();
        }
    }

    // (1) Gaze Input: Subscribe to the Gaze Manager's focus oject changed event
    void OnEnable()
    {
        GazeManager.Instance.FocusedObjectChanged += OnFocusChanged;
    }

    void OnDisable()
    {
        if (GazeManager.Instance == null)  return;
        GazeManager.Instance.FocusedObjectChanged -= OnFocusChanged;
    }

    // (1) Gaze Input: Highlight interactions by applying a hover effect on the bottles
    void OnFocusChanged(GameObject previousObject, GameObject newObject)
    {
        // Bottle rollover effect
        if (newObject != null && newObject.name.Contains("bottle"))
        {
            newObject.GetComponent<Renderer>().material.color = Color.red;
        }

        // Bottle rollout effect
        if (previousObject != null && previousObject.name.Contains("bottle"))
        {
            previousObject.GetComponent<Renderer>().material.color = previousObject.GetComponent<BottleScript>().OriginalColor;
        }
    }

    // (5) Spatial mapping with customised Tap to Place script
    private TapToPlaceOnce _tapToPlace;

    void Start()
    {
        _tapToPlace = gameObject.GetComponent<TapToPlaceOnce>();
    }

    public void EnableTapToPlace()
    {
        _tapToPlace.enabled = true;
        Debug.Log("Enabled Tap to Place.");
    }

    public bool IsTapToPlaceEnabled()
    {
        return _tapToPlace.enabled;
    }

}
