using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testasdasd : MonoBehaviour
{
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Move Speed = " + playerData.moveSpeed.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
