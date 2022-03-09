using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom_WetFloorSpawner : MonoBehaviour
{
    [SerializeField] public Transform prefab;
    [SerializeField] public Transform HoneyGrenadePowerup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "LootReplenish")
        {
            if (Random.Range(0, 100) < 61)
            {
                //Loot prefab is spawned at location of spawner
                Transform clone;
                clone = Instantiate(prefab, transform.position, prefab.rotation);
                clone.transform.parent = transform;
                clone.gameObject.tag = "PowerUp";
            }
            else
            {
                Transform grenade;
                grenade = Instantiate(HoneyGrenadePowerup, transform.position, HoneyGrenadePowerup.rotation);
               // grenade.transform.parent = transform;
                grenade.gameObject.tag = "PowerUpHoney";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
