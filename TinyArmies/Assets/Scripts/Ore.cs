using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ore : MonoBehaviour
{
    public Slider sl;
    public float health;
    public float Damage;

    public float Radius;

    public int amount;

    public LayerMask mask;

    public bool Mining;
    // Start is called before the first frame update
    void Start()
    {
        sl.maxValue = health;
        sl.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        Mining = Physics2D.OverlapCircle(transform.position, Radius, mask);


        


        if (Mining == true )
        {
            sl.gameObject.SetActive(true);
            health -= Damage;
            sl.value = health;
            if (health <= 0)
            {

                Destroy(gameObject);
            }
        }
    }
}
