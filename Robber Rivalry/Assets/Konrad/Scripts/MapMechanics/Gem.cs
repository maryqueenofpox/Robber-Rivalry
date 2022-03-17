using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] GameObject parentPlatform;
    [SerializeField] Transform originalPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null && transform.position.y < 0f)
        {
            transform.parent = parentPlatform.transform;
            transform.position = originalPos.position;
        }
    }
}