using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Vector3 lookAtPoint;
    public float forceAmount = 10f;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(lookAtPoint);
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * forceAmount);
        StartCoroutine(DestroyAfterTime());
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "rabbit")
        {
            Debug.Log("Hit rabbit!");
            RabbitController rc = other.gameObject.GetComponent<RabbitController>();
            StartCoroutine(rc.Die());
        }    
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
