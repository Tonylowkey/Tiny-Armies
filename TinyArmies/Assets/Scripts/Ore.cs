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

    public Sprite sp1;
    public Sprite sp2;
    public Sprite sp3;

    public bool Tree;
    
    public SpriteRenderer sr;

    public int randomRotate;
    // Start is called before the first frame update
    void Start()
    {
        sl.maxValue = health;
        sl.value = health;
        if(Tree == true)
        {
            int random = Random.Range(0, 3);
            if(random == 0)
            {
                sr.sprite = sp1;
            }
            if (random == 1)
            {
                sr.sprite = sp2;
            }
            if (random == 2)
            {
                sr.sprite = sp3;
            }

        }
        if (Tree == false)
        {
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                sr.sprite = sp1;
            }
            if (random == 1)
            {
                sr.sprite = sp2;
            }
            if (random == 2)
            {
                sr.sprite = sp3;
            }



        }


         randomRotate = Random.Range(0, 30);
        if(randomRotate >= 16)
        {
            randomRotate = randomRotate / 2;
            Quaternion rotation = Quaternion.Euler(0, 0, randomRotate);
            transform.rotation = rotation;

        }
        if (randomRotate <= 15)
        {
            randomRotate -= randomRotate * 2;
            Quaternion rotation =  Quaternion.Euler(0,0 ,randomRotate);
            transform.rotation = rotation;
        }
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
