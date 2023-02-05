using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CityManager : MonoBehaviour
{
    public TextMeshProUGUI townName;
    public bool capitalCity = false;
    public float resourceCost = 0.1f;
    public float cantBuildNearMeRadius = 10f;
    public float networkConnectionRadius = 15f;
    public GameObject MushCityCanvas;
    public List<CityManager> connectedCities;
    public List<GameObject> connectedMyceliumPaths;
    public bool artilleryMode;
    public GameObject artilleryModeParticle;
    public GameObject artilleryPrefabToFire;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.waterSupply -= resourceCost * Time.deltaTime;
        GameManager.Instance.nutrientSupply -= resourceCost * Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && GameManager.Instance.interactionMode == GameManager.InteractionMode.Artillery && artilleryMode)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) 
            {
                ProjectileController projectile = GameObject.Instantiate(artilleryPrefabToFire, artilleryModeParticle.transform.position, artilleryModeParticle.transform.rotation).GetComponent<ProjectileController>();
                projectile.lookAtPoint = hit.point;
            }
        }

    }

    public void unpauseTime()
    {
        // Time.timeScale = 1;
    }

    public void InitiateArtilleryMode()
    {
        artilleryMode = true;
        artilleryModeParticle.SetActive(true);
        GameManager.Instance.mainHUDController.TurnOnArtilleryMode(artilleryModeParticle);
    }

    public void EndArtilleryMode()
    {
        artilleryMode = false;
        artilleryModeParticle.SetActive(false);
    }
    
    public void DeleteCity()
    {
        GameManager.Instance.cities.Remove(this);
        foreach(GameObject myceliumPath in connectedMyceliumPaths)
        {
            Destroy(myceliumPath);
            foreach(CityManager connectedCity in connectedCities)
            {
                connectedCity.connectedMyceliumPaths.Remove(myceliumPath);
                connectedCity.connectedCities.Remove(this);
            }
        }
        Destroy(this.gameObject);
    }
}
