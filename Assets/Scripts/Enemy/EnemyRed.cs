using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class EnemyRed : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private Animator anim;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy Health: " + health);
        if (health <= 0)
        {
            anim.SetTrigger("DEATH");
            // Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); // Attacking // Stop Attacking
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 13f); // Detection (Start Chasing)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f); // Stop Chasing
    }
}
