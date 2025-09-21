using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("Hit" + collision.gameObject.name);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("Hit" + collision.gameObject.name);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyRed>()?.TakeDamage(20);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(20);
            Destroy(gameObject);
        }
    }
}
