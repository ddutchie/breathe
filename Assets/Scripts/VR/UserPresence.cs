using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

//User Presence Check - Audio Queues help for inside and outside vr
public class UserPresence : MonoBehaviour
{
    bool userPresent = false;
    InputDevice headDevice;
    bool previousState = false;
    public UnityEvent eventHeadsetOff, eventHeadsetOn;

    void Start()
    {
        headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);

    }

    //Recenter Playspace  - used when headset is placed onto user. 
    public void RecenterVRPlayspace()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        if (devices.Count != 0)
        {
            devices[0].subsystem.TryRecenter();
        }
    }
    void Update()
    {
        if (headDevice.isValid)
        {
            // bool userPresent = false;
            bool presenceFeatureSupported = headDevice.TryGetFeatureValue(CommonUsages.userPresence, out userPresent);
            if (previousState != userPresent)
            {
                if (userPresent) eventHeadsetOn.Invoke();
                else eventHeadsetOff.Invoke();
                previousState = userPresent;
            }
            // Debug.Log("presence feature supported " + presenceFeatureSupported + " userPresent is " + userPresent);
        }
    }
}
