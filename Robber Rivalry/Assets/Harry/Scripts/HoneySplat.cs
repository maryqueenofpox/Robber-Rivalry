using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneySplat : MonoBehaviour
{
    float activeDuration = 10f;
    [SerializeField] Transform HoneyExit;
    

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
            clone = Instantiate(HoneyExit, transform.position, HoneyExit.rotation);
            clone.gameObject.tag = "HoneyExit";
        }
        if (activeDuration <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
