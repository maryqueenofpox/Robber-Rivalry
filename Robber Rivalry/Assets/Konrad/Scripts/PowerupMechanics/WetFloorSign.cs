using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetFloorSign : MonoBehaviour
{
    float activeDuration = 10f;
    float rigidTIme = 0.1f;

    bool doOnce;

    RaycastHit hit;
    RaycastHit hit2;

    [SerializeField] float stretchSpeed = 5f;
    [SerializeField] float maxStretchDistance = 2f;
    bool isScaling = false;

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
                DoRayCast();
                StartCoroutine(scaleOverTime());
                doOnce = true;
            }
        }

        activeDuration -= Time.deltaTime;
        if (activeDuration <= 0.0f)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.right);
        Debug.DrawRay(transform.position, Vector3.right * 2);
    }

    void DoRayCast()
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

    IEnumerator scaleOverTime()
    {
        //Make sure there is only one instance of this function running
        if (isScaling)
        {
            yield break; ///exit if this is still running
        }
        isScaling = true;

        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = transform.localScale;

        while (counter < 5)
        {
            counter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScaleSize, new Vector3((hit.distance), transform.localScale.y, transform.localScale.z), counter / 5);
            yield return null;
        }

        isScaling = false;
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