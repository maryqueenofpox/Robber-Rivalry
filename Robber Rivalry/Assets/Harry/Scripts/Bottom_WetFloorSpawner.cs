using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom_WetFloorSpawner : MonoBehaviour
{
    [SerializeField] public Transform powerUpBox;
    
    private void OnCollisionExit(Collision other)
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
