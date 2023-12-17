using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damagePerTick = 5; 
    public float tickRate = 1f; 
    public float damageRadius = 3f; 
    private float timeSinceLastTick;

    private void Update()
    {
        if (Time.time - timeSinceLastTick >= tickRate)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);

            foreach (Collider2D collider in colliders)
            {
                if(collider.tag == "Enemy"){
                    GroupHealth health = collider.GetComponent<GroupHealth>();
                    if (health != null)
                    {
                        health.TakeDamage(damagePerTick);
                    }
                }
            }

            timeSinceLastTick = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the damage radius in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
