using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BreathingManager : MonoBehaviour
{
    public static BreathingManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Already a BreathingManager in Scene");
            Destroy(this);
        }
    }
    public BreathingXRController[] controllers;
    [Range(1, 5)]
    public float breathingTime = 2;

    public Material breathingMaterial;
    public Color minColor, maxColor;
    public Color minLineColor, maxLineColor;
    Color blendedColor, blendedLineColor;
    // Start is called before the first frame update
    public Transform breathingSphere;

    public NoiseBall.NoiseBallRenderer noiseBall;
    public float breathingOffset;
    float breathingScale;

    public GameObject mainSphere, selectSpheres;
    public void CalculateBreathingOffset(float distance)
    {

        breathingOffset = Mathf.Abs(breathingScale - distance) * 0.1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        mainSphere.SetActive(false);
        selectSpheres.SetActive(true);
        //     OutBreath();
    }



    public void StartBreathing()
    {
        if (breathingTime == 0) return;
        InBreath();
    }

    void InBreath()
    {
        breathingSphere.DOScale(Vector3.one * 1.1f, breathingTime).SetEase(Ease.InOutQuad).OnComplete(OutBreath);
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].SendHapticPulse();
        }
    }
    void OutBreath()
    {
        breathingSphere.DOScale(Vector3.one * 0.15f, breathingTime).SetEase(Ease.InOutQuad).OnComplete(InBreath);
    }
    public void OutBreathStop()
    {
        DOTween.Kill(breathingSphere);
        // breathingSphere.DOScale(Vector3.one * 0.15f, breathingTime * 0.25f).SetEase(Ease.InOutQuad).OnComplete(VRPlayerInfo.instance.InitNewPlayer);
        breathingSphere.DOScale(Vector3.one * 0.15f, breathingTime * 0.25f).SetEase(Ease.InOutQuad).OnComplete(Start);

    }


    // public bool canManualBreathe;
    // public void ManualBreath(float distance)
    // {
    //     if (!canManualBreathe) return;
    //     breathingSphere.transform.localScale = Vector3.one * distance;
    // }

    void SetColorBasedOnDistance(float distance)
    {
        blendedColor = Color.Lerp(minColor, maxColor, Mathf.Clamp01(distance));
        blendedLineColor = Color.Lerp(minLineColor, maxLineColor, Mathf.Clamp01(breathingOffset));
        breathingMaterial.color = blendedColor;
        noiseBall._surfaceColor = blendedColor;
        noiseBall._lineColor = blendedLineColor;
        noiseBall._noiseAmplitude = breathingOffset;
    }
    float currentBreathingTime = 0;

    void Update()
    {
        if (GameManager.instance.theCurrentGameState == GameManager.stateOfGame.running) currentBreathingTime += Time.deltaTime;
        else
        {
            currentBreathingTime = 0;
        }
        if (currentBreathingTime > 0)
        {
            HelpManager.instance.SetText(currentBreathingTime.ToString("F2") + " s", false);
        }
        breathingScale = breathingSphere.transform.lossyScale.x - 0.15f;
        SetColorBasedOnDistance(breathingScale);
    }

}
