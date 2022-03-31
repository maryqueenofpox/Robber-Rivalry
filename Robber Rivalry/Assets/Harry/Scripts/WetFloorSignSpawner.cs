using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetFloorSignSpawner : MonoBehaviour
{
    [SerializeField] public Transform powerUpBox;
    // Start is called before the first frame update
    void Start()
    {
            //Wet floor prefab is spawned at location of spawner
            Transform clone;
            clone = Instantiate(powerUpBox, transform.position, powerUpBox.rotation);
            clone.transform.parent = transform;
            clone.gameObject.tag = "PowerUp";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "LootReplenish")
        {
                //Loot prefab is spawned at location of spawner
                Transform clone;
                clone = Instantiate(powerUpBox, transform.position, powerUpBox.rotation);
                clone.transform.parent = transform;
                clone.gameObject.tag = "PowerUp";
        }
    }
}
