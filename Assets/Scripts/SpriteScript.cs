using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{
private Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = initialRotation;
    }
}
