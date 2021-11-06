using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGrabber : MonoBehaviour
{

    private float loot = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Loot")
        {
            loot++;
            Destroy(other.gameObject);
        }
    }
}
