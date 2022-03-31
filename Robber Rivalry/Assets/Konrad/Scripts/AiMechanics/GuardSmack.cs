using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSmack : MonoBehaviour
{
    Vector3 originalPos;
    [SerializeField]
    Transform guardSpawner;

    public Transform guardTeleportTo { get; private set; }

    private void Start()
    {
        originalPos = transform.position;
        guardTeleportTo = guardSpawner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ForceField ff = collision.gameObject.GetComponent<ForceField>();
            LootGrabber lg = collision.gameObject.GetComponent<LootGrabber>();

            if (ff.enabled)
            {
                ff.enabled = false;
                transform.position = guardSpawner.position;
            }
            else
                lg.transform.position = lg.respawnpoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Killzone")
        {
            transform.position = guardSpawner.position;
        }
    }
}
