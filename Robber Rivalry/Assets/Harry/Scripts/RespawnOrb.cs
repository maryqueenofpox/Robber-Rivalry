using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOrb : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;

    private float waitTime = 2;
    private int timeToDestroy = 2;

    private float pullSpeed = 30f;

    bool moveToPlatform = false;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = 2;
        
       // StartCoroutine(CountdownToStart());
    }

   // IEnumerator CountdownToStart()
  //  {
   //     while (waitTime > 1)
     //   {
     //       yield return new WaitForSeconds(1f);
     //       waitTime--;
    //    }

    //    while (waitTime > 0 && waitTime < 1)
    //    {
    //        moveToPlatform = true;
    //        yield return new WaitForSeconds(1f);
    //        waitTime--;
    //    }
    //    StopCoroutine(CountdownToStart());
    //    Destroy(gameObject);
   // }

    // Update is called once per frame
    void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        else if (waitTime > 0 && waitTime < 1)
        {
            transform.position = Vector3.MoveTowards(platform.transform.position, transform.position, pullSpeed * Time.deltaTime);
        }
        else
        {
            waitTime = 2;
            Destroy(gameObject);
        }
    }
}
