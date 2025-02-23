using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Transform child;
    ChildController controller;

    void Start()
    {
        child = GameObject.Find("Child").transform;
        controller = GameObject.Find("Child").GetComponent<ChildController>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, child.position);
        if (controller.magnetReceived)
        {            
            if (distance <= 3f)
            {
                transform.position = Vector3.MoveTowards(transform.position, child.position, 10f * Time.deltaTime);
            }
        }
        if (transform.position.z < (child.position.z - 5.0f))
        {
            gameObject.SetActive(false);
        }
    }
}
