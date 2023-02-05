using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHUDController : MonoBehaviour
{
    int defaultMask;
    public GameObject toggleOffButton;
    public GameObject toggleOnButton;
    public LayerMask viewNetworkMask;

    private void Start() 
    {
        defaultMask = Camera.main.cullingMask;
    }
    public void ToggleMyceliumNetworkOn()
    {
        Camera.main.cullingMask = viewNetworkMask;
        toggleOnButton.SetActive(false);
        toggleOffButton.gameObject.SetActive(true);
    }

    public void ToggleMyceliumNetworkOff()
    {
        Camera.main.cullingMask = defaultMask;
        toggleOffButton.SetActive(false);
        toggleOnButton.SetActive(true);
    }

}
