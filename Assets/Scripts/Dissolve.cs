using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Transform child;

    void Start()
    {
        child = GameObject.Find("Child").transform;
    }

    void Update()
    {
        if (transform.position.z < (child.position.z - 5.0f))
        {
            gameObject.SetActive(false);
        }
    }
}
