using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingSelector : MonoBehaviour
{
    // Start is called before the first frame update
    bool startLoad;
    bool hasSelected = false;
    public float selectTime = 1.5f;

    public float breathingTime = 2;
    public void OnTriggerEnter(Collider other)
    {
        var isXrController = other.GetComponent<BreathingXRController>() ? other.GetComponent<BreathingXRController>() : other.GetComponentInParent<BreathingXRController>();
        if (isXrController != null)
        {
            startLoad = true;
            //     Debug.Log("XRController has entered Trigger");
            // if (eventOnEnter != null) eventOnEnter.Invoke();
            // controllersInSphere++;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var isXrController = other.GetComponent<BreathingXRController>() ? other.GetComponent<BreathingXRController>() : other.GetComponentInParent<BreathingXRController>();
        if (isXrController != null)
        {
            startLoad = false;
            hasSelected = false;
            GameManager.instance.SetSelectIndicator(1.5f, this.transform);

            // if (eventOnExit != null) eventOnExit.Invoke();

            // //   Debug.Log("XRController has exited Trigger");
            // controllersInSphere--;
        }
    }

    void OnTriggerStay(Collider other)
    {
        var isXrController = other.GetComponent<BreathingXRController>() ? other.GetComponent<BreathingXRController>() : other.GetComponentInParent<BreathingXRController>();
        if (isXrController != null)
        {
            GameManager.instance.SetSelectIndicator(selectTime, other.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoad && !hasSelected)
        {
            selectTime -= Time.deltaTime;
            if (selectTime <= 0)
            {
                hasSelected = true;
                // Debug.Log("Should Select");
                GameManager.instance.SelectGameMode(breathingTime);
                GameManager.instance.SetSelectIndicator(1.5f, this.transform);
                GameManager.instance.PlayTimeSelectedAudio();

            }
        }
        else
        {
            // hasSelected = false;
            // GameManager.instance.SetSelectIndicator(1.5f, this.transform);

            selectTime = 1.5f;
        }
    }
}
