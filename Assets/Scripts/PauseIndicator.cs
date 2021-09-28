using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseIndicator : MonoBehaviour
{
    public Image radialLoadingImage;
    // Start is called before the first frame update
    public void SetLoadStatus(float amount)
    {
        radialLoadingImage.fillAmount = amount;
    }


}
