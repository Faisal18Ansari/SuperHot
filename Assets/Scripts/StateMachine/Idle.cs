using UnityEngine;

public class Idle : StateMachineBehaviour
{
    float timer = 0f;
    public float idleTime = 0f;
    Transform player;
    public float detectionRange = 18f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Start Walking
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("WALK", true);
        }

        // Start Chasing
        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceToPlayer < detectionRange)
        {
            animator.SetBool("CHASE", true);
        }
        
    }


}
