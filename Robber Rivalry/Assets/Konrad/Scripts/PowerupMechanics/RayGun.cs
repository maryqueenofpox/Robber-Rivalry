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
            controls.gotShot = true;
            controls.isStunned = true;
            Destroy(gameObject);
        }
    }
}
