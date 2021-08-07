using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineFloatAnimation : MonoBehaviour
{
    float originalY;

    public float speed = 3;
    public Vector3 offset = new Vector3(0,0.05f,0);
    public float floatStrength = 0.05f; // You can change this in the Unity Editor to 
                                    // change the range of y positions that are possible.

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = offset + new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time * speed) * floatStrength),
            transform.position.z);
    }
}
