using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerInfo : MonoBehaviour
{
    public static VRPlayerInfo instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Already a VRPlayerInfo in Scene");
            Destroy(this);
        }
    }
    public playerInfo thisPlayer;
    void Start()
    {
        // InitNewPlayer();
    }
    public void InitNewPlayer()
    {
        thisPlayer = new playerInfo();
        StopAllCoroutines();
        StartCoroutine(PlayerInfoGathering());
    }


    IEnumerator PlayerInfoGathering()
    {
        //    BreathingManager.instance.canManualBreathe = true;
        BreathingManager.instance.StopAllCoroutines();
        HelpManager.instance.SetText("Hello");

        yield return new WaitForSeconds(4);
        HelpManager.instance.SetText("Sit or stand, and put both hands into the sphere so you are positionioned comfortably.");
        while (!GameManager.instance.mainBreathingSphere.bothHandsInSphere)
        {
            yield return null;

        }
        HelpManager.instance.SetText("This is your starting position.");
        yield return new WaitForSeconds(4);
        HelpManager.instance.SetText("Move your arms outwards as you slowly take a large breath.");
        while (GameManager.instance.mainBreathingSphere.bothHandsInSphere)
        {
            yield return null;

        }
        yield return new WaitForSeconds(4);

        // yield return (!CheckControllers.instance.bothHandsInSphere);
        HelpManager.instance.SetText("Breathe out slowly and move your arms back towards the starting position.");
        while (!GameManager.instance.mainBreathingSphere.bothHandsInSphere)
        {
            yield return null;

        }
        // yield return (CheckControllers.instance.bothHandsInSphere);
        HelpManager.instance.SetText("OK!");
        // while (CheckControllers.instance.bothHandsInSphere)
        // {
        //     yield return null;

        // }
        //  BreathingManager.instance.canManualBreathe = false;
        yield return new WaitForSeconds(1);

        BreathingManager.instance.StartBreathing();
        GameManager.instance.SwitchState(GameManager.stateOfGame.running);
        GameManager.instance.PlayStartGameAudio();
        // yield return new WaitForSeconds(2);
        HelpManager.instance.SetText("");







    }

    ///We can make the experience more comfortable by getting some info of the user in VR. 
    public class playerInfo
    {
        public float armLength;
        public float armDistance;
        public float shoulderOffset;
        public float breathingTime;

        public void SetPlayerInfo(float inArmLength, float inArmDistance, float inShoulderOffset, float inBreathingTime)
        {
            armLength = inArmLength;
            armDistance = inArmDistance;
            shoulderOffset = inShoulderOffset;
            breathingTime = inBreathingTime;
        }
    }


}
