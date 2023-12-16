using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyControllerScript : MonoBehaviour
{
    public static ArmyControllerScript Instance;

    public GameObject selected;
    private GameObject target;
    private bool newSelect;

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Click();
    }

    public void Select(GameObject unit)
    {
        selected = unit;
        newSelect = true;
    }

    public void Click()
    {
        if(selected!=null && !newSelect)
        {
            selected.SendMessage("Deselect");
            selected = null;
        }
        
        newSelect = false;
    }
}
