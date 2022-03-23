using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    Transform Geode;

 
    void Start()
    {
        StartCoroutine(SpawnEnemies(15, 1));
    }

    IEnumerator SpawnEnemies(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            if (Random.Range(0, 10) >= 9)
            {
                Transform clone;
                clone = Instantiate(Geode, transform.position, Geode.rotation);
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
        }
    }


    void Update()
    {
        
    }
   

}
