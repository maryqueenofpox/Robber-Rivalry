using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetFloorSign : MonoBehaviour
{
    float activeDuration = 10f;
    float rigidTIme = 0.5f;

    bool doOnce;

    RaycastHit hit;
    RaycastHit hit2;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(-90f, transform.rotation.y, transform.rotation.z);
        gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
        doOnce = false;
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
            if (!doOnce)
            {
                ChangeScale();
                doOnce = true;
            }
        }

        activeDuration -= Time.deltaTime;
        if (activeDuration <= 0.0f)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit2, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit2.distance, Color.white);
        }
    }

    void ChangeScale()
    {
        transform.localScale = new Vector3((hit.distance + hit2.distance) * 2, transform.localScale.y, transform.localScale.z);
    }
}