using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BreathingXRController : MonoBehaviour
{
    public InputDevice device;
    public InputDeviceCharacteristics deviceChar;
    public bool assigned;
    public GameObject[] hideThese;

    // Start is called before the first frame update
    void Start()
    {
        if (!assigned)
        {
            var devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(deviceChar, devices);

            if (devices.Count > 0)
            {
                //     Debug.Log($"{devices.Count} devices found for {deviceChar}");
                device = devices[0];
                //      Debug.Log($"device {device} assigned");
                assigned = true;
            }
        }
    }
    public void SendHapticPulse()
    {
        HapticCapabilities capabilities;
        if (device.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                //    Debug.Log($"supports impulse");
                uint channel = 0;
                float amplitude = 0.1f;
                float duration = 2.0f;
                bool result = device.SendHapticImpulse(channel, amplitude, duration);
                //   Debug.Log(result); //this is FALSE, no idea why
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool triggerValue;

        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out triggerValue))
        {
            foreach (var item in hideThese)
            {
                item.SetActive(triggerValue);
            }
        }
    }
}
