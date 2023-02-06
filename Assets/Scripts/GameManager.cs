using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("Error: GameManager is null!");
            }
            return instance;
        }
    }

    public enum InteractionMode
    {
        CityPlacement,
        Artillery
    }

    public InteractionMode interactionMode = InteractionMode.CityPlacement;

    public MainHUDController mainHUDController;
    public float waterGain = 0f;
    public float waterSupply = 30f;
    public float nutrientGain = 0f;
    public float nutrientSupply = 90f;
    public List<CityManager> cities;
    public List<GameObject> wateringHoles;
    public List<GameObject> deadAnimals;
    public List<GameObject> currentSpawnedRabbits;
    public TextMeshProUGUI totalNutrientsText;
    public TextMeshProUGUI totalWaterText;
    public TextMeshProUGUI connectedNutrientsText;
    public TextMeshProUGUI connectedWaterText;
    private List<CityManager> visitedCities;
    

    void Awake()
    {
        cities = new List<CityManager>();
        instance = this;
        Physics.IgnoreLayerCollision(7, 8);
        visitedCities = new List<CityManager>();
    }

    private void Update() 
    {
        if(cities.Count <= 0)
        {
            nutrientGain = 0;
            waterGain = 0;
        }

        GameManager.Instance.nutrientSupply += nutrientGain * Time.deltaTime;
        GameManager.Instance.waterSupply += waterGain * Time.deltaTime;


        totalNutrientsText.text = "Total Nutrients: " + nutrientSupply;
        totalWaterText.text = "Total Water: " + waterSupply;
    }

    public void EvaluateConnectedNutrients()
    {
        visitedCities = new List<CityManager>();
        int totalConnectedNutrients = 0;

        if(cities.Count <= 0)
        {
            return;
        }
        
        visitedCities.Add(cities[0]);
        Debug.Log("Visited " + cities[0].name);
        foreach(CityManager connectedCity in cities[0].connectedCities)
        {
            totalConnectedNutrients += EvaluateConnectedNutrients(connectedCity, 0);
        }
        foreach(GameObject squirrel in deadAnimals)
        {
            float distance = Vector3.Distance(cities[0].gameObject.transform.position, squirrel.transform.position);

            if(distance < 2f)
            {
                totalConnectedNutrients += 1;
            }
        }
        Debug.Log("Result of DFS searching for connected nutrients: " + totalConnectedNutrients);
        connectedNutrientsText.text = "Nutrients Connected to Capital: " + totalConnectedNutrients;
        if(cities.Count <= 0)
        {
            nutrientGain = 0;
        }
        else
        {
            nutrientGain = (float)totalConnectedNutrients;
        }
    }

    public int EvaluateConnectedNutrients(CityManager city, int totalConnectedNutrients)
    {
        // If already visited
        if(visitedCities.Contains(city))
        {
            return 0;
        }

        Debug.Log("Visited " + city.name);

        visitedCities.Add(city);
        foreach(CityManager connectedCity in city.connectedCities)
        {
            if(!visitedCities.Contains(connectedCity))
            {
                totalConnectedNutrients += EvaluateConnectedNutrients(connectedCity, totalConnectedNutrients);
            }
        }

        foreach(GameObject squirrel in deadAnimals)
        {
            float distance = Vector3.Distance(city.gameObject.transform.position, squirrel.transform.position);

            if(distance < 2f)
            {
                Debug.Log("City " + city.name + " is close to water, incrementing");
                totalConnectedNutrients += 1;
            }
        }
        return totalConnectedNutrients;
    }

    public void EvaluateConnectedWater()
    {
        visitedCities = new List<CityManager>();
        int totalConnectedWater = 0;

        if(cities.Count <= 0)
        {
            return;
        }
        
        visitedCities.Add(cities[0]);
        Debug.Log("Visited " + cities[0].name);
        foreach(CityManager connectedCity in cities[0].connectedCities)
        {
            totalConnectedWater += EvaluateConnectedNutrients(connectedCity, 0);
        }
        foreach(GameObject water in wateringHoles)
        {
            float distance = Vector3.Distance(cities[0].gameObject.transform.position, water.transform.position);

            if(distance < 2f)
            {
                totalConnectedWater += 1;
            }
        }
        Debug.Log("Result of DFS searching for connected water: " + totalConnectedWater);
        connectedWaterText.text = "Water Connected to Capital: " + totalConnectedWater;
        if(cities.Count <= 0)
        {
            waterGain = 0;
        }
        else
        {
            waterGain = (float)totalConnectedWater;
        }
    }

    public int EvaluateConnectedWater(CityManager city, int totalConnectedWater)
    {
        // If already visited
        if(visitedCities.Contains(city))
        {
            return 0;
        }

        Debug.Log("Visited " + city.name);

        visitedCities.Add(city);
        foreach(CityManager connectedCity in city.connectedCities)
        {
            if(!visitedCities.Contains(connectedCity))
            {
                totalConnectedWater += EvaluateConnectedNutrients(connectedCity, totalConnectedWater);
            }
        }

        foreach(GameObject water in wateringHoles)
        {
            float distance = Vector3.Distance(city.gameObject.transform.position, water.transform.position);

            if(distance < 2f)
            {
                Debug.Log("City " + city.name + " is close to a squirrel, incrementing");
                totalConnectedWater += 1;
            }
        }
        return totalConnectedWater;
    }
}
