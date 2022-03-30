using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
    [SerializeField] Button menuButton;
    [SerializeField] Button remachButton;

    private void Start()
    {
        remachButton.Select();
    }
}
