using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyGrenade : MonoBehaviour
{

    float activeDuration = 3f;
    [SerializeField] float throwForce = 5f;
    [SerializeField] Transform HoneySplat;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        activeDuration -= Time.deltaTime;
        if (activeDuration <= 0.2f)
        {
            Transform clone;
            clone = Instantiate(HoneySplat, transform.position, HoneySplat.rotation);
            clone.gameObject.tag = "HoneySplat";
        }
        if (activeDuration <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
