using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    string noun;

    void Start()
    {
        noun = gameObject.tag;
    }

    void Update()
    {
        if (noun == "magnet") 
        {
            transform.Rotate(0, 1, 0, Space.World);
        }
        if (noun == "coin")
        {
            transform.Rotate(0, 1, 0, Space.World);
        }
    }
}
