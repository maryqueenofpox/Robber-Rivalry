using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    Transform Geode;
    [SerializeField] float lowRange = 50.0f;
    [SerializeField] float highRange = 71.0f;
    float pickedRange;
    bool doShower = true;

    private void Start()
    {
        pickedRange = Random.Range(lowRange, highRange);
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
        if (doShower)
        {
            pickedRange -= Time.deltaTime;
            if (pickedRange <= 0f)
            {
                StartCoroutine(SpawnEnemies(19, 1));
                doShower = false;
            }
        }
    }
}
