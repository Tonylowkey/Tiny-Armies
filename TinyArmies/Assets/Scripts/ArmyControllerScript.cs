using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyControllerScript : MonoBehaviour
{
    public static ArmyControllerScript Instance;

    [SerializeField] LayerMask controllerLayer;
    [SerializeField] LayerMask militiaLayer;
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
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));


        var controllerHit = Physics2D.OverlapCircle(mousePos, 0.1f, controllerLayer);
        var militiaHit = Physics2D.OverlapCircle(mousePos, 0.1f, militiaLayer);
        



        if (selected != null)
            selected.SendMessage("Deselect");

        if(controllerHit == true)
        {

            selected = controllerHit.gameObject;

            selected.SendMessage("Select");

        }
        else if(militiaHit == true)
        {

            selected = FindObjectOfType<SquadControlScript>().gameObject;
            selected.SendMessage("Select");
        }
        else if(selected != null)
        {
            selected.SendMessage("Deselect");
            selected = null;
        }
    }
}
