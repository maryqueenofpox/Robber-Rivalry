using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float timeUntilSelfDestruct = 2f;

    private void Update()
    {
        timeUntilSelfDestruct -= Time.deltaTime;

        if (timeUntilSelfDestruct <= 0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControls controls = other.GetComponent<PlayerControls>();
            ForceField ff = other.GetComponent<ForceField>();
            if (ff.enabled)
            {
                ff.enabled = false;
            }
            else if (controls.vulnerable)
            {
                controls.gotShot = true;
                controls.isStunned = true;
                controls.vulnerable = false;
            }
            else
                return;
            
            Destroy(gameObject);
        }

        if (other.CompareTag("Guard"))
        {
            GuardSmack guardTP = other.GetComponent<GuardSmack>();
            other.transform.position = guardTP.guardTeleportTo.position;
        }
    }
}
