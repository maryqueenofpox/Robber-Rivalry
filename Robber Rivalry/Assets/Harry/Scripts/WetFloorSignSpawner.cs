using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetFloorSignSpawner : MonoBehaviour
{
    public Transform prefab;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 100) <= 40)
        {
            //Wet floor prefab is spawned at location of spawner
            Transform clone;
            clone = Instantiate(prefab, transform.position, prefab.rotation);
            clone.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
