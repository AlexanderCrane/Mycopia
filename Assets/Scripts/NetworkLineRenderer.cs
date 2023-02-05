using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkLineRenderer : MonoBehaviour
{
    public Material nutrientSourceLineColor;
    public Material waterSourceLineColor;
    public Material bothResourcesLineColor;

    public void Setup(GameObject objectDrawingLinesFrom, GameObject objectToDrawLinesTo)
    {
        LineRenderer newLine = this.gameObject.GetComponent<LineRenderer>();
        bool closeToNutrients = false;
        bool closeToWater = false;

        foreach(GameObject squirrel in GameManager.Instance.deadAnimals)
        {
            float distance1 = Vector3.Distance(objectDrawingLinesFrom.transform.position, squirrel.transform.position);
            float distance2 = Vector3.Distance(objectToDrawLinesTo.transform.position, squirrel.transform.position);

            if(distance1 < 2f || distance2 < 2f)
            {
                closeToNutrients = true;
            }
        }

        foreach(GameObject waterHole in GameManager.Instance.wateringHoles)
        {
            float distance1 = Vector3.Distance(objectDrawingLinesFrom.transform.position, waterHole.transform.position);
            float distance2 = Vector3.Distance(objectToDrawLinesTo.transform.position, waterHole.transform.position);

            if(distance1 < 2f || distance2 < 2f)
            {
                closeToWater = true;
            }
        }

        if(closeToNutrients && closeToWater)
        {
            newLine.material = bothResourcesLineColor;
        }
        else if(closeToNutrients)
        {
            newLine.material = nutrientSourceLineColor;
        }
        else if(closeToWater)
        {
            newLine.material = waterSourceLineColor;
        }

        newLine.SetPosition(0, this.transform.position);
        newLine.SetPosition(1, new Vector3(objectToDrawLinesTo.gameObject.transform.position.x, objectToDrawLinesTo.gameObject.transform.position.y - 0.25f, objectToDrawLinesTo.gameObject.transform.position.z));
    }
}
