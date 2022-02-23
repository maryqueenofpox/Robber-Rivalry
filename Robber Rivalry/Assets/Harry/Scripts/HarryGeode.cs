using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarryGeode : MonoBehaviour
{
    float rigidTIme = 5f;
    bool doOnce;
    void Start()
    {
        gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
        gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidTIme > 0)
        {
            rigidTIme -= Time.deltaTime;
        }
        else
        {
            gameObject.GetComponentInChildren<Rigidbody>().isKinematic = true;
            gameObject.GetComponentInChildren<Rigidbody>().useGravity = false;
            if (!doOnce)
            {
                doOnce = true;
            }
        }
    }
}
