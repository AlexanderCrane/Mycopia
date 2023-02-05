using System.Collections;
using System.Collections.Generic;
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

    public float waterSupply;
    public float nutrientSupply;
    public List<CityManager> cities;
    public List<GameObject> wateringHoles;
    public List<GameObject> deadAnimals;
    public List<GameObject> currentSpawnedRabbits;
    

    void Awake()
    {
        cities = new List<CityManager>();
        instance = this;
        Physics.IgnoreLayerCollision(7, 8);
    }
}
