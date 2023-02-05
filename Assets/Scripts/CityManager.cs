using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.waterSupply -= resourceCost * Time.deltaTime;
        GameManager.Instance.nutrientSupply -= resourceCost * Time.deltaTime;
    }

    public void unpauseTime()
    {
        Time.timeScale = 1;
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
