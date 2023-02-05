using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCreator : MonoBehaviour
{
    public GameObject mushroomCityPrefab;
    public bool placingCity = true; // TODO: This should change to be false, and only set to true when user initiates city placement

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                GameObject.Instantiate(mushroomCityPrefab, hit.point, mushroomCityPrefab.transform.rotation);
                
                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
