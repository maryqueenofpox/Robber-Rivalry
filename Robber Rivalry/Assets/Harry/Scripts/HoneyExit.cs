using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyExit : MonoBehaviour
{
        float activeDuration = 5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            activeDuration -= Time.deltaTime;
            if (activeDuration <= 0.0f)
                Destroy(gameObject);
        }
}
