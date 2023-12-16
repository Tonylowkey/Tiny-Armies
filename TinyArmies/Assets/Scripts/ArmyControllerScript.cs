using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyControllerScript : MonoBehaviour
{
    public static ArmyControllerScript Instance;

    private GameObject selected;
    private GameObject target;

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
    }

    public void Select(GameObject unit)
    {
        selected = unit;

        Debug.Log("selected");
    }
}
