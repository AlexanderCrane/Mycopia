using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CityCreator : MonoBehaviour
{
    public GameObject mushroomCityPrefab;
    public GameObject networkLinePointPrefab;
    public bool placingCity = true; // TODO: This should change to be false, and only set to true when user initiates city placement

    string[] namePrefixes = new string[] { "Spore", "Fungus", "Myco", "Toadstool", "Mold", "Truffle", "Rot", "Muck" };
    string[] nameSuffixes = new string[] { "burgh", "sbury", "by", " Town", " City", " Village", "ton", "ville" };
    List<string> usedCityNames;

    // Start is called before the first frame update
    void Start()
    {
        usedCityNames = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.interactionMode == GameManager.InteractionMode.CityPlacement)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if(hit.collider.isTrigger && hit.transform.tag == "mushcity")
                {
                    hit.transform.GetComponent<CityManager>().MushCityCanvas.SetActive(true);
                    // Time.timeScale = 0;
                    return;
                }
                if(hit.point.z > 25f || hit.point.z < -30f || hit.point.x > 21f || hit.point.x < -16)
                {
                    return;
                }
                bool overlapsExistingCity = false;
                
                foreach(CityManager city in GameManager.Instance.cities)
                {
                    float distance = Vector3.Distance(hit.point, city.transform.position);

                    if(distance < city.cantBuildNearMeRadius)
                    {
                        overlapsExistingCity = true;
                        // Debug.Log("Distance from " + city.townName.text + " is " + distance + " which is less than " + city.cantBuildNearMeRadius);
                        break;
                    }
                }

                if(overlapsExistingCity)
                {
                    return;
                }

                string prefix = namePrefixes[Random.Range(0, namePrefixes.Length)];
                string suffix = nameSuffixes[Random.Range(0, nameSuffixes.Length)];
                string cityName = prefix + suffix;

                while(true)
                {
                    if(usedCityNames.Contains(cityName) || usedCityNames.Contains("Capital: " + cityName))
                    {
                        cityName = "New " + cityName;
                    }
                    else
                    {
                        break;
                    }
                }

                if(GameManager.Instance.cities.Count <= 0)
                {
                    cityName = "Capital: " + cityName; 
                }
                
                usedCityNames.Add(cityName);

                GameObject newCity = GameObject.Instantiate(mushroomCityPrefab, hit.point, mushroomCityPrefab.transform.rotation);
                newCity.gameObject.name = cityName;
                CityManager newCityManager = newCity.GetComponent<CityManager>();
                newCityManager.townName.text = cityName;

                foreach(CityManager connectedCityManager in GameManager.Instance.cities)
                {
                    if(GameManager.Instance.cities.Count <= 0)
                    {
                        break;
                    }

                    float distance = Vector3.Distance(hit.point, connectedCityManager.transform.position);

                    if(distance < connectedCityManager.networkConnectionRadius)
                    {
                        connectedCityManager.connectedCities.Add(newCityManager);
                        newCityManager.connectedCities.Add(connectedCityManager);

                        GameObject newNetworkPath = GameObject.Instantiate(networkLinePointPrefab, new Vector3(hit.point.x, hit.point.y - 0.25f, hit.point.z), mushroomCityPrefab.transform.rotation);
                        NetworkLineRenderer lineRenderer = newNetworkPath.GetComponent<NetworkLineRenderer>();

                        newNetworkPath.gameObject.name = connectedCityManager.gameObject.name + " to " + newCity.gameObject.name;

                        connectedCityManager.connectedMyceliumPaths.Add(newNetworkPath);
                        newCityManager.connectedMyceliumPaths.Add(newNetworkPath);
                        
                        lineRenderer.Setup(newCity.gameObject, connectedCityManager.gameObject);
                    }
                }

                GameManager.Instance.cities.Add(newCityManager);

                GameManager.Instance.EvaluateConnectedNutrients();
            }
        }
    }
}
