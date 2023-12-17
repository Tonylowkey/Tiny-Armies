using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    public int wood;
    public int rock;
    public int iron;
    public int gold;


    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
