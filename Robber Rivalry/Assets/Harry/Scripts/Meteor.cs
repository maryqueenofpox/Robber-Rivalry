using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    Transform Geode;
    float timeToStart = 20f;
    bool doShower = true;

    void Start()
    {
     //   StartCoroutine(SpawnEnemies(19, 1));
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

   
     

    void FixedUpdate()
    {
        timeToStart -= Time.deltaTime;
        if (timeToStart <= 0f)
        {
            if (doShower == true)
            {


                StartCoroutine(SpawnEnemies(19, 1));
                doShower = false;
            }
        }
    }
   

}
