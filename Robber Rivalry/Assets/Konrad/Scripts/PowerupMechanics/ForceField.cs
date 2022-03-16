using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] GameObject forceField;
    private void OnEnable()
    {
        forceField.SetActive(true);
    }

    private void OnDisable()
    {
        forceField.SetActive(false);
    }
}
