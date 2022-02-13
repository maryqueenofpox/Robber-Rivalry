using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetFloorSign : MonoBehaviour
{
    float activeDuration = 10f;
    float rigidTIme = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
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
        }

        activeDuration -= Time.deltaTime;
        if (activeDuration <= 0.0f)
            Destroy(gameObject);
    }
}