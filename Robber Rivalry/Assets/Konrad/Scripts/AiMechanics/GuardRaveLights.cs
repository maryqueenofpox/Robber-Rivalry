using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRaveLights : MonoBehaviour
{
    [SerializeField] Material emissionMap1;
    [SerializeField] Material emissionMap2;

    Material currentMaterials;

    // Start is called before the first frame update
    void Start()
    {
        currentMaterials = GetComponent<Renderer>().material;

        currentMaterials = emissionMap1;

        GetComponent<Renderer>().material = currentMaterials;

        StartCoroutine(RaveParty());
    }

    IEnumerator RaveParty()
    {
        while (true)
        {
            currentMaterials = GetComponent<Renderer>().material;

            currentMaterials = emissionMap2;

            GetComponent<Renderer>().material = currentMaterials;

            yield return new WaitForSeconds(0.2f);

            currentMaterials = GetComponent<Renderer>().material;

            currentMaterials = emissionMap1;

            GetComponent<Renderer>().material = currentMaterials;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
