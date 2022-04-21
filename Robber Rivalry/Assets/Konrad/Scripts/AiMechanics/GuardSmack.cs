using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSmack : MonoBehaviour
{
    [SerializeField]
    Transform guardSpawner;

    [SerializeField] AudioSource bonkSource;

    public Transform guardTeleportTo { get; private set; }
    [SerializeField] GameObject guardSpawnAnimation;

    float guardSpawnAnimationTimer = 2f;
    float originalGuardSpawnAnimationTimer;

    private void Start()
    {
        guardTeleportTo = guardSpawner;
        guardSpawnAnimation.SetActive(false);
        originalGuardSpawnAnimationTimer = guardSpawnAnimationTimer;
    }

    private void Update()
    {
        if (guardSpawnAnimation.activeSelf)
        {
            guardSpawnAnimationTimer -= Time.deltaTime;

            if (guardSpawnAnimationTimer <= 0)
            {
                guardSpawnAnimation.SetActive(false);
                guardSpawnAnimationTimer = originalGuardSpawnAnimationTimer;
            }
        }
    }

    public void PlayGuardAnimation()
    {
        guardSpawnAnimation.SetActive(true);
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
                PlayGuardAnimation();
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
                lg.guardPosForOrb = transform.position;
                lg.spawnRespawnOrb();
                pc.RespawnAnimation();


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
            PlayGuardAnimation();
            transform.position = guardSpawner.position;
        }
    }
}
