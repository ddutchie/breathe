using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpManager : MonoBehaviour
{
    public HelpStep[] helpSteps;
    int currentStep;
    public static HelpManager instance;
    public TextMeshProUGUI infoText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Already a HelpManager in Scene");
            Destroy(this);
        }
    }


    // // Start is called before the first frame update
    void Start()
    {
        // SetHealthStep();
    }

    IEnumerator PlayerInfoGathering()
    {
        yield return new WaitForSeconds(4);
        if (GameManager.instance.mainBreathingSphere.bothHandsInSphere)
        {
            SetNextStep();
        }
    }


    [ContextMenu("Next Step")]
    public void SetNextStep()
    {
        if (currentStep < helpSteps.Length)
            currentStep++;
        SetHealthStep(currentStep);
    }
    // // Update is called once per frame
    // void Update()
    // {

    // }

    public void SetHealthStep(int i = 0)
    {
        infoText.text = helpSteps[i].helpInfo;
        currentStep = i;
    }

    public void SetText(string x, bool playAudio = true)
    {
        infoText.text = x;
        if (playAudio) GameManager.instance.PlayAlertAudio();

    }
}
[System.Serializable]
public class HelpStep
{
    public string helpInfo;
}
