using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    float activeDuration = 10f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "MagnetField";
    }

    // Update is called once per frame
    void Update()
    {
        activeDuration -= Time.deltaTime;
        if (activeDuration <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Loot")
        {
            Debug.Log("LOOT GRABBED");
        }
    }

}
