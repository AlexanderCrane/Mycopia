using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CityManager : MonoBehaviour
{
    public TextMeshProUGUI townName;
    public float resourceCost = 0.1f;
    public float cantBuildNearMeRadius = 10f;
    public float networkConnectionRadius = 15f;
 
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
}
