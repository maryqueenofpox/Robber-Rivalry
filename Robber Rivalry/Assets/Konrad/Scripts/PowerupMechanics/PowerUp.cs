using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 60f;

    private void Update()
    {
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
           Destroy(gameObject);
        }
    }
}