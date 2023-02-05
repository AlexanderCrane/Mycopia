using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkLineRenderer : MonoBehaviour
{
    public GameObject networkPoint;
    List<GameObject> objectsToDrawLinesTo;
    public GameObject debugSecondObject; //This is only for debugging

    // Start is called before the first frame update
    void Start()
    {
        objectsToDrawLinesTo = new List<GameObject>();

        LineRenderer newLine = this.gameObject.GetComponent<LineRenderer>();
        newLine.SetPosition(0, this.gameObject.transform.position);
        newLine.SetPosition(1, debugSecondObject.gameObject.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNewConnection(GameObject newConnection)
    {
        objectsToDrawLinesTo.Add(newConnection);
        LineRenderer newLine = this.gameObject.GetComponent<LineRenderer>();
        newLine.SetPosition(0, networkPoint.transform.position);
        newLine.SetPosition(1, newConnection.gameObject.transform.position);
    }
}
