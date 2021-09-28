using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckControllers : MonoBehaviour
{

    public UnityEvent eventOnEnter;
    public UnityEvent eventOnExit;
    int controllersInSphere;
    public void OnTriggerEnter(Collider other)
    {
        var isXrController = other.GetComponent<BreathingXRController>() ? other.GetComponent<BreathingXRController>() : other.GetComponentInParent<BreathingXRController>();
        if (isXrController != null)
        {
            //     Debug.Log("XRController has entered Trigger");
            if (eventOnEnter != null) eventOnEnter.Invoke();
            controllersInSphere++;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var isXrController = other.GetComponent<BreathingXRController>() ? other.GetComponent<BreathingXRController>() : other.GetComponentInParent<BreathingXRController>();
        if (isXrController != null)
        {
            if (eventOnExit != null) eventOnExit.Invoke();

            //   Debug.Log("XRController has exited Trigger");
            controllersInSphere--;
        }
    }
    public bool bothHandsInSphere = false;
    public bool atLeastOneHandInSphere;
    public float handsNeededTogetherTime = 1.5f;
    float timer = 0;

    void OnDisable()
    {
        if (eventOnExit != null) eventOnExit.Invoke();
    }
    public void Update()
    {
        if (controllersInSphere == 2)
        {
            timer += Time.deltaTime;
            if (timer > handsNeededTogetherTime) bothHandsInSphere = true;
        }
        else
        {
            timer = 0;
            bothHandsInSphere = false;
        }
        atLeastOneHandInSphere = controllersInSphere > 0;
    }

}
