using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] float pullSpeed = 50f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Loot"))
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, pullSpeed * Time.deltaTime);
        }
    }
}
