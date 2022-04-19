using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
   void Start()
    {
        Destroy(gameObject, 3.3f);
    }
}
