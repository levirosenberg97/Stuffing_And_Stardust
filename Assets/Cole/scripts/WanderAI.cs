using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{

    public float WanderRadius;
    public float MaxTime;
    public float MinTime;
    public float Speed;

    private Transform Target;
    private NavMeshAgent Agent;

    private float Timer;
    private float BoogyTime;

	// Use this for initialization
	void OnEnable ()
    {
        Agent = GetComponent<NavMeshAgent>();
        Timer = MinTime;
	}

	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;

        if(Timer >= BoogyTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position, WanderRadius, -1);
            Agent.SetDestination(newPos);
            Timer = 0;
           
        }
	}

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int Layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, Layermask);

        return navHit.position; 
    }

    void SetRandTime()
    {
        BoogyTime = Random.Range(MinTime, MaxTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, WanderRadius);
    }
}
