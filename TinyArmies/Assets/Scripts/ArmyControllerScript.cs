using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyControllerScript : MonoBehaviour
{
    public static ArmyControllerScript Instance;

    [SerializeField] LayerMask controllerLayer;
    [SerializeField] LayerMask militiaLayer;

    [SerializeField] LayerMask EnemyLayer;
    public GameObject selected;
    private GameObject target;


    public List<float> Distances;
    

    public float SmallDist;


    public GameObject closestEnemy;

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

        if (Input.GetMouseButtonDown(1))
        {
            Collider2D[] EnemyHit = Physics2D.OverlapCircleAll(transform.position, 100, EnemyLayer);
            if (EnemyHit != null)
            {
                Distances.Clear();
               
                for (int i = 0; i < EnemyHit.Length; i++)
                {

                    Distances.Add(Vector2.Distance(transform.position, new Vector2(EnemyHit[i].transform.position.x, EnemyHit[i].transform.position.y)));
                   
                    
                }
                SmallDist = Distances.Min();
                for (int i = 0; i < EnemyHit.Length; i++)
                {
                    if (Distances[i] == SmallDist)
                    {
                        closestEnemy = EnemyHit[i].gameObject;
                    }
                }
                

                
            }
        }
        
    }

    public void Click()
    {
        var controllerHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, controllerLayer);
        var militiaHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, militiaLayer);

        if(selected != null)
            selected.SendMessage("Deselect");

        if(controllerHit.collider != null)
        {
            if(controllerHit.collider.gameObject.tag == "groupController")
            {
                selected = controllerHit.collider.gameObject;

                selected.SendMessage("Select");

            }
        }
        else if(militiaHit.collider != null)
        {
            if(militiaHit.collider.gameObject.tag == "playerMilitia")
            {
                selected = militiaHit.collider.gameObject;
                selected.SendMessage("Select");
            }
        }
        else if(selected != null)
        {
            selected.SendMessage("Deselect");
            selected = null;
        }
    }
}
