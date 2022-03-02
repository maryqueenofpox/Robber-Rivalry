using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapMechanic : MonoBehaviour
{
    [SerializeField] float slapFoce = 5000f;
    [SerializeField] float slapCooldown = 3f;
    bool canSlap = true;
    [SerializeField]
    float timeToSetSlapToFalse = 1f;
    float originalSlapToFalse;
    [SerializeField]
    float durationToIncreaseBy = 0.1f;
    bool doTheSlap = false;
    float originalSlapCooldown;

    // Start is called before the first frame update
    void Start()
    {
        originalSlapToFalse = timeToSetSlapToFalse;

        originalSlapToFalse = timeToSetSlapToFalse;
        originalSlapCooldown = slapCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
