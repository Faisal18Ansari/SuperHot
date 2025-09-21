using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Walking : StateMachineBehaviour
{
    float timer = 0f;
    public float walkTime = 10f;
    Transform player;
    NavMeshAgent agent;
    public float detectionRange = 19f;
    public float walkSpeed = 2f;
    List<Transform> wayPoints = new List<Transform>();//The places to be

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Initialization //
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        timer = 0;
        // Get all waypoints and Move to First Waypoint //
        GameObject waypointCluster = GameObject.FindGameObjectWithTag("WayPoints");
        foreach (Transform t in waypointCluster.transform)
        {
            wayPoints.Add(t);//Gotta go
        }
        // Random waypoint selection - because predictable AI is boring AI
        Vector3 nextPosition = wayPoints[Random.Range(0, wayPoints.Count)].position;
        agent.SetDestination(nextPosition);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        //If agent arrived at waypoint, move to next waypoint---//
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);//Gotta go again

        }
        //Transition to Idle State //
        timer += Time.deltaTime;
        if (timer >= walkTime)
        {
            animator.SetBool("WALK", false);
        }
        // Transition to Chase State //
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionRange)
        {
            animator.SetBool("CHASE", true);// "I see you"
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);// Stop moving
    }


}
