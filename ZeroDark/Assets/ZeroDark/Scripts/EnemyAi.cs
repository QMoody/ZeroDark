using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAi : MonoBehaviour
{
    private GameObject Target;
    public GameObject Player;
    public GameObject patrolArea;

    public GameObject lightOrb;

    public float enemyDistanceRun;

    NavMeshAgent agent;




    public enum State
    {
        Patrol,
        Chase,
        LostSight,
        Flee


    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.Chase;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.Chase:
                ChaseThePlayer();
                break;
            case State.LostSight:
                LostThePlayer();
                break;
            case State.Flee:
                FleeFromLightOrb();
                break;
            case State.Patrol:
                PatrolTheArea();
                break;


        }   
    }


    void ChaseThePlayer()
    {
        agent.SetDestination(Target.transform.position);
    }

    void LostThePlayer()
    {

        agent.SetDestination(Target.transform.position);

    }

    void FleeFromLightOrb()
    {
        float distance = Vector3.Distance(transform.position, lightOrb.transform.position);
        Debug.Log("RUNAWAY");

        //running away from the light orb
        if( distance < enemyDistanceRun)
        {
            Vector3 dirToLightOrb = transform.position - lightOrb.transform.position;
            Vector3 newPos = transform.position + dirToLightOrb;

            agent.SetDestination(newPos);

        }


    }

    void PatrolTheArea()
    {
        if( Target.transform.position == patrolArea.transform.position && Target != null)
        {


        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Target = Player;
            state = State.Chase;

        }

        if( other.tag == "LightOrb")
        {
            Target = null;
            state = State.Flee;
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            state = State.LostSight;
            Target = patrolArea;

        }
        
        if(other.tag == "LightOrb")
        {
            state = State.Patrol;
            Target = patrolArea;

        }
    }
}
