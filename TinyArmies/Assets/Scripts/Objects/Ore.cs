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

    public Collider2D Mining;

    public Sprite sp1;
    public Sprite sp2;
    public Sprite sp3;

    public bool Tree;
    
    public SpriteRenderer sr;

    public int randomRotate;

    public int oreIndex;

    Gamemanager gm;
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
            oreIndex = 1;
        }
        if (Tree == false)
        {
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                sr.sprite = sp1;
                oreIndex = 3;
            }
            if (random == 1)
            {
                sr.sprite = sp2;
                oreIndex = 2;
            }
            if (random == 2)
            {
                sr.sprite = sp3;
                oreIndex = 1;
            }



        }


         randomRotate = Random.Range(0, 20);
        if(randomRotate >= 10)
        {
            randomRotate = randomRotate / 2;
            Quaternion rotation = Quaternion.Euler(0, 0, randomRotate);
            transform.rotation = rotation;

        }
        if (randomRotate <= 9)
        {
            randomRotate -= randomRotate;
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
            Mining.gameObject.GetComponent<Farmer>().mining = true;
            sl.gameObject.SetActive(true);
            health -= Damage;
            sl.value = health;
            if (health <= 0)
            {
                Check();
                GridManager.Instance.tiles[(int)(transform.position.x)][(int)(transform.position.y)].GetComponent<TileScript>().occupied = false;
                Destroy(gameObject);
            }
        }
        if(Mining == false)
        {
            if(Mining != null)
            {
                Mining.gameObject.GetComponent<Farmer>().mining = false;
            }
           
        }
    }

    public void Check()
    {
        if(Tree == true)
        {
            if (oreIndex == 1)
            {
                Gamemanager.Instance.wood += amount;
            }
            if (oreIndex == 2)
            {
                Gamemanager.Instance.wood += amount;
            }
            if (oreIndex == 3)
            {
                Gamemanager.Instance.wood += amount;
            }
        }
        if (Tree == false)
        {
            if (oreIndex == 1)
            {
                Gamemanager.Instance.rock += amount;
            }
            if (oreIndex == 2)
            {
                Gamemanager.Instance.wood += amount;
            }
            if (oreIndex == 3)
            {
                Gamemanager.Instance.iron += amount;
            }
        }
    }
}
