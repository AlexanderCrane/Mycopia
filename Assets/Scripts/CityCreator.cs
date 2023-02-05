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

                GameObject city = GameObject.Instantiate(mushroomCityPrefab, hit.point, mushroomCityPrefab.transform.rotation);
                city.GetComponent<CityManager>().townName.text = cityName;
                
                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
