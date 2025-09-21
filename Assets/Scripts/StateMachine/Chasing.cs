using UnityEngine;
using UnityEngine.AI;

// Chasing.cs - Because personal space is overrated
public class Chasing : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float chaseSpeed = 6f;
    public float stopChasingDistance = 21f;// "You're too far, I give up"
    public float attackRange = 2.5f;// "Now it's personal"

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;// Always the Player
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agent.SetDestination(player.position); animator.transform.LookAt(player);// Always lookin' at you
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        // Checking if the agent should stop Chasing
        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("CHASE", false);// "Whatever, you're not worth it"
        }
        // Checking if the agent should Attack //
        if (distanceFromPlayer < attackRange)
        {
            animator.SetBool("ATTACK", true);// "SAY HELLO TO MY LITTLE... fist?"
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);// Stop moving
    }

}
