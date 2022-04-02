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
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        transform.Rotate(-90f, transform.rotation.y, transform.rotation.z);
        gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
        doOnce = false;

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
        {
            Physics.IgnoreCollision(item.GetComponent<Collider>(), GetComponent<Collider>());
        }
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
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right), Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left), Color.blue);


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
        transform.localScale = new Vector3((hit.distance + hit2.distance), transform.localScale.y, transform.localScale.z);
        AstarPath.active.Scan();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerControls controls;
            controls = other.gameObject.GetComponent<PlayerControls>();
            ForceField ff;
            ff = other.gameObject.GetComponent<ForceField>();

            if (ff.enabled)
            {
                ff.enabled = false;
            }
            else
            {
                if (controls.vulnerable)
                    controls.isStunned = true;
                else
                    Debug.Log("Not vulnerable");
            }
        }
    }
}