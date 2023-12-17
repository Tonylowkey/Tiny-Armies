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
        var controllerHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), controllerLayer);
        var militiaHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), militiaLayer);

        if(selected != null)
            selected.SendMessage("Deselect");

        if(controllerHit.collider.gameObject.tag == "groupController")
        {
            selected = controllerHit.collider.gameObject;
            selected.SendMessage("Select");
        }
        else if(militiaHit.collider.gameObject.tag == "playerMilitia")
        {
            selected = militiaHit.collider.gameObject;
        }
        else if(selected != null)
        {
            selected.SendMessage("Deselect");
            selected = null;
        }
    }
}
