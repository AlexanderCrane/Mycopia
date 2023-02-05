using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitController : MonoBehaviour
{
    public bool running = false;
    public Transform currentTarget;
    NavMeshAgent agent;
    bool finishedEating = true;
    Animator animator;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(waitToChooseNewTarget());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Velocity: " + GetComponent<Rigidbody>().velocity.magnitude);
        // if(!running && GetComponent<Rigidbody>().velocity.magnitude > 5f)
        // {
        //     running = true;
        //     animator.SetInteger("AnimIndex", 1);
        // }
        // else if(GetComponent<Rigidbody>().velocity.magnitude < 5f)
        // {
        //     running = false;
        //     animator.SetInteger("AnimIndex", 0);
        // }

        // Check if we've reached the destination
        if (!agent.pathPending && currentTarget != null && !dead)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetInteger("AnimIndex", 0);

                    // Reached target city
                    if(finishedEating)
                    {
                        StartCoroutine(eatMushroomCity());
                    }
                }
            }
        }
    }
    
    public IEnumerator Die()
    {
        Debug.Log("Rabbit attempting to die");
        dead = true;
        animator.SetInteger("AnimIndex", 2);
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<CapsuleCollider>().enabled = false;
        // agent.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    public IEnumerator waitToChooseNewTarget()
    {
        Debug.Log("Started target acquisition coroutine");
        yield return new WaitForSeconds(3f);
        if(currentTarget == null && GameManager.Instance.cities.Count > 0 && !dead)
        {
            int randomlyChosenTarget = Random.Range(0, GameManager.Instance.cities.Count);
            currentTarget = GameManager.Instance.cities[randomlyChosenTarget].transform;
            Debug.Log("Set rabbit target to " + currentTarget.gameObject.name);
            agent.destination = currentTarget.transform.position;
            animator.SetInteger("AnimIndex", 1);
        }

        if(currentTarget == null && !dead)
        {
            StartCoroutine(waitToChooseNewTarget());
        }
    }

    public IEnumerator eatMushroomCity()
    {
        finishedEating = false;
        yield return new WaitForSeconds(3f);
        CityManager targetCityManager = currentTarget.GetComponent<CityManager>();
        targetCityManager.DeleteCity();
        currentTarget = null;
        finishedEating = true;
        StartCoroutine(waitToChooseNewTarget());
    }
}
