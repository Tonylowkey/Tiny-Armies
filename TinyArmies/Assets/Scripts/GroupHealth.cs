using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupHealth : MonoBehaviour
{
    public int sharedHealth = 100;

    public void TakeDamage(int damage)
    {
        sharedHealth -= damage;

        if (sharedHealth <= 0)
        {
            // Handle group defeat logic
            Destroy(gameObject);
        }
    }
}

