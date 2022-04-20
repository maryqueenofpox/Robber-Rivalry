using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSmack : MonoBehaviour
{
    [SerializeField]
    Transform guardSpawner;

    [SerializeField] AudioSource bonkSource;

    public Transform guardTeleportTo { get; private set; }

    private void Start()
    {
        guardTeleportTo = guardSpawner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ForceField ff = collision.gameObject.GetComponent<ForceField>();
            LootGrabber lg = collision.gameObject.GetComponent<LootGrabber>();
            PlayerControls pc = collision.gameObject.GetComponent<PlayerControls>();
            GemMechanic gm = collision.gameObject.GetComponent<GemMechanic>();


            if (ff.enabled)
            {
                bonkSource.Play();
                ff.enabled = false;
                transform.position = guardSpawner.position;
            }
            else if (pc.vulnerable)
            {
                lg.doDropLoot = true;
                lg.dropLootAtThisPosition = transform.position;
                pc.vulnerable = false;
                pc.canDoStuff = false;
                bonkSource.Play();
                gm.DropGem();

               
                lg.transform.position = lg.respawnpoint.position;
                lg.spawnRespawnOrb();


            }
            else
            {
                return;
            }
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
