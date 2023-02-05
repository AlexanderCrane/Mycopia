using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Adapted from script written by Windexglow 11-13-10.
     
    float mainSpeed = 20.0f; //regular speed
    float shiftAdd = 50.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 100.0f; //Maximum speed when holding shift
    private float totalRun= 1.0f;
     
    void Update () {       
        //Keyboard commands
        // float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0) // only move while a direction key is pressed
        {
            if (Input.GetKey (KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p  = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            } else 
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }
         
            p = p * Time.deltaTime;
            Vector3 newPosition = transform.position;
            
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }
     
    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 then it's not active.
        Vector3 p_Velocity = new Vector3();

        //TODO: limits are currently hard-coded, may want to adjust base distance on map center
        if (Input.GetKey (KeyCode.W) && transform.position.x < 0f){
            p_Velocity += new Vector3(0, 0 , 1);
        }
        if (Input.GetKey (KeyCode.S) && transform.position.x > -27f){
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey (KeyCode.A) && transform.position.z < 15f){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D) && transform.position.z > -20f){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
