using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCreator : MonoBehaviour
{
    public GameObject mushroomCityPrefab;
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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                print("casting ray");
                print(hit.transform.tag);
                if(hit.collider.isTrigger && hit.transform.tag == "mushcity")
                {
                    print("inside mushcity");
                    hit.transform.GetComponent<CityManager>().MushCityCanvas.SetActive(true);
                    Time.timeScale = 0;
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

                    if(distance < city.radius)
                    {
                        overlapsExistingCity = true;
                        Debug.Log("Distance from " + city.townName.text + " is " + distance + " which is less than " + city.radius);
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
                    if(usedCityNames.Contains(cityName))
                    {
                        cityName = "New " + cityName;
                    }
                    else
                    {
                        break;
                    }
                }
                
                usedCityNames.Add(cityName);

                GameObject newCity = GameObject.Instantiate(mushroomCityPrefab, hit.point, mushroomCityPrefab.transform.rotation);
                CityManager newCityManager = newCity.GetComponent<CityManager>();
                newCityManager.townName.text = cityName;
                GameManager.Instance.cities.Add(newCityManager);
                
                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
