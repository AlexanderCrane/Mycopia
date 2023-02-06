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
    public float waterSupply = 30f;
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
        totalNutrientsText.text = "Total Nutrients: " + nutrientSupply;
        totalWaterText.text = "Total Water: " + waterSupply;
    }

    public int EvaluateConnectedNutrients()
    {
        visitedCities = new List<CityManager>();
        int totalConnectedNutrients = 0;

        if(cities.Count <= 0)
        {
            return 0;
        }
        
        visitedCities.Add(cities[0]);
        Debug.Log("Visited " + cities[0].name);
        foreach(CityManager connectedCity in cities[0].connectedCities)
        {
            totalConnectedNutrients += EvaluateConnectedNutrients(connectedCity, totalConnectedNutrients);
        }
        return totalConnectedNutrients;
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
            totalConnectedNutrients += EvaluateConnectedNutrients(connectedCity, totalConnectedNutrients);
        }
        return totalConnectedNutrients;
    }

    public int EvaluateConnectedWater()
    {
        visitedCities = new List<CityManager>();

        if(cities.Count <= 0)
        {
            return 0;
        }

        visitedCities.Add(cities[0]);
        Debug.Log("Visited " + cities[0].name);
        foreach(CityManager connectedCity in cities[0].connectedCities)
        {
            return EvaluateConnectedWater(connectedCity);
        }
        return 0;
    }

    public int EvaluateConnectedWater(CityManager city)
    {
        return 1;
    }
}
