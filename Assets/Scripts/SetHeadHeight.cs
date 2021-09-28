using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeadHeight : MonoBehaviour
{
    public Transform headTransform;
    public float headOffset = 0.1f;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, headTransform.position.y - headOffset, transform.position.z);
    }
}
