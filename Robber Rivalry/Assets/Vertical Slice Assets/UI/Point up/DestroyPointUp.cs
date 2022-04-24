using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPointUp : MonoBehaviour
{

    [SerializeField] float destroyTime = 1f;
    [SerializeField] Vector3 scoreOffset = new Vector3(0, 2, 0);

    void Start()
    {
        transform.localPosition += scoreOffset;
        Destroy(gameObject, destroyTime);
    }
}
