using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float value = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0, 0, value * Time.deltaTime);
    }
}
