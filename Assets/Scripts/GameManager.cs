using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Already a GameManager in Scene");
            Destroy(this);
        }
    }

    public Material skyboxMaterial;
    public Color minColor, maxColor;

    void SetColorBasedOnDistance(float distance)
    {
        blendedColor = Color.Lerp(minColor, maxColor, Mathf.Clamp01(distance));
        skyboxMaterial.color = blendedColor;
    }


    public enum stateOfGame
    {
        paused, running
    }
    public stateOfGame theCurrentGameState = stateOfGame.paused;

    float timer = 3;

    public void SwitchState(stateOfGame state)
    {
        // Debug.Log("State Switched : " + state);
        theCurrentGameState = state;
    }
    public PauseIndicator pauseIndicator;
    public PauseIndicator selectIndicator;


    public CheckControllers mainBreathingSphere;

    public void SetSelectIndicator(float time, Transform selectingHand, float timeOriginal = 1.5f)
    {
        selectIndicator.transform.position = selectingHand.transform.position;
        selectIndicator.SetLoadStatus((timeOriginal - time) / timeOriginal);
    }

    public void SelectGameMode(float breathingTime)
    {
        BreathingManager.instance.breathingTime = breathingTime;
        BreathingManager.instance.mainSphere.SetActive(true);
        BreathingManager.instance.selectSpheres.SetActive(false);
        VRPlayerInfo.instance.InitNewPlayer();
    }
    float originalTime = 1.5f;
    bool overrideHandCheck = false;
    void Update()
    {

        distance = Vector3.Distance(BreathingManager.instance.controllers[0].transform.position, BreathingManager.instance.controllers[1].transform.position);
        SetColorBasedOnDistance(distance - distanceOffset);
        BreathingManager.instance.CalculateBreathingOffset(distance - distanceOffset);

        if (distance <= 0.2 && theCurrentGameState == stateOfGame.running && (!mainBreathingSphere.atLeastOneHandInSphere || overrideHandCheck))
        {
            overrideHandCheck = true;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SwitchState(stateOfGame.paused);
                BreathingManager.instance.OutBreathStop();
                PlayEndGameAudio();

            }
        }
        else
        {
            timer = originalTime;
            overrideHandCheck = false;
        }
        pauseIndicator.transform.position = (BreathingManager.instance.controllers[0].transform.position + BreathingManager.instance.controllers[1].transform.position) * 0.5f;
        pauseIndicator.SetLoadStatus((originalTime - timer) / originalTime);

        // BreathingManager.instance.ManualBreath(distance);
    }




    ///DebugDev

    public float distance;
    public float distanceOffset = 0.06f;

    public Color blendedColor;


    public AudioClip startGameClip, endGameClip, timeSelectedClip, alertClip;
    public AudioSource audioSource;

    public void PlayStartGameAudio()
    {
        audioSource.clip = startGameClip;
        audioSource.Play();
    }
    public void PlayEndGameAudio()
    {
        audioSource.clip = endGameClip;
        audioSource.Play();
    }

    public void PlayTimeSelectedAudio()
    {
        audioSource.clip = timeSelectedClip;
        audioSource.Play();
    }
    public void PlayAlertAudio()
    {
        audioSource.clip = alertClip;
        audioSource.Play();
    }

}
