using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHUDController : MonoBehaviour
{
    int defaultMask;
    public GameObject toggleNetworkOffButton;
    public GameObject toggleNetworkOnButton;
    public GameObject cityPlacementCanvas;
    public GameObject artilleryModeCanvas;
    public GameObject gameOverCanvas;
    public LayerMask viewNetworkMask;
    public GameObject currentArtilleryParticle;
    public GameObject mainExitButton;

    private void Start() 
    {
        defaultMask = Camera.main.cullingMask;
    }
    public void ToggleMyceliumNetworkOn()
    {
        Camera.main.cullingMask = viewNetworkMask;
        toggleNetworkOnButton.SetActive(false);
        toggleNetworkOffButton.gameObject.SetActive(true);
    }

    public void ToggleMyceliumNetworkOff()
    {
        Camera.main.cullingMask = defaultMask;
        toggleNetworkOffButton.SetActive(false);
        toggleNetworkOnButton.SetActive(true);
    }

    public void TurnOnCityPlacementMode()
    {
        cityPlacementCanvas.SetActive(true);
        artilleryModeCanvas.SetActive(false); 
        GameManager.Instance.interactionMode = GameManager.InteractionMode.CityPlacement;
        if(currentArtilleryParticle != null)
        {
            currentArtilleryParticle.SetActive(false);
        }
        foreach(CityManager city in GameManager.Instance.cities)
        {
            city.artilleryMode = false;
        }
    }

    // public void TurnOffCityPlacementMode()
    // {
    //     cityPlacementCanvas.SetActive(false);
    //     artilleryModeCanvas.SetActive(true);
    // }  

    public void TurnOnArtilleryMode(GameObject artilleryParticle)
    {
        foreach(CityManager city in GameManager.Instance.cities)
        {
            city.MushCityCanvas.SetActive(false);
        }

        currentArtilleryParticle = artilleryParticle;
        cityPlacementCanvas.SetActive(false);
        artilleryModeCanvas.SetActive(true);
        GameManager.Instance.interactionMode = GameManager.InteractionMode.Artillery;
    }

    // public void TurnOffArtilleryMode()
    // {
    //     cityPlacementCanvas.SetActive(true);
    //     artilleryModeCanvas.SetActive(false); 
    // }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        mainExitButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
