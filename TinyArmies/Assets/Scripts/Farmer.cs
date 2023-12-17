using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    public bool In;
    public float speed;

    public Vector2 mousePos;

    public bool Miner;

    SpriteRenderer sp;

    public Sprite sp1;
    public Sprite sp2;

    private Animator anim;

    public bool mining;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if(Miner == true)
        {
            sp.sprite = sp1;
        }
        if (Miner == false)
        {
            sp.sprite = sp2;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (mining == true)
        {
            anim.SetBool("Ismining", true);
        }
        if (mining == false)
        {
            anim.SetBool("Ismining", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            In = true;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (In == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);
            anim.SetBool("Isrunning", true);
            if (Vector2.Distance(transform.position, mousePos) < 0.1f)
            {
                In = false;
                anim.SetBool("Isrunning", false);
            }
        }
    }
}
