using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Loot")
        {
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "PowerUp")
        {
            Destroy(other.gameObject);
        }
    }
}

